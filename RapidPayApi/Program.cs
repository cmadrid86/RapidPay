using Hellang.Middleware.ProblemDetails;
using RapidPayApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.ConfigureSwagger();
builder.ConfigureMvcOptions();
builder.ConfigureServices();
builder.ConfigureDbContext();
builder.ConfigureOptions();
builder.ConfigureAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerInDev();
app.UseProblemDetails();
app.UseRouting();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.Run();