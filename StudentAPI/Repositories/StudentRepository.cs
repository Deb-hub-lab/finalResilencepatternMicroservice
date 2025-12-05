using Npgsql;
using StudentAPI.Models;

namespace StudentAPI.Repositories
{
    public class StudentRepository : IStudentRepository
    {

        private readonly string _connectionString;

        public StudentRepository(IConfiguration config)
        {
            //_connectionString = config.GetConnectionString("Postgres");
            _connectionString = config.GetConnectionString("Postgres")
       ?? throw new InvalidOperationException("Postgres connection string not found.");
        
        }

        public async Task<List<Student>> GetAllAsync()
        {
            var students = new List<Student>();

            using var conn = new NpgsqlConnection(_connectionString);
            using var cmd = new NpgsqlCommand("SELECT id, name, grade FROM studentsAgain", conn);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                students.Add(new Student
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    Grade = reader.GetInt32(2)
                });
            }

            return students;
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            using var cmd = new NpgsqlCommand("SELECT id, name,grade FROM studentsAgain WHERE id=@id", conn);

            cmd.Parameters.AddWithValue("@id", id);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Student
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    Grade = reader.GetInt32(2)
                };
            }

            return null;
        }
    }
}
