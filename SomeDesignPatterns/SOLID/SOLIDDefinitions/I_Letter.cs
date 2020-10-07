using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.SOLIDDefinitions
{
    /// <summary>
    /// I — Interface segregation principle
    /// In programming, the interface segregation principle states that 
    /// no client should be forced to depend on methods it does not use.
    /// 
    /// Put more simply: Do not add additional functionality 
    /// to an existing interface by adding new methods.
    /// </summary>
    public interface IBird3
    {
        void Run();
    }

    public interface IBird4
    {
        void Swim();
    }
}
