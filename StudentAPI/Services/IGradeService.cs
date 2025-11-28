namespace StudentAPI.Services
{
    public interface IGradeService
    {
        Task<int> GetStudentGrade(int id);
    }
}
