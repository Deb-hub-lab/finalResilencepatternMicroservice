using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<Student>> ListAllAsync(CancellationToken ct = default);
        Task<IReadOnlyList<StudentAttendanceProjection>> GetAttendanceCountByMonthAsync(int year, int month, CancellationToken ct = default);
        // projection type to avoid loading heavy entities
    }

    public record StudentAttendanceProjection(Guid Id, string Name, int PresentCount);
}
