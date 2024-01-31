using Microsoft.Extensions.Configuration.AzureKeyVault;
using WeatherApp.Repositories;

namespace WeatherApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.ConfigureAppConfiguration((context, config) =>
            {
                var settings = config.Build();
                var keyVaultURL = settings["KeyVaultConfiguration:KeyVaultURL"];
                var keyVaultClientId = settings["KeyVaultConfiguration:ClientId"];
                var keyVaultClientSecret = settings["KeyVaultConfiguration:ClientSecret"];

                config.AddAzureKeyVault(keyVaultURL, keyVaultClientId, keyVaultClientSecret, new DefaultKeyVaultSecretManager());
            });
            builder.Services.AddMvc();
            
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();
            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");
           
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.Run();
        }
    }
}
