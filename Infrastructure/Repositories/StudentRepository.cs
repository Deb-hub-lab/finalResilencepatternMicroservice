using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _db;
        public StudentRepository(AppDbContext db) => _db = db;

        public async Task<Student?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
            await _db.Students.FindAsync(new object[] { id }, ct);

        public async Task<IReadOnlyList<Student>> ListAllAsync(CancellationToken ct = default) =>
            await _db.Students.AsNoTracking().ToListAsync(ct);

        public async Task<IReadOnlyList<StudentAttendanceProjection>> GetAttendanceCountByMonthAsync(int year, int month, CancellationToken ct = default)
        {
            var start = new DateTime(year, month, 1);
            var end = start.AddMonths(1);

            var q = await _db.Students
                .Select(s => new StudentAttendanceProjection(
                    s.Id,
                    s.Name,
                    _db.Attendances
                       .Where(a => a.StudentId == s.Id && a.IsPresent && a.OccurredAt >= start && a.OccurredAt < end)
                       .Count()
                ))
                .AsNoTracking()
                .ToListAsync(ct);

            return q;
        }

    }
}
