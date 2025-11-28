
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

        public async Task<int> GetStudentGrade(int id)
        {
            string url = _httpClient.BaseAddress.ToString()+ Convert.ToString(id);
            return await _httpClient.GetFromJsonAsync<int>(url);
        }
    }
}
