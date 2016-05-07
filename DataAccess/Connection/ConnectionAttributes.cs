using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Connection
{
    public static class ConnectionAttributes
    {
        private static readonly string Provider;
        private static readonly string ConString;

        static ConnectionAttributes()
        {
            var s = ConfigurationManager.ConnectionStrings["DbConnection"];
            Provider = s.ProviderName;
            ConString = s.ConnectionString;
        }

        public static string GetProvider()
        {
            return Provider;
        }

        public static string GetConString()
        {
            return ConString;
        }
    }
}
