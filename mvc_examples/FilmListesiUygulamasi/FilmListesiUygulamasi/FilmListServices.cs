using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FilmListesiUygulamasi
{
    public class FilmListesiServisi : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string _apiKey;

        public FilmListesiServisi(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public void SetApiKey(string apiKey)
        {
            _apiKey = apiKey;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await GetFilmListesi();
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }

        private async Task GetFilmListesi()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _apiKey);

                // themoviedb.org API'sine istek at
                var response = await client.GetAsync("https://developers.themoviedb.org/3/getting-started/introduction");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    // API yanıtını Film nesnelerine dönüştür
                    var films = JsonConvert.DeserializeObject<Film[]>(content);

                    // Elde edilen filmleri veritabanına kaydetmek için gerekli işlemleri yap
                    using (var context = new FilmContext())
                    {
                        // Films tablosunu temizle
                        context.Films.RemoveRange(context.Films);

                        // Yeni film listesini ekle
                        context.Films.AddRange(films);

                        // Değişiklikleri veritabanına kaydet
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {
                // Hata durumunda gerekli loglama veya hata işleme işlemlerini gerçekleştir
                // Örneğin: Console.WriteLine(ex.Message);
            }
        }
    }
}
