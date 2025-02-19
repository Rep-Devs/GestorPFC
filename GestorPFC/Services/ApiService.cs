using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GestorPFC.Services
{
    public class ApiService
    {
        private readonly HttpClient _client = new();

        public async Task<bool> Login(string username, string password)
        {
            var json = JsonSerializer.Serialize(new { Username = username, PasswordHash = password });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("http://localhost:5000/api/auth/login", content);

            return response.IsSuccessStatusCode;
        }
    }
}
