using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.SOLIDDefinitions
{
    /// <summary>
    /// S — Single responsibility principle
    /// In programming, the Single Responsibility Principle states that 
    /// every module or class should have responsibility over a single part of the functionality provided by the software.
    /// </summary>

    #region Violating implementation
    public class FileHandler1
    {
        public void Delete(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occured: ", ex.ToString());
                File.WriteAllText("LocalErrors.txt", ex.ToString());
            }
        }
    }
    #endregion

    #region Correct implementation
    public class FileHandler2
    {
        private ErrorLogger errorLogger = new ErrorLogger();

        public void Delete(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                errorLogger.log(ex.ToString());
            }
        }
    }

    public class ErrorLogger
    {
        public void log(string error)
        {
            Debug.WriteLine("An error occured: ", error);
            File.WriteAllText("LocalErrors.txt", error);
        }
    }
    #endregion
}
