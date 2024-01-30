using WeatherApp.Repositories;

namespace WeatherApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
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