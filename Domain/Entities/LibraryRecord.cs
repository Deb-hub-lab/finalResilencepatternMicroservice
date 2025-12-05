using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LibraryRecord
    {
        public int LibraryId { get; set; }
        public string BookName { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
    }
}
