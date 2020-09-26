using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeDesignPatterns.CreationalDP
{
    /// <summary>
    /// Pros:
    /// - You can be sure that a class has only a single instance.
    /// - You gain a global access point to that instance.
    /// - The singleton object is initialized only when it’s requested for the first time.
    /// 
    /// Cons:
    /// - Violates the Single Responsibility Principle. 
    /// The pattern solves two problems at the time.
    /// - The Singleton pattern can mask bad design, 
    /// for instance, when the components of the program know too much about each other.
    /// - The pattern requires special treatment in a multithreaded environment 
    /// so that multiple threads won’t create a singleton object several times.
    /// - It may be difficult to unit test the client code of the Singleton 
    /// because many test frameworks rely on inheritance when producing mock objects. 
    /// Since the constructor of the singleton class is private and overriding static methods is impossible in most languages, 
    /// you will need to think of a creative way to mock the singleton. 
    /// Or just don’t write the tests. Or don’t use the Singleton pattern. :D 
    /// </summary>

    public interface ISingleton { }

    public class LogFileSingleton : ISingleton
    {
        // Lazy instance
        private static LogFileSingleton _logFileSingleton;
        private static object _lockObject = new object();

        protected LogFileSingleton()
        {            
        }

        public static LogFileSingleton LazyGetInstance()
        {
            if (_logFileSingleton == null)
            {
                return new LogFileSingleton();
            }
            return _logFileSingleton;
        }

        public static LogFileSingleton ThreadSafeGetInstance()
        {
            lock (_lockObject)
            {
                if (_logFileSingleton == null)
                {
                    return new LogFileSingleton();
                }
                return _logFileSingleton; 
            }
        }

        public void LogContent(string content)
        {
            Console.WriteLine($"Logging: {content}");
        }
    }
}
