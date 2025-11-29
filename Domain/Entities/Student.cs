using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Student
    {
        public Guid Id { get; set; }      // public setter for HasData
        public string Name { get; set; } = null!;

        private readonly List<Attendance> _attendances = new();
        public IReadOnlyCollection<Attendance> Attendances => _attendances.AsReadOnly();

        public Student() { } // EF parameterless

        public Student(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Student(Guid id, string name)   // constructor for seeding
        {
            Id = id;
            Name = name;
        }

        public void AddAttendance(DateTime occurredAt, bool isPresent = true)
        {
            _attendances.Add(new Attendance(this.Id, occurredAt, isPresent));
        }
    }

}
