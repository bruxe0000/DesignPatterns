using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SOLID.SOLIDDefinitions
{
    /// <summary>
    /// O — Open/closed principle
    /// In programming, the open/closed principle states that 
    /// software entities (classes, modules, functions, etc.) should be OPEN for EXTENSIONS, but CLOSED for MODIFICATION.
    /// </summary>

    #region Violating implementation
    public class Logger1
    {
        public void Log(string message)
        {
            if (message.Contains("[ERROR]"))
            {
                File.WriteAllText("ErrorMessage.txt", message);
            }
            else
            {
                Debug.WriteLine(message);
            }
        }
    }
    #endregion

    #region Correct implementation

    public interface ILogger
    {
        void Log(string message);
    }

    public class Logger2 : ILogger
    {
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }
    }

    public class ErrorLogger2 : ILogger
    {
        public void Log(string message)
        {
            File.WriteAllText("ErrorMessage.txt", message);
        }
    }
    #endregion
}
