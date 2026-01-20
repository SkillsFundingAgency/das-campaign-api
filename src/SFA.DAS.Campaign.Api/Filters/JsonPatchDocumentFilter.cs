using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;

namespace SFA.DAS.Campaign.Api.Filters;

[ExcludeFromCodeCoverage]
public class JsonPatchDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        if (swaggerDoc?.Components?.Schemas == null)
        {
            return;
        }

        var patchOperation = swaggerDoc.Components.Schemas
            .FirstOrDefault(s => s.Key.Equals("operation", StringComparison.OrdinalIgnoreCase));

        if (patchOperation.Key != null && patchOperation.Value?.Properties != null)
            patchOperation.Value.Properties.Remove("operationType");
    }
}