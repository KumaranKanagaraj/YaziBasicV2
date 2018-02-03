using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using YaziBasicV2.Models;

namespace YaziBasicV2.Helpers
{
    public class ApiHandler
    {
        public async Task<string> GetData(string baseUrl, string queryUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = await client.GetAsync(queryUrl);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> PostFactData(string baseUrl, string queryUrl, VerityDto verityDto)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = await client.PostAsJsonAsync(queryUrl, verityDto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> UpdateFactData(string baseUrl, string queryUrl, VerityDto verityDto)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = await client.PutAsJsonAsync(queryUrl, verityDto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> PostCategoryData(string baseUrl, string queryUrl, CategoryDto categoryDto)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = await client.PostAsJsonAsync(queryUrl, categoryDto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> UpdateCategoryData(string baseUrl, string queryUrl, CategoryDto categoryDto)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = await client.PutAsJsonAsync(queryUrl, categoryDto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> DeleteData(string baseUrl, string queryUrl, string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var response = await client.DeleteAsync(queryUrl + $@"/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
