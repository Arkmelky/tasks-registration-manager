using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    /// <summary>
    /// Database model "Tasks"
    /// </summary>
    public partial class Task
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Workload { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TaskStateId { get; set; }
        public int PersonId { get; set; }
    }
}
