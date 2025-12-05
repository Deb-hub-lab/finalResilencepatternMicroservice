using StudentAPI.Models;

namespace StudentAPI.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        void InvalidateCache();
    }
}
