using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FilmListesiUygulamasi
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // API anahtarını burada belirtin
            string apiKey = "bdeb933ae25f5b2cfc00fc48d779214d";

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var filmListesiServisi = services.GetRequiredService<FilmListesiServisi>();
                filmListesiServisi.SetApiKey(apiKey);

                filmListesiServisi.StartAsync(CancellationToken.None).GetAwaiter().GetResult();
            }


            host.Run();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient();
                    services.AddHostedService<FilmListesiServisi>(); // FilmListesiServisi'ni hizmet olarak kaydettir
                    services.AddScoped<FilmListesiServisi>(); // FilmListesiServisi'nin bir örneğini oluştur
                });

    }
}
