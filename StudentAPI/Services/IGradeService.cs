namespace StudentAPI.Services
{
    public interface IGradeService
    {
        Task<int> GetStudentGrade(int id);
        Task<int> GetGradeAsync(int id);   // NEW METHOD
    }
}
