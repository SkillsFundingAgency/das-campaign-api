using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;

namespace SFA.DAS.Campaign.Api.Filters;

[ExcludeFromCodeCoverage]
public class HealthChecksFilter : IDocumentFilter
{
    private const string HealthCheckEndpoint = "/health";
    private const string ServiceStatusTag = "Service Status";

    public void Apply(
        OpenApiDocument swaggerDoc,
        DocumentFilterContext context)
    {
        swaggerDoc.Tags ??= new HashSet<OpenApiTag>();

        if (!swaggerDoc.Tags.Any(tag => tag.Name == ServiceStatusTag))
        {
            swaggerDoc.Tags.Add(new OpenApiTag
            {
                Name = ServiceStatusTag
            });
        }

        var operation = new OpenApiOperation
        {
            Tags = new HashSet<OpenApiTagReference>
            {
                new OpenApiTagReference(ServiceStatusTag, swaggerDoc)
            },
            Responses = new OpenApiResponses
            {
                ["200"] = new OpenApiResponse
                {
                    Description = "The service is healthy.",
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["text/plain"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = JsonSchemaType.String,
                                Enum = new List<JsonNode>
                                {
                                    JsonValue.Create("Healthy")!
                                }
                            }
                        }
                    }
                },
                ["503"] = new OpenApiResponse
                {
                    Description = "The service is unhealthy.",
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["text/plain"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = JsonSchemaType.String,
                                Enum = new List<JsonNode>
                                {
                                    JsonValue.Create("Unhealthy")!
                                }
                            }
                        }
                    }
                }
            }
        };

        var pathItem = new OpenApiPathItem();
        pathItem.AddOperation(HttpMethod.Get, operation);

        swaggerDoc.Paths ??= new OpenApiPaths();
        swaggerDoc.Paths[HealthCheckEndpoint] = pathItem;
    } 
}