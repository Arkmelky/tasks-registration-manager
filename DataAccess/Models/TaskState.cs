using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    /// <summary>
    /// Database model "TaskStates"
    /// </summary>
    public partial class TaskState
    {
        public int TaskStateId { get; set; }
        public string Name { get; set; }
    }
}
