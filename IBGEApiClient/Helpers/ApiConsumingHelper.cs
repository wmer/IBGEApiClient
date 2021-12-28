using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IBGEApiClient.Helpers {
    public class ApiConsumingHelper {
        private HttpClient _client;

        public ApiConsumingHelper(string baseAdress) {
            _client = new HttpClient() {
                BaseAddress = new Uri(baseAdress)
            };
            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(mediaType);
        }

        public (T result, string statusCode, string message) Get<T>(string endPoint) {
            return GetAssync<T>(endPoint).Result;
        }

        public async Task<(T result, string statusCode, string message)> GetAssync<T>(string endPoint) {
            var response = await _client.GetAsync($"{_client.BaseAddress}{endPoint}");
            return DeserializeResponse<T>(response);
        }

        private (T result, string statusCode, string message) DeserializeResponse<T>(HttpResponseMessage response) {
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var statusCode = response.StatusCode.ToString();
            if (response.IsSuccessStatusCode) {
                return (JsonConvert.DeserializeObject<T>(responseContent), statusCode, responseContent);
            } else {
                return (default(T), statusCode, responseContent);
            }
        }
    }
}
