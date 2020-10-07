using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.SOLIDDefinitions
{
    /// <summary>
    /// L — Liskov substitution principle
    /// In programming, the Liskov substitution principle states that 
    /// if S is a subtype of T, then objects of type T may be replaced (or substituted) with objects of type S.
    /// </summary>


    #region Violating implemetation
    public interface IBird
    {
        void Fly();
    }

    public class Duck : IBird
    {
        public void Fly()
        {
            Console.WriteLine("Duck is flying...");
        }
    }
    
    public class Ostrich : IBird
    {
        public void Fly()
        {
            Console.WriteLine("Ostrich cannot fly...");
        }
    }
    #endregion

    #region Correct implementation
    public interface IBird2
    {
        float GetWeight();
    }

    public class FlyingBird : IBird2
    {
        public float GetWeight()
        {
            return 1.6f;
        }

        public void Fly()
        {
            Console.WriteLine("Duck is flying...");
        }
    }

    public class Duck2 : FlyingBird
    {

    }

    public class Ostrich2 : IBird2
    {
        public float GetWeight()
        {
            return 30.6f;
        }
    }

    #endregion
}
