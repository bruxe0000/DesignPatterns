using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SomeDesignPatterns.CreationalDP;
using SomeDesignPatterns.BehavioralDP;

namespace SomeDesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            // **** Singleton Example ****
            //SampleSingleton();
            //SampleMultiThreadSingleton();

            // **** Factory Method Example ****
            //SampleFactoryMethod();

            // **** Abstract Method Example ****
            //AbstractFactoryExamples.BuyCar("toyota");
            //AbstractFactoryExamples.BuyCar("porsche");

            // **** Prototype Example ****
            //PrototypeExample.AskingUyen();

            // **** Builder Example ****
            CarBuilderExampler.MakeCar();

            // **** Adapter Example ****
            //AdapterExample.Example1();

            // **** Farcade Example ****
            //FarcadeExample.RestaurantOrder();

            // **** Stategy Example ****
            //StrategyExamples.CookingStrategy();

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
