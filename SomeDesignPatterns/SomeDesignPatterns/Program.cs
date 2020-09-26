using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SomeDesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            // **** Singleton Sample ****
            //SampleSingleton();
            //SampleMultiThreadSingleton();

            // **** Factory Method Sample ****
            SampleFactoryMethod();

            Console.ReadKey();
        }

        static void SampleSingleton()
        {
            LogFileSingleton _sampleLog = LogFileSingleton.LazyGetInstance();
            _sampleLog.LogContent("Sample Log");
        }

        static void SampleMultiThreadSingleton()
        {
            LogFileSingleton _sampleLog = LogFileSingleton.ThreadSafeGetInstance();
            Thread threadA = new Thread(() => _sampleLog.LogContent("Log Content A"));
            Thread threadB = new Thread(() => _sampleLog.LogContent("Log Content B"));

            threadA.Start();
            threadB.Start();
        }

        static void SampleFactoryMethod()
        {
            Connection connection = ConnectionFactory.CreateConnection(DbType.Oracle);
            string connectionString = connection.GetConnectionString();
            Console.WriteLine(connectionString);
        }

    }
}
