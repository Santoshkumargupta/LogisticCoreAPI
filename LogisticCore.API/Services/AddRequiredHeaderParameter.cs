using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace LogisticCore.Api.Services
{
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "APIkey",
                @In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Title = "key",
                    Description="value",
                    Type = "string"
                }
            });
        }
    }
}
