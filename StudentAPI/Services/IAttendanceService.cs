namespace StudentAPI.Services
{
    public interface IAttendanceService
    {
        Task<int> GetMonthlyAttendanceAsync(Guid studentId);
    }
}
