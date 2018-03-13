using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Web.JwtApp;
using Web.Service.DataRepository;
using Autofac;
using Web.Service;
using Autofac.Extensions.DependencyInjection;
using AspectCore.Extensions.Autofac;
using Web.Service.Core;
using WebCoreApi;
using WebCoreApi.filters;
using Microsoft.AspNetCore.Http;

namespace WebCoreApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConnectionSettings>(Configuration);
            var jwtSettings = new JwtSettings();
            Configuration.Bind(jwtSettings);
            services.Configure<JwtSettings>(Configuration);
            //services.BuildServiceProvider().GetService<IOptions<JwtSettings>>();
            services.AddMvc(
                options =>
                {
                    options.Filters.Add<HttpGlobalExceptionFilter>();
                }
                );
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SecurityTokenValidators.Clear();
                    options.SecurityTokenValidators.Add(new MySecurityTokenValidator());
                    //options.TokenValidationParameters = new TokenValidationParameters()
                    //{
                    //    ValidIssuer = jwtSettings.Issuer,// "issuer",
                    //    ValidAudience = jwtSettings.Audience,// "audidence",
                    //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey))
                    //};

                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            return Task.Run(() =>
                            {
                                var token = context.Request.Headers["token"];
                                context.Token = token.FirstOrDefault();
                            });
                        },
                        OnTokenValidated = context =>
                        {
                            return Task.Run(() =>
                            {
                                var token = context.SecurityToken;
                                //context.Fail("SecurityToken is null ");
                            });
                        }

                    };

                });
            services.AddLog4();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Add other framework services
            // Add Autofac
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            containerBuilder.RegisterModule<ServiceModule>();
            containerBuilder.RegisterModule<DataRepositoryModule>();
            containerBuilder.RegisterType<MySession>().As<IMySession>().SingleInstance();


            containerBuilder.RegisterDynamicProxy();
            var container = containerBuilder.Build();

            var serviceProvider = new AutofacServiceProvider(container);

            return serviceProvider;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();

        }
    }
}
