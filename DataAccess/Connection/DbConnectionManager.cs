using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DataAccess.Models;

namespace DataAccess.Connection
{
    public interface IDbConnectionManager
    {
        void Initialaize();
        DbConnection GetConnection();
        DbCommand CreateCommand();
        void OpenConnection();
        void CloseConnection();
    }

    public class DbConnectionManager:IDbConnectionManager
    {
        private DbProviderFactory _dbProviderFactory;

        private DbConnection _connection;
        private DbCommand _command;
        
        public DbConnectionManager()
        {
        }

        public void Initialaize()
        {
            _dbProviderFactory = DbProviderFactories.GetFactory(ConnectionAttributes.GetProvider());
        }

        public DbProviderFactory GetFactory()
        {
            return _dbProviderFactory;
        }

        public DbConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = _dbProviderFactory.CreateConnection();
                _connection.ConnectionString = ConnectionAttributes.GetConString();
            }
            return _connection;
        }

        public DbCommand CreateCommand()
        {
            if (_command == null)
            {
                _command = _dbProviderFactory.CreateCommand();
                _command.Connection = _connection ?? GetConnection();
            }
            return _command;

        }

        public void OpenConnection()
        {
            _connection.Open();
        }

        public void CloseConnection()
        {
            _connection.Close();
        }

    }
}
