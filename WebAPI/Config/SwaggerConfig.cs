using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Config
{
    public static class SwaggerConfig
    {
        private static OpenApiInfo GetOpenApiInfo => new OpenApiInfo
        {
            Title = "theta-CandidateAPI",
            Version = "v1",
            Description = "Candidate API for thetalentbot",
            Contact = new OpenApiContact
            {
                Name = "thetalentbot",
                Url = new Uri("https://google.com")
            },
            License = new OpenApiLicense()
            {
                Name = "Commercial",
                Url = new Uri("https://google.com")
            }
        };

        public static IServiceCollection AddSwaggerConfigBearerToken(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.DocInclusionPredicate((docName, description) => true);

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

                c.SwaggerDoc("v1", GetOpenApiInfo);

                c.AddSecurityDefinition("jwt_auth", securityDefinition);

                // Make sure swagger UI requires a Bearer token to be specified
                c.AddSecurityRequirement(securityRequirements);
            });

            return services;
        }

        public static IServiceCollection AddSwaggerConfigurationApiKeySecurity(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.DocInclusionPredicate((docName, description) => true);

                c.AddSecurityDefinition(ApiKeyConstants.HeaderName, new OpenApiSecurityScheme
                {
                    Description = "Api key needed to access the endpoints. X-Api-Key: My_API_Key",
                    In = ParameterLocation.Header,
                    Name = ApiKeyConstants.HeaderName,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = ApiKeyConstants.HeaderName,
                            Type = SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = ApiKeyConstants.HeaderName
                            },
                         },
                         new string[] {}
                     }
                });

                c.SwaggerDoc("v1", GetOpenApiInfo);
            });

            return services;
        }

        public static IServiceCollection AddSwaggerConfigurationOAuth2(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.DocInclusionPredicate((docName, description) => true);

                OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("/auth-server/connect/authorize", UriKind.Relative),
                            TokenUrl = new Uri("/auth-server/connect/token", UriKind.Relative),
                            Scopes = new Dictionary<string, string>
                            {
                                {"readAccess", "Access read operations"},
                                {"writeAccess", "Access write operations"}
                            }
                        }
                    }
                };

                c.AddSecurityDefinition("oauth2", securityScheme);

                c.SwaggerDoc("v1", GetOpenApiInfo);
            });

            return services;
        }

        public static class ApiKeyConstants
        {
            public static string HeaderName => "teste";
        }
    }
}