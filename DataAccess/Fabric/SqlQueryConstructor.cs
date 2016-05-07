using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Fabric
{
    /// <summary>
    /// Types of queries
    /// </summary>
    public enum EntityQueryType
    {
        Insert,
        Delete,
        Update,
        Select

    }

    /// <summary>
    /// This functions implemets by db models 
    /// </summary>
    public interface IEntitySqlQueries
    {
        string Insert();
        string Delete();
        string Update();
        string Select();
        string Select(string where);

    }

    /// <summary>
    /// Fabric method, returns sql query as string
    /// for db models
    /// </summary>
    public static class SqlQueryBuilder
    {
        public static string PrepareSqlQuery(EntityQueryType type, IEntitySqlQueries entity, string where)
        {
            string sqlQuery = "";

            switch (type)
            {
                case EntityQueryType.Select:
                    if (!String.IsNullOrEmpty(where))
                    {
                        sqlQuery = entity.Select(where);
                    }
                    else
                    {
                        sqlQuery = entity.Select();
                    }

                    break;
                case EntityQueryType.Insert:
                    sqlQuery = entity.Insert();
                    break;
                case EntityQueryType.Update:
                    sqlQuery = entity.Update();
                    break;
                case EntityQueryType.Delete:
                    sqlQuery = entity.Delete();
                    break;
                default:
                    return null;
            }

            return sqlQuery;

        }
    }

}
