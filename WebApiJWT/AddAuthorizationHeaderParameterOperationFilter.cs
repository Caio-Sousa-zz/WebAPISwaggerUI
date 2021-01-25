using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Web.Http.Description;
using IOperationFilter = Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter;

namespace WebApiJWT
{
    public class AddAuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
      
        public void Apply(OpenApiOperation operation, Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "X-User-Token",
                Description = "Teste",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema() { Type = "String" },
                Required = true,
                Example = new OpenApiString("Tenant ID example")
            });
        }
    }
}
