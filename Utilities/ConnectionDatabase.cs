using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace design_pattern_repository.Utilities
{
    // Design Pattern - Singleton
    // https://github.com/ajunquit/design-pattern-singleton
    public class ConnectionDatabase
    {

        private string Server { get; set; }
        private string Database { get; set; }
        private string UserId { get; set; }
        private string Password { get; set; }
        private int Port { get; set; }

        private static ConnectionDatabase _connectionDatabase;

        private ConnectionDatabase()
        {
            Server = "db4free.net";
            Database = "cosmodb";
            UserId = "usrcosmo";
            Password = "auKn9j5Z_8QHv!X";
            Port = 3306; 
        }

        public string GetConnectionString()
        {
            return $"Server={Server};Port={Port};Database={Database};Uid={UserId};Pwd={Password};";
        }

        public static ConnectionDatabase GetConnectionDatabase()
        {
            if (_connectionDatabase == null)
            {
                _connectionDatabase = new ConnectionDatabase();
            }
            return _connectionDatabase;
        }

    }
}
