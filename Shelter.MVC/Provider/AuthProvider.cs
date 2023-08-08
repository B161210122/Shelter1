using Newtonsoft.Json;
using Shelter.Common.Dtos;
using Shelter.Common.JWT;

namespace Shelter.MVC.Provider
{
    public class AuthProvider
    {
        HttpClient _httpClient;

        public AuthProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<LoggedDto> Login(UserForLoginDto userForLoginDto)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(userForLoginDto));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var returnedValue = await _httpClient.PostAsync("auth/login", content);

            if (returnedValue.IsSuccessStatusCode)
            {
                var value = await returnedValue.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LoggedDto>(value);

            }

            return null;
        }

        public async Task<AccessToken> RefreshToken()
        {
            var returnedValue = await _httpClient.GetAsync("auth/RefreshToken");

            if (returnedValue.IsSuccessStatusCode)
            {
                var value = await returnedValue.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AccessToken>(value);

            }

            return null;
        }

        public async Task RevokeToken()
        {
            var returnedValue = await _httpClient.GetAsync("auth/RevokeToken");

            if (returnedValue.IsSuccessStatusCode)
            {
                var value = await returnedValue.Content.ReadAsStringAsync();

            }

        }

        public async Task<RegisteredDto> Register(UserForRegisterDto dto)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(dto));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var returnedValue = await _httpClient.PostAsync("auth/register", content);

            if (returnedValue.IsSuccessStatusCode)
            {
                var value = await returnedValue.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<RegisteredDto>(value);

            }

            return null;
        }
    }
}
