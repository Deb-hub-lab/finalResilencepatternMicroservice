using Application.DTOs;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IStudentRepository _repo;
        public AttendanceService(IStudentRepository repo) => _repo = repo;

        public async Task<IReadOnlyList<StudentAttendanceDto>> GetAttendanceByMonthAsync(int year, int month, CancellationToken ct = default)
        {
            var projections = await _repo.GetAttendanceCountByMonthAsync(year, month, ct);
            return projections.Select(p => new StudentAttendanceDto
            {
                Id = p.Id,
                Name = p.Name,
                PresentCount = p.PresentCount
            }).ToList();
        }
    }

}
