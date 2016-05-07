using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Models;

namespace TasksRegistrationManager.Models
{
    public class TaskView : Task
    {
        public TaskState State { get; set; }
        public Person Person { get; set; }
    }
}