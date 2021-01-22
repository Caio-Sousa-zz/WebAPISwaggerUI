using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codtran.Api.Helpers.Attribute
{
    public class HideSwaggerFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var contextApiDescription in context.ApiDescriptions)
            {
                var actionDescriptor = (ControllerActionDescriptor)contextApiDescription.ActionDescriptor;

                if (actionDescriptor.ControllerTypeInfo.GetCustomAttributes(typeof(HideSwaggerDocAttribute), false).Any() ||
                    actionDescriptor.MethodInfo.GetCustomAttributes(typeof(HideSwaggerDocAttribute), false).Any())
                {
                    var key = "/" + contextApiDescription.RelativePath.TrimEnd('/');
                    swaggerDoc.Paths.Remove(key);
                }
            }
        }
    }
}