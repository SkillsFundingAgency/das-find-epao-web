﻿using System.Collections.Generic;
using System.IO;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourses;
using SFA.DAS.FindEpao.Domain.Configuration;
using SFA.DAS.FindEpao.Infrastructure.HealthCheck;
using SFA.DAS.FindEpao.Web.AppStart;

namespace SFA.DAS.FindEpao.Web
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfigurationRoot _configuration;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _environment = environment;
            
            var config = new ConfigurationBuilder()
                .AddConfiguration(configuration)
                .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile("appsettings.Development.json", true)
#endif
                .AddEnvironmentVariables();

            
            _configuration = config.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddOptions();
            services.Configure<FindEpaoApi>(_configuration.GetSection("FindEpaoApi"));
            services.AddSingleton(cfg => cfg.GetService<IOptions<FindEpaoApi>>().Value);
            services.Configure<FindEpaoWeb>(_configuration.GetSection("FindEpaoWeb"));
            services.AddSingleton(cfg => cfg.GetService<IOptions<FindEpaoWeb>>().Value);
            
            services.AddControllersWithViews();

            services.AddServiceRegistration();
            services.AddMediatR(typeof(GetCoursesQueryHandler).Assembly);
            services.AddMediatRValidation();

            services.AddApplicationInsightsTelemetry(options =>
            {
                options.ConnectionString = _configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"];
            });

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            if (!_environment.IsDevelopment())
            {
                services.AddHealthChecks()
                    .AddCheck<FindEpaoOuterApiHealthCheck>(
                        "Find Epao Outer Api",
                        failureStatus: HealthStatus.Unhealthy,
                        tags: ["ready"]);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHealthChecks();
                app.UseExceptionHandler("/Error/500");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.Use(async (context, next) =>
            {
                if (context.Response.Headers.ContainsKey("X-Frame-Options"))
                {
                    context.Response.Headers.Remove("X-Frame-Options");
                }

                context.Response.Headers.TryAdd("X-Frame-Options", "SAMEORIGIN");

                await next();

                if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
                {
                    //Re-execute the request so the user gets the error page
                    var originalPath = context.Request.Path.Value;
                    context.Items["originalPath"] = originalPath;
                    context.Request.Path = "/error/404";
                    await next();
                }
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
