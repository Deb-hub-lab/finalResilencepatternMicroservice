using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using Application.Interfaces;

namespace Application.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly ILibraryRepository _repo;
        private readonly ICacheService _cache;

        public LibraryService(ILibraryRepository repo, ICacheService cache)
        {
            _repo = repo;
            _cache = cache;
        }

        public async Task<List<LibraryRecord>> GetBooksIssuedToStudent(string studentName)
        {
            string cacheKey = $"library_{studentName.ToLower()}";

            // First check cache
            var cached = _cache.Get<List<LibraryRecord>>(cacheKey);
            if (cached != null)
                return cached;

            // Call DB
            var data = await _repo.GetBooksIssuedToStudent(studentName);

            // Add to cache for 1 minute
            _cache.Set(cacheKey, data, TimeSpan.FromMinutes(1));

            return data;
        }
    }
}
