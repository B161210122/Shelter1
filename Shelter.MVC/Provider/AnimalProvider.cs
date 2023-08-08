using Newtonsoft.Json;
using Shelter.Common.Dtos;

namespace Shelter.MVC.Provider
{
    public class AnimalProvider
    {
        HttpClient _httpClient;

        public AnimalProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddAnimal(AnimalDto dto, string token)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(dto));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var returnedValue = await _httpClient.PostAsync("animal/add", content);

            if (returnedValue.IsSuccessStatusCode)
            {
                await returnedValue.Content.ReadAsStringAsync();
            }

        }

        public async Task UpdateAnimal(AnimalDto dto, string token)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(dto));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var returnedValue = await _httpClient.PostAsync("animal/update", content);

            if (returnedValue.IsSuccessStatusCode)
            {
                await returnedValue.Content.ReadAsStringAsync();
            }

        }
        public async Task DeleteAnimal(AnimalDto dto, string token)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(dto));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var returnedValue = await _httpClient.PostAsync("animal/delete", content);

            if (returnedValue.IsSuccessStatusCode)
            {
                await returnedValue.Content.ReadAsStringAsync();
            }

        }

        public async Task<AnimalDto> GetAnimalById(int id, string token)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(id));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var returnedValue = await _httpClient.GetAsync("animal/GetById");

            if (returnedValue.IsSuccessStatusCode)
            {
                var result = await returnedValue.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<AnimalDto>(result);
                return data;
            }
            return null;
        }

        public async Task<List<AnimalDto>> GetAnimals(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var returnedValue = await _httpClient.GetAsync("animal");

            if (returnedValue.IsSuccessStatusCode)
            {
                var result = await returnedValue.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<AnimalDto>>(result);
                return data;
            }
            return null;
        }
    }
}
