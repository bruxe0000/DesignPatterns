using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeDesignPatterns.BehavioralDP
{
    /// <summary>
    /// Pros:
    /// - You can swap algorithms used inside an object at runtime.
    /// - You can isolate the implementation details of an algorithm from the code that uses it.
    /// - You can replace inheritance with composition.
    /// - Open/Closed Principle (SOLID). You can introduce new strategies without having to change the context.
    /// 
    /// Cons:
    /// - If you only have a couple of algorithms and they rarely change, 
    /// there’s no real reason to overcomplicate the program with new classes 
    /// and interfaces that come along with the pattern.
    /// - Clients must be aware of the differences between strategies to be able to select a proper one.
    /// - A lot of modern programming languages have functional type support 
    /// that lets you implement different versions of an algorithm inside a set of anonymous functions. 
    /// Then you could use these functions exactly as you’d have used the strategy objects, 
    /// but without bloating your code with extra classes and interfaces.
    /// </summary>

    /// <summary>
    /// The Strategy abstract class, which defines an interface common to all supported strategy algorithms.
    /// </summary>
    abstract class CookStrategy
    {
        public abstract void Cook(string food);
    }

    /// <summary>
    /// A Concrete Strategy class
    /// </summary>
    class Grilling : CookStrategy
    {
        public override void Cook(string food)
        {
            Console.WriteLine("\nCooking " + food + " by grilling it.");
        }
    }

    /// <summary>
    /// A Concrete Strategy class
    /// </summary>
    class OvenBaking : CookStrategy
    {
        public override void Cook(string food)
        {
            Console.WriteLine("\nCooking " + food + " by oven baking it.");
        }
    }

    /// <summary>
    /// A Concrete Strategy class
    /// </summary>
    class DeepFrying : CookStrategy
    {
        public override void Cook(string food)
        {
            Console.WriteLine("\nCooking " + food + " by deep frying it");
        }
    }

    /// <summary>
    /// The Context class, which maintains a reference to the chosen Strategy.
    /// </summary>
    class CookingMethod
    {
        private string Food;
        private CookStrategy _cookStrategy;

        public void SetCookStrategy(CookStrategy cookStrategy)
        {
            this._cookStrategy = cookStrategy;
        }

        public void SetFood(string name)
        {
            Food = name;
        }

        public void Cook()
        {
            _cookStrategy.Cook(Food);
            Console.WriteLine();
        }
    }

    public class StrategyExamples
    {
        /// <summary>
        /// The Strategy pattern is a method by which we can move repeated if/then logic to polymorphism.
        /// In this example, we want to cook some food using a variety of different methods.
        /// In a naive example, we'd need an IF statement for each cooking method.
        /// If we have a lot of cooking methods, we'll also have a lot of IF statements.
        /// But in this example, we can define a Strategy for each cook method and simply set which one we want at runtime.
        /// </summary>
        public static void CookingStrategy()
        {
            CookingMethod cookMethod = new CookingMethod();

            Console.WriteLine("What food would you like to cook?");
            var food = Console.ReadLine();
            cookMethod.SetFood(food);

            Console.WriteLine("What cooking strategy would you like to use (1-3)?");
            int input = int.Parse(Console.ReadKey().KeyChar.ToString());


            switch (input)
            {
                case 1:
                    cookMethod.SetCookStrategy(new Grilling());
                    cookMethod.Cook();
                    break;

                case 2:
                    cookMethod.SetCookStrategy(new OvenBaking());
                    cookMethod.Cook();
                    break;

                case 3:
                    cookMethod.SetCookStrategy(new DeepFrying());
                    cookMethod.Cook();
                    break;

                default:
                    Console.WriteLine("Invalid Selection!");
                    break;
            }
        }
    }
}
