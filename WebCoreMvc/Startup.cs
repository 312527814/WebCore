using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Web.Service.Domain;
using Web.Service.IRepository;
using Web.Service.DataRepository;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Web.Service;
using AspectCore.Extensions.Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Net.Http.Headers;

namespace WebCoreMvc
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
            //services.AddDirectoryBrowser();
            services.Configure<ConnectionSettings>(Configuration);
            //services.AddSingleton<ConnectionSettings>(new ConnectionSettings());
            services.AddSession();
            services.AddMvc(options =>
            {
                options.Filters.Add<HttpGlobalExceptionFilter>();
            });
           
            services.AddSingleton<DapperModule>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/MyLogin";
                });

            // Add other framework services
            // Add Autofac
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            
            containerBuilder.RegisterModule<DapperModule>();
            containerBuilder.RegisterModule<CoreModule>();


            containerBuilder.RegisterDynamicProxy();
            var container = containerBuilder.Build();

            var serviceProvider = new AutofacServiceProvider(container);

            return serviceProvider;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
           
            app.UseSession();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseBrowserLink();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            app.UseExceptionHandler(new ExceptionHandlerOptions()
            {
                ExceptionHandler = context =>
                {
                    var ex = context.Features.Get<IExceptionHandlerPathFeature>().Error;
                    context.Response.ContentLength = null;
                    context.Response.ContentType = "";
                    return Task.CompletedTask;
                },
                ExceptionHandlingPath = "/Home/Error"
            });

            //app.UseDirectoryBrowser(
            //    new DirectoryBrowserOptions()
            //    {
            //        RequestPath = "/a"
            //    });
            string contentRoot = Directory.GetCurrentDirectory();

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(contentRoot, "StaticFile")),
            //    RequestPath = "/Static",

            //});
            //var ss = new PhysicalFileProvider(Path.Combine(contentRoot, "StaticFile"));
            //var change = ss.Watch(@"123.txt");
            //change.RegisterChangeCallback((o) => {
            //    var c = o;
            //}, true);

            //app.UseFileServer(new FileServerOptions()
            //{
            //    //EnableDirectoryBrowsing = true,
            //    //FileProvider = new PhysicalFileProvider()
            //    RequestPath = "/Static",

            //});
            app.UseStaticFiles();
            app.UseAuthentication();


            //app.UseStatusCodePages(async context =>
            //{
            //    var httpContext = context.HttpContext;
            //    //var error = httpContext.Features.Get<IExceptionHandlerPathFeature>().Error;
            //    httpContext.Request.Path = "/home/about";
            //    await context.Next(context.HttpContext);
            //});
            //app.Run(async context =>
            //{
            //    await Task.Run(() =>
            //    {
            //        throw new Exception("dd");
            //    });
            //});

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            

            //app.Run(context =>
            //{
            //    return Task.CompletedTask;
            //});


        }


    }

    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }



}
