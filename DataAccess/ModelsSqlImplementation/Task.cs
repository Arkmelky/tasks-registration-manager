using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Connection;
using DataAccess.Fabric;

namespace DataAccess.Models
{
    public partial class Task : IEntitySqlQueries
    {
        public string Insert()
        {
            return String.Format("INSERT INTO " +
                   " Tasks (Name,Workload,StartDate,EndDate,TaskStateId,PersonId) " +
                   "VALUES('{0}','{1}','{2}','{3}','{4}','{5}')"
                   ,Name,Workload,StartDate,EndDate,TaskStateId,PersonId);
        }

        public string Delete()
        {
            return "DELETE FROM Tasks WHERE TaskId = " + TaskId;
        }

        public string Update()
        {
            return String.Format("UPDATE Tasks SET Name='{1}',Workload='{2}',StartDate='{3}',EndDate='{4}',TaskStateId='{5}',PersonId='{6}'" +
                                 " WHERE TaskId={0};", TaskId, Name, Workload, StartDate, EndDate, TaskStateId, PersonId);
        }

        public string Select()
        {
            return "SELECT * FROM Tasks";
        }

        public string Select(string where)
        {
            return "SELECT * FROM Tasks " + where;
        }
    }
}
