using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using WebCoreApi;

namespace Web.JwtApp
{
    public class MySecurityTokenValidator : ISecurityTokenValidator
    {

        public bool CanValidateToken { get; set; }

        public int MaximumTokenSizeInBytes { get; set; }

        public bool CanReadToken(string securityToken)
        {
            return true;
        }

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            var bytes = Convert.FromBase64String(securityToken);
            var json = Encoding.UTF8.GetString(bytes);
            var token = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(json);
            if (token.ETag != token.GetEag())
            {
                throw new Exception("Token被篡改了");
            }
            if (token.expires < DateTime.Now || token.NotBefore > DateTime.Now)
            {
                throw new Exception("Token失效");
            }
            validatedToken = null;
            var claims = new List<Claim> {
                new Claim("Id",token.Id.ToString()),
            };
            var roles = token.Roles.Split(',');
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            //var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            //identity.AddClaim(new Claim("name", "jesse"));
            //identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, securityToken == "jessetalk.cn" ? "admin" : "user"));
            //var principal = new ClaimsPrincipal(identity);
            //var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
            //var principal = new ClaimsPrincipal(identity);




            //var claims = new Claim[] {
            //    new Claim( ClaimTypes.Name,validationParameters.NameClaimType ),
            //    new Claim(ClaimTypes.Role,validationParameters.RoleClaimType)
            //};
            var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            //var key = validationParameters.IssuerSigningKey;
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var head = new JwtHeader(creds);
            //var load = new JwtPayload("issuer", "audidence", claims, notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(2));
            //validatedToken = new JwtSecurityToken(head, load);
            //var token = new JwtSecurityTokenHandler().WriteToken(validatedToken);

            //throw new Exception("token is guoqi");




            return principal;




        }
    }
}
