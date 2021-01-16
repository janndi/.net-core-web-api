using Microsoft.OpenApi.Models;

namespace Infrastructure.ThirdpartyService.Swagger
{
    public class SwaggerOptions : OpenApiInfo
    {
        public string RoutePrefix { get; set; } = "";
    }
}
