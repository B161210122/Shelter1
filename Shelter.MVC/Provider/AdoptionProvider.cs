using Newtonsoft.Json;
using Shelter.Common.Dtos;

namespace Shelter.MVC.Provider
{
    public class AdoptionProvider
    {
        HttpClient _httpClient;

        public AdoptionProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task AddAdoption(AdoptionDto dto, string token)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(dto));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var returnedValue = await _httpClient.PostAsync("adoption/add", content);

            if (returnedValue.IsSuccessStatusCode)
            {
                await returnedValue.Content.ReadAsStringAsync();
            }

        }

        public async Task UpdateAdoption(AdoptionDto dto, string token)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(dto));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var returnedValue = await _httpClient.PostAsync("adoption/update", content);

            if (returnedValue.IsSuccessStatusCode)
            {
                await returnedValue.Content.ReadAsStringAsync();
            }

        }
        public async Task DeleteAdoption(AdoptionDto dto, string token)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(dto));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var returnedValue = await _httpClient.PostAsync("adoption/delete", content);

            if (returnedValue.IsSuccessStatusCode)
            {
                await returnedValue.Content.ReadAsStringAsync();
            }

        }

        public async Task<AdoptionDto> GetAdoptionById(int id, string token)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(id));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var returnedValue = await _httpClient.GetAsync("adoption/GetById");

            if (returnedValue.IsSuccessStatusCode)
            {
                var result = await returnedValue.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<AdoptionDto>(result);
                return data;
            }
            return null;
        }

        public async Task<List<AdoptionDto>> GetAll(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var value = await _httpClient.GetAsync("adoption");
            if (value.IsSuccessStatusCode)
            {

                var content = await value.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<AdoptionDto>>(content);
                return data;
            }
            return null;
        }
    }
}
