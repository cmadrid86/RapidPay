using RapidPayApi.Dto;

namespace RapidPayApi.Extensions;

public static class OptionsExtensions
{
    public static void ConfigureOptions(this WebApplicationBuilder builder)
    {
        var settingsSection = builder.Configuration.GetSection("Settings");
        var settings = settingsSection.Get<Settings>();

        builder.Services.Configure<Settings>(options =>
        {
            if (settings == null)
            {
                throw new ArgumentException("Missing settings");
            }
            if (string.IsNullOrWhiteSpace(settings.Username) || string.IsNullOrWhiteSpace(settings.Password))
            {
                throw new ArgumentException("Missing basic auth settings");
            }

            settingsSection.Bind(options);
        });
    }
}