using System.Text.Json;
using System.Text.Json.Serialization;
using Hellang.Middleware.ProblemDetails.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace RapidPayApi.Extensions;

public static class MvcConfigurationExtensions
{
    public static void ConfigureMvcOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });

        builder.Services.AddControllers(config =>
        {
            config.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
            config.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
            config.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status422UnprocessableEntity));
            config.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));

            config.ReturnHttpNotAcceptable = true;

            config.OutputFormatters
                .OfType<SystemTextJsonOutputFormatter>()
                .FirstOrDefault()?
                .SupportedMediaTypes
                .Remove("text/json");
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        })
        .AddProblemDetailsConventions();
    }
}