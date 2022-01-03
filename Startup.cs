using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChipsApi.Authentication;
using ChipsApi.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Telerik.Reporting.Cache.File;
using Telerik.Reporting.Services;
using Telerik.Reporting.Services.AspNetCore;

namespace ChipsApi
{
    public class Startup
    {
        private IServiceCollection services;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            this.services = services;
            services.ConfigureJwtAuthentication();
            services.AddCors();
            services.AddControllers();

            //System.IO.Path.Combine(sp.GetService<IWebHostEnvironment>().ContentRootPath, "Reports");

            services.TryAddSingleton<IReportServiceConfiguration>(sp =>
               new ReportServiceConfiguration
               {
                   ReportingEngineConfiguration = ConfigurationHelper.ResolveConfiguration(sp.GetService<IWebHostEnvironment>()),
                   HostAppId = "ReportingCore3App",
                   Storage = new FileStorage(),
                   ReportSourceResolver = new UriReportSourceResolver(
                       System.IO.Path.Combine(sp.GetService<IWebHostEnvironment>().ContentRootPath, "Reports")),

                   //ReportResolver = new ReportTypeResolver().AddFallbackResolver(new ReportFileResolver(System.IO.Path.Combine(sp.GetService<IWebHostEnvironment>().ContentRootPath, "Reports")))
               });



            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
            });
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Chips API", Version = "v1" });
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };
                c.AddSecurityDefinition("Bearer", //Name the security scheme
                new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
                    Scheme = "bearer" //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer", //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });
            });
            // configure strongly typed settings objects
            //var appSettingsSection = Configuration.GetSection("AppSettings");
            //services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            //var appSettings = appSettingsSection.Get<AppSettings>();
            //var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddRazorPages()
    .AddNewtonsoftJson();

            //})
            //.AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };
            //});

            //// configure DI for application services
            //services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            //this.services.AddTransient(ctx => new TestValuesController());
            //this.services.AddSingleton(typeof(TestValuesController));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //c.SwaggerEndpoint("v1/swagger.json", "MyAPI V1");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Api V1");
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                //endpoints.MapRazorPages();
                endpoints.MapControllers();
                //endpoints.MapControllerRoute(
                //name: "default",
                //pattern: "{controller=Reports}/{action=Index}/{InvoiceID?}");
        });

        }
    }
    public class ConfigurationService
    {
        public IConfiguration Configuration { get; private set; }

        public IWebHostEnvironment Environment { get; private set; }
        private string configFileName = "appsettings.json";

        public ConfigurationService(IWebHostEnvironment environment)
        {
            this.Environment = environment;

            var configFileName = System.IO.Path.Combine(environment.ContentRootPath, this.configFileName);
            var config = new ConfigurationBuilder()
                            .AddJsonFile(configFileName, true)
                            .Build();
            //var defaultBuilder = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder()
            //    .Build();
            //var config = defaultBuilder.Services.GetService<IConfiguration>();

            this.Configuration = config;
        }
    }

    static class ConfigurationHelper
    {
        public static IConfiguration ResolveConfiguration(IWebHostEnvironment environment)
        {
            var reportingConfigFileName = System.IO.Path.Combine(environment.ContentRootPath, "appsettings.json");
            return new ConfigurationBuilder()
                .AddJsonFile(reportingConfigFileName, true)
                .Build();
        }
    }





}


