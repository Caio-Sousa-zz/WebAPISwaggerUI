using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                var apiinfo = new OpenApiInfo
                {
                    Title = "theta-CandidateAPI",
                    Version = "v1",
                    Description = "Candidate API for thetalentbot",
                    Contact = new OpenApiContact
                    { 
                        Name = "thetalentbot", Url = new Uri("https://google.com") 
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "Commercial",
                        Url = new Uri("https://google.com")
                    }
                };

                //BEARER SECURITY SCHEME
                OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                };

                OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                {
                    {securityDefinition, new string[] { }},
                };

                options.SwaggerDoc("v1", apiinfo);
                options.AddSecurityDefinition("jwt_auth", securityDefinition);
              
                // Make sure swagger UI requires a Bearer token to be specified
                options.AddSecurityRequirement(securityRequirements);            
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.InjectJavascript("/swagger-ui/js/custom.js");
                    c.InjectStylesheet("/swagger-ui/css/custom.css");
                    c.DefaultModelsExpandDepth(-1);
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
                });

                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}