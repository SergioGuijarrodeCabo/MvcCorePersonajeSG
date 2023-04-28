using MvcCorePersonajeSG.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcCorePersonajeSG.Services
{
    public class ServicePersonajes
    {
        private MediaTypeWithQualityHeaderValue Header;
        private string UrlApi;

        public ServicePersonajes(IConfiguration configuration)
        {
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = configuration.GetValue<string>
                ("ApiUrls:ApiPersonajes");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "/api/personajes";
            List<Personaje> personajes =
                await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }


        public async Task<Personaje> FindPersonajeAsync(int id)
        {
            string request = "/api/personajes/" + id;
            Personaje personaje =
                await this.CallApiAsync<Personaje>(request);
            return personaje;
        }

        public async Task DeletePersnajeAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/personajes/" + id;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
        
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            
            }
        }


        public async Task InsertPersonajesAsync
           (int IdPersonaje, string NombrePersonaje, string Imagen, int IdSerie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/personajes";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                Personaje personaje = new Personaje();
                personaje.IdPersonaje = IdPersonaje;
                personaje.NombrePersonaje = NombrePersonaje;
                personaje.Imagen = Imagen;
                personaje.IdSerie = IdSerie;

                string json = JsonConvert.SerializeObject(personaje);

                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }

        public async Task UpdatePersonajeAsync
           (int IdPersonaje, string NombrePersonaje, string Imagen, int IdSerie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/personajes";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje personaje =
                    new Personaje
                    {
                        IdPersonaje = IdPersonaje,
                        NombrePersonaje = NombrePersonaje,
                        Imagen = Imagen,
                        IdSerie = IdSerie
                    };
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent
                    (json, Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);
            }
        }

    }
}
