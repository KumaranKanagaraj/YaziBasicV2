using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO;
using YaziBasicV2.Entities;
using YaziBasicV2.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using YaziBasicV2.Services;
using Microsoft.AspNetCore.Diagnostics;
using YaziBasicV2.Models;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;

namespace YaziBasicV2
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Debug()
                            .WriteTo.RollingFile(Path.Combine(env.ContentRootPath, "Logging", "log-{Date}.txt"))
                            .CreateLogger();
        }

        private string GetXmlCommentsPath()
        {
            var app = PlatformServices.Default.Application;
            return System.IO.Path.Combine(app.ApplicationBasePath, "YaziBasicV2.xml");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add service and create Policy with options
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination")
                    .AllowCredentials());
            });
            services.AddDbContext<ArticlesContext>(options =>
            {
                options.UseMySql(UriHelper.DbConnectionString);
            });
            services.AddScoped<IVerityRepository, VerityRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISocialImpressionRepository, SocialImpressionRepository>();


            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(implementationFactory =>
            {
                var actionContext = implementationFactory.GetService<IActionContextAccessor>()
                .ActionContext;
                return new UrlHelper(actionContext);
            });
            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            });
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("libraryapi",
                 new Info
                 {
                     Title = "Api Documentation"
                 });
                setupAction.IncludeXmlComments(GetXmlCommentsPath());
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              ILoggerFactory loggerFactory,
                              IApplicationLifetime appLifetime)
        {
            // global policy - assign here or on each controller
            app.UseCors("CorsPolicy");

            loggerFactory.AddConsole();
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global exception logger");
                            logger.LogError(500,
                                exceptionHandlerFeature.Error,
                                exceptionHandlerFeature.Error.Message,
                                exceptionHandlerFeature.Error.StackTrace);
                        }

                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexcepted fault happened. Try again later." + exceptionHandlerFeature.Error.Message + "   --->" + exceptionHandlerFeature.Error.StackTrace);
                    });
                });
            }

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Verity, VerityDto>();
                cfg.CreateMap<VerityDto, Verity>();

                cfg.CreateMap<Verity, VerityForDisplayDto>();
                cfg.CreateMap<VerityForDisplayDto, Verity>();

                cfg.CreateMap<Category, CategoryDto>();
                cfg.CreateMap<CategoryDto, Category>();

            });

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseSwagger(); 
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/libraryapi/swagger.json", "Api Documentation");
            }); 

        }


    }
}
