using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Connection;
using DataAccess.Fabric;

namespace DataAccess.Models
{
    public partial class TaskState : IEntitySqlQueries
    {
        public string Insert()
        {
            return String.Format("INSERT INTO " +
                   " TaskStates (TaskStateId,Name) " +
                   "VALUES(NULL,'{0}')"
                   , Name);
        }

        public string Delete()
        {
            return "DELETE FROM TaskStates WHERE TaskStateId = " + TaskStateId;
        }

        public string Update()
        {
            return String.Format("UPDATE TaskStates SET Name='{1}'" +
                                 " WHERE TaskStateId={0};", TaskStateId, Name);
        }

        public string Select()
        {
            return "SELECT * FROM TaskStates";
        }

        public string Select(string where)
        {
            return "SELECT * FROM TaskStates " + where;
        }
    }
}
