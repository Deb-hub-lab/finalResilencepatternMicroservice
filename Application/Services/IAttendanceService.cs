using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAttendanceService
    {
        Task<IReadOnlyList<StudentAttendanceDto>> GetAttendanceByMonthAsync(int year, int month, CancellationToken ct = default);
    }
}
