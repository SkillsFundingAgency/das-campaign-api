using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;

namespace SFA.DAS.Campaign.Api.Filters;

[ExcludeFromCodeCoverage]
public class HealthChecksFilter : IDocumentFilter
{
    private const string HealthCheckEndpoint = @"/health";

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var pathItem = new OpenApiPathItem();
        var operation = new OpenApiOperation();

        operation.Tags ??= new HashSet<OpenApiTagReference>();
        operation.Tags.Add(new OpenApiTagReference("Service Status", swaggerDoc, null));

        operation.Responses ??= [];

        var healthyResponse = new OpenApiResponse
        {
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["text/plain"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = JsonSchemaType.String,
                        Enum = [
                            JsonValue.Create("Healthy"),
                        ]
                    }
                }
            }
        };

        operation.Responses.Add("200", healthyResponse);

        var unhealthyResponse = new OpenApiResponse
        {
            Content = new Dictionary<string, OpenApiMediaType>()
        };
        unhealthyResponse.Content["text/plain"] = new OpenApiMediaType
        {
            Schema = new OpenApiSchema
            {
                Type = JsonSchemaType.String,
                Enum = [
                    JsonValue.Create("Unhealthy"),
                ]
            }
        };

        operation.Responses.Add("503", unhealthyResponse);
        pathItem.AddOperation(HttpMethod.Get, operation);
        swaggerDoc?.Paths.Add(HealthCheckEndpoint, pathItem);
    }
}