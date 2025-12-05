
using System.Net.Http.Json;

namespace StudentAPI.Services
{
    
    public class GradeService : IGradeService
    {
        private readonly HttpClient _httpClient;

        public GradeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Existing Method (fixed URL issue)
        public async Task<int> GetStudentGrade(int id)
        {
            //string url = _httpClient.BaseAddress.ToString() + Convert.ToString(id);
            string url = $"{_httpClient.BaseAddress?.ToString() ?? string.Empty}{id}";

            return await _httpClient.GetFromJsonAsync<int>(url);
            //return await _httpClient.GetFromJsonAsync<int>($"api/grade/{id}");
        }

        // NEW Method as requested
        public async Task<int> GetGradeAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/grade/{id}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<int>();
        }
    }

}
