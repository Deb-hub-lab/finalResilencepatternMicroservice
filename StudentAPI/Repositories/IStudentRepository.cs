using StudentAPI.Models;

namespace StudentAPI.Repositories
{
    public interface IStudentRepository
    {

        Task<List<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
    }
}
