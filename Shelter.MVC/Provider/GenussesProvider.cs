using Newtonsoft.Json;
using Shelter.Common.Dtos;


namespace Shelter.MVC.Provider
{
    public class GenussesProvider
    {
        HttpClient _httpClient;

        public GenussesProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddGenus(GenusDto dto, string token)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(dto));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var returnedValue = await _httpClient.PostAsync("genus/add", content);

            if (returnedValue.IsSuccessStatusCode)
            {
                await returnedValue.Content.ReadAsStringAsync();
            }

        }

        public async Task UpdateGenus(GenusDto dto, string token)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(dto));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var returnedValue = await _httpClient.PostAsync("genus/update", content);

            if (returnedValue.IsSuccessStatusCode)
            {
                await returnedValue.Content.ReadAsStringAsync();
            }

        }
        public async Task DeleteGenus(GenusDto dto, string token)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(dto));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var returnedValue = await _httpClient.PostAsync("genus/delete", content);

            if (returnedValue.IsSuccessStatusCode)
            {
                await returnedValue.Content.ReadAsStringAsync();
            }

        }

        public async Task<GenusDto> GetGenusById(int id, string token)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(id));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var returnedValue = await _httpClient.GetAsync("genus/GetById");

            if (returnedValue.IsSuccessStatusCode)
            {
                var result = await returnedValue.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<GenusDto>(result);
                return data;
            }
            return null;
        }

        public async Task<List<GenusDto>> GetAll(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var value = await _httpClient.GetAsync("Genus");
            if (value.IsSuccessStatusCode)
            {

                var content = await value.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<GenusDto>>(content);
                return data;
            }
            return null;
        }
    }
}
