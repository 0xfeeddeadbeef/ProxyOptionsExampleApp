namespace ProxyOptionsExampleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class MyExampleHttpClient
    {
        private HttpClient _httpClient;

        public MyExampleHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Example
        public async Task<string> CallTheService()
        {
            return await _httpClient.GetStringAsync("/api/example");
        }
    }
}
