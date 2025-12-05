using Microsoft.Extensions.Caching.Memory;
using StudentAPI.Models;
using StudentAPI.Repositories;

namespace StudentAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;
        private readonly IMemoryCache _cache;
        private readonly CacheTokenProvider _tokenProvider;

        public StudentService(IStudentRepository repo, IMemoryCache cache, CacheTokenProvider tokenProvider )
        {
            _repo = repo;
            _cache = cache;
            _tokenProvider = tokenProvider;
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            string cacheKey = "all_students";

            //if (_cache.TryGetValue(cacheKey, out List<Student> cachedStudents))
            //    return cachedStudents;

            if (_cache.TryGetValue(cacheKey, out List<Student>? cachedStudents) && cachedStudents is not null)
                return cachedStudents;

            var students = await _repo.GetAllAsync();

            _cache.Set(cacheKey, students, new MemoryCacheEntryOptions()
                .AddExpirationToken(_tokenProvider.GetToken()));

            return students;
        }


        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            string cacheKey = $"student_{id}";

            //if (_cache.TryGetValue(cacheKey, out Student cachedStudent))
            //    return cachedStudent;

            if (_cache.TryGetValue(cacheKey, out Student? cachedStudent) && cachedStudent is not null)
                return cachedStudent;

            var student = await _repo.GetByIdAsync(id);

            if (student != null)
            {
                _cache.Set(cacheKey, student, new MemoryCacheEntryOptions()
                    .AddExpirationToken(_tokenProvider.GetToken()));
            }

            return student;
        }
        // 🚀 NEW FUNCTION: When student data changes, invalidate cache
        public void InvalidateCache()
        {
            _tokenProvider.Reset(); // Clears all caches connected to the token
        }
    }
}
