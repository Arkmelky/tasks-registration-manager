using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Connection;
using DataAccess.Fabric;

namespace DataAccess.Models
{
    public partial class Person : IEntitySqlQueries
    {
        public string Insert()
        {
            return String.Format("INSERT INTO " +
                   " Persons (FirstName,LastName,MiddleName) " +
                   "VALUES('{0}','{1}','{2}')"
                   , FirstName,LastName,MiddleName);
        }

        public string Delete()
        {
            /*
                   "UPDATE Tasks " +
                   "SET Tasks.PersonId ='NULL' " +
                   "WHERE Tasks.PersonId = " + PersonId +
                   " , "+
             */

            return "DELETE FROM Persons WHERE Persons.PersonId = " + PersonId;
        }

        public string Update()
        {
            return String.Format("UPDATE Persons SET Persons.FirstName = '{1}' , Persons.LastName = '{2}' , Persons.MiddleName = '{3}'" +
                                 " WHERE PersonId={0};", PersonId, FirstName, LastName, MiddleName);
        }

        public string Select()
        {
            return "SELECT * FROM Persons";
        }

        public string Select(string where)
        {
            return "SELECT * FROM Persons " + where;
        }
    }
}
