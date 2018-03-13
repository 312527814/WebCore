using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using WebCoreApi;
using Web.Service;

namespace WebCoreApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Account/[Action]")]
    public class AccountController : Controller
    {
        public JwtSettings jwtSettings { get; set; }
        private ILogger logger;
        //public AccountController(IOptions<JwtSettings> options, ILoggerFactory loggerFactory)
        //{

        //    jwtSettings = options.Value;
        //    //this.logger = loggerFactory.CreateLogger<Microsoft.AspNetCore.Mvc.Internal.MvcAttributeRouteHandler>();
        //    //logger.LogError("ddddd");
        //}
        public IActionResult myLogin()
        {
            /**
            var claims = new Claim[] {
                new Claim( ClaimTypes.Name,"zhangsan" ),
                new Claim(ClaimTypes.Role,"1")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var head = new JwtHeader(creds);
            var load = new JwtPayload(jwtSettings.Issuer,jwtSettings.Audience, claims, notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(2));
            var token = new JwtSecurityToken(head, load);
            
            //var token = new JwtSecurityToken("issuer", "audidence", claims, notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(2), signingCredentials: creds);
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    **/
            var token = new Token()
            {
                Id = 1,
                Roles = "1",
                NotBefore = DateTime.Now,
                expires = DateTime.Now.AddHours(1),
            };
            token.ETag = token.GetEag();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(token);
            var base64Json = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            return Content(base64Json);
        }
    }
}