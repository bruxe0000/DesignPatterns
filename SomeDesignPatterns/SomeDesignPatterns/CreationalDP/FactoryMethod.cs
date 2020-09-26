using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeDesignPatterns
{
    /// <summary>
    /// Pros:
    /// - You avoid tight coupling between the creator and the concrete products.
    /// - Single Responsibility Principle (SOLID):
    /// You can move the product creation code into one place in the program, 
    /// making the code easier to support.
    /// - Open/Closed Principle (SOLID):
    /// You can introduce new types of products into the program 
    /// without breaking existing client code.
    /// 
    /// Cons:
    /// - The code may become more complicated 
    /// since you need to introduce a lot of new subclasses to implement the pattern. 
    /// The best case scenario is when you’re introducing the pattern 
    /// into an existing hierarchy of creator classes.
    /// </summary>

    public enum DbType
    {
        MSSQL,
        MySQL,
        Oracle
    }

    public abstract class Connection
    {
        public abstract string GetConnectionString();
    }

    public class MSSqlConnection: Connection
    {
        public override string GetConnectionString()
        {
            return "Get MSSql's connection string";
        }
    }

    public class MySqlConnection : Connection
    {
        public override string GetConnectionString()
        {
            return "Get MySql's connection string";
        }
    }

    public class OracleConnection : Connection
    {
        public override string GetConnectionString()
        {
            return "Get Oracle's connection string";
        }
    }

    public class ConnectionFactory
    {
        public static Connection CreateConnection(DbType dbType)
        {
            Connection connection = null;

            switch (dbType)
            {
                case DbType.MSSQL:
                    connection = new MSSqlConnection();
                    break;
                case DbType.MySQL:
                    connection = new MySqlConnection();
                    break;
                case DbType.Oracle:
                    connection = new OracleConnection ();
                    break;
            }

            return connection;
        }
    }
}
