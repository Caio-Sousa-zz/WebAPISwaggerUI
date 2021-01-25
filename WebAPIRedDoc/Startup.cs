using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using WebAPIRedDoc.Helpers;

namespace WebAPIRedDoc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private static string GetDescription()
        {
            return File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "redoc",
                "description.md"));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Extensions = new Dictionary<string, IOpenApiExtension>
                    {
                        {
                            "x-logo", new OpenApiObject
                            {
                                {
                                    "url",
                                    new OpenApiString(
                                        "https://rdlcom.com/wp-content/uploads/qa-testing-as-a-service-test-io-creative-company-logo-terrific-1.png")
                                },
                                {"altText", new OpenApiString("COMPANY")}
                            }
                        }
                    },
                    Description = GetDescription()
                });

                c.DocumentFilter<XCodeSamplesFilter>();

                //BEARER SECURITY SCHEME
                var securityDefinition = new OpenApiSecurityScheme
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http
                };

                var securityRequirements = new OpenApiSecurityRequirement
                {
                    {securityDefinition, new string[] { }}
                };

                c.AddSecurityDefinition("jwt_auth", securityDefinition);

                // Make sure swagger UI requires a Bearer token to be specified
                c.AddSecurityRequirement(securityRequirements);

                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseReDoc(c =>
            {
                c.RoutePrefix = "redoc";
                c.DocumentTitle = "API Documentation";
                c.SpecUrl = "/swagger/v1/swagger.json";
                c.InjectStylesheet("/redoc/custom.css");
                c.IndexStream = () => GetStream("redoc.index.html"); // requires file to be added as an embedded resource
            });
            
            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "My API V1"); });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public static Stream GetStream(string resourceName)
        {
            var assembly = Assembly.GetAssembly(typeof(Startup));
            var name = assembly.GetManifestResourceNames().Where(a => a.Contains(resourceName)).FirstOrDefault();

            return assembly.GetManifestResourceStream(name);
        }
    }
}