namespace StudentAPI.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly HttpClient _client;

        public AttendanceService(HttpClient client)
        {
            _client = client;
        }

        public async Task<int> GetMonthlyAttendanceAsync(Guid studentId)
        {
            HttpResponseMessage response;   
            //r response = await _client.GetAsync($"student/{studentId}");
            try
            {
                response = await _client.GetAsync($"2015/11");
            }
            catch (Exception ex)
            {

                throw;
            }
             //dentId}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<int>();
        }
    }

}
