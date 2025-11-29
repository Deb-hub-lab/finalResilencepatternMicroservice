using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Attendance
    {
        public Guid Id { get; set; }          // public setter needed for HasData
        public Guid StudentId { get; set; }   // public setter needed for HasData
        public DateTime OccurredAt { get; set; }
        public bool IsPresent { get; set; }

        // Parameterless constructor for EF
        public Attendance() { }

        // Constructor for normal domain use
        public Attendance(Guid studentId, DateTime occurredAt, bool isPresent)
        {
            Id = Guid.NewGuid();
            StudentId = studentId;
            OccurredAt = occurredAt.Date;
            IsPresent = isPresent;
        }

        // Constructor for seeding
        public Attendance(Guid id, Guid studentId, DateTime occurredAt, bool isPresent)
        {
            Id = id;
            StudentId = studentId;
            OccurredAt = occurredAt.Date;
            IsPresent = isPresent;
        }
    }

}
