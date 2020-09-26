using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SomeDesignPatterns.StructuralDP
{
    /// <summary>
    /// Pros:
    /// - Single Responsibility Principle (SOLID):
    /// You can separate the interface or data conversion code 
    /// from the primary business logic of the program.
    /// - Open/Closed Principle (SOLID):
    /// You can introduce new types of adapters into the program without breaking the existing client code, 
    /// as long as they work with the adapters through the client interface.
    /// 
    /// Cons:
    /// - The overall complexity of the code increases 
    /// because you need to introduce a set of new interfaces and classes. 
    /// Sometimes it’s simpler just to change the service class 
    /// so that it matches the rest of your code. :D
    /// </summary>

    public class RoundPeg
    {
        private float radius;

        public RoundPeg() { }

        public RoundPeg(float radius)
        {
            this.radius = radius;
        }

        public float GetRadius()
        {
            return radius;
        }
    }

    public class SquarePeg
    {
        private float width;
        public SquarePeg(float width)
        {
            this.width = width;
        }

        public float GetWidth()
        {
            return this.width;
        }
    }

    public class RoundHole
    {
        private float radius;
        public RoundHole(float radius)
        {
            this.radius = radius;
        }

        public float GetRadius()
        {
            return this.radius;
        }

        public bool Fits(RoundPeg peg)
        {
            return this.GetRadius() >= peg.GetRadius();
        }
    }

    public class SquarePegAdapter : RoundPeg
    {
        private SquarePeg peg;

        public SquarePegAdapter(SquarePeg peg) : base()
        {
            this.peg = peg;
        }

        public float GetRadius()
        {
            return (float) (peg.GetWidth() * Math.Sqrt(2) / 2);
        }
    }
    
    public class AdapterExample
    {
        public static void Example1()
        {
            RoundHole hole = new RoundHole(5);
            RoundPeg rPeg = new RoundPeg(5);

            /// Case 1: Ground truth :D
            if (hole.Fits(rPeg))
            {
                Console.WriteLine($"Round peg with radius {rPeg.GetRadius()} fits hole with radius {hole.GetRadius()}");
            }
            else
            {
                Console.WriteLine($"Round peg with radius {rPeg.GetRadius()} does not fit hole with radius {hole.GetRadius()}");
            }

            SquarePeg smallSQPeg = new SquarePeg(5);
            SquarePeg largeSQPeg = new SquarePeg(10);

            /// Case 2: Unmatch type
            //hole.Fits(smallSQPeg); // This won't compile (incompatible types)

            /// Case 3: Adapter
            SquarePegAdapter smallSQPegAdapter = new SquarePegAdapter(smallSQPeg);
            SquarePegAdapter largeSQPegAdapter = new SquarePegAdapter(largeSQPeg);

            Debug.Assert(hole.Fits(smallSQPegAdapter), "Unmatch adapter");
            Console.WriteLine($"Converted square peg with radius {smallSQPegAdapter.GetRadius()} fits hole with radius {hole.GetRadius()}");

            Debug.Assert(hole.Fits(largeSQPegAdapter) != false, "Suddenly match adapter");
            Console.WriteLine($"Converted square peg with radius {largeSQPegAdapter.GetRadius()} does not fit hole with radius {hole.GetRadius()}");

        }
    } 

}
