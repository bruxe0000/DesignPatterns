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
    /// D - Dependency inversion principle
    /// In programming, the dependency inversion principle is a way to decouple software modules.
    /// This principle states that:
    ///   - High-level modules should not depend on low-level modules.Both should depend on abstractions.
    ///   - Abstractions should not depend on details. Details should depend on abstractions.
    /// </summary>

    #region Violating implementation
    public class FileHandler3
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
    public class FileHandler4
    {
        private ILogger errorLogger;

        public void GetLogger(ILogger injectedLogger)
        {
            errorLogger = injectedLogger;
        }

        public void Delete(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                errorLogger.Log(ex.ToString());
            }
        }
    }
    #endregion
}
