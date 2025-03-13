using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RapidPayApi.Engines;
using RapidPayApi.Exceptions;
using RapidPayApi.Managers;
using RapidPayApi.Repositories;

namespace RapidPayApi.Extensions;

public static class ServicesExtensions
{
    public static void ConfigureDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<RapidPayDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
    }

    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddProblemDetails(options =>
        {
            options.IncludeExceptionDetails = (_, _) => false;
            options.Map<CustomException>(ex => ex.ToProblemDetails());
        });

        // Services depending on DB Context
        builder.Services.AddScoped<ICardEngine, CardEngine>();
        builder.Services.AddScoped<ICardManagementManager, CardManagementManager>();
        builder.Services.AddScoped<IPaymentEngine, PaymentEngine>();

        // Singleton services
        builder.Services.AddSingleton<IAuthManager, AuthManager>();
        builder.Services.AddSingleton<ICardRepository, CardRepository>();
        builder.Services.AddSingleton<ITokenEngine, TokenEngine>();
        builder.Services.AddSingleton<ITransactionRepository, TransactionRepository>();
        builder.Services.AddSingleton<IUniversalFeesExchangeEngine, UniversalFeesExchangeEngine>();
    }

    private static ProblemDetails ToProblemDetails(this CustomException e) =>
        new()
        {
            Detail = e.Message,
            Status = e.StatusCode,
            Title = e.Title,
            Type = $"https://httpstatuses.com/{e.StatusCode}"
        };
}