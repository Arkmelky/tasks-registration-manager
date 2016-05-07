using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Connection;

namespace DataAccess.Models
{
    /// <summary>
    /// Database model "Persons"
    /// </summary>
    public partial class Person 
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
    }

    
}
