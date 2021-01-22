using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebAPIRedDoc.Helpers
{
    //https://blog.kloud.com.au/2017/08/04/swashbuckle-pro-tips-for-aspnet-web-api-part-1/
    //https://stackoverflow.com/questions/55329325/how-to-add-x-code-samples-for-redoc-with-swashbuckle-aspnetcore
    public class XCodeSamplesFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            string source = @"PetStore.v1.Pet pet = new PetStore.v1.Pet();
                               pet.setApiKey(""your api key"");
                               pet.petType = PetStore.v1.Pet.TYPE_DOG;
                               pet.name = ""Rex"";
                               // set other fields
                               PetStoreResponse response = pet.create();
                               if (response.statusCode == HttpStatusCode.Created)
                               {
                                 // Successfully created
                               }
                               else
                               {
                                 // Something wrong -- check response for errors
                                 Console.WriteLine(response.getRawResponse());
                               }";
            // need to check if extension already exists, otherwise swagger 
            // tries to re-add it and results in error  
            var  codeSample = "x-code-samples";

            if (!swaggerDoc.Info.Extensions.ContainsKey(codeSample))
                 swaggerDoc.Info.Extensions.Add(codeSample, new OpenApiObject
                {
                    {"lang", new OpenApiString("PHP")},
                    //{"label", new OpenApiString("")},
                    {"source", new OpenApiString("cut for brevity")},
                });

        }
    }
}
