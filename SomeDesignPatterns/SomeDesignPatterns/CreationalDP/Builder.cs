using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeDesignPatterns.CreationalDP
{
    /// <summary>
    /// Pros:
    /// - You can construct objects step-by-step, 
    /// defer construction steps or run steps recursively.
    /// - You can reuse the same construction code 
    /// when building various representations of products.
    /// - Single Responsibility Principle (SOLID). 
    /// You can isolate complex construction code 
    /// from the business logic of the product.
    /// 
    /// Cons:
    /// - The overall complexity of the code increases 
    /// since the pattern requires creating multiple new classes.
    /// </summary>

    /// <summary>
    /// Using the Builder pattern makes sense only when your products
    /// are quite complex and require extensive configuration. 
    /// The following two products are related,
    /// although they don't have a common interface.
    /// </summary>
    public class Car2
    {
        // A car can have a GPS, trip computer and some number of
        // seats. Different models of cars (sports car, SUV,
        // cabriolet) might have different features installed or
        // enabled.
    }

    public class Manual
    {
        // Each car should have a USER MANUAL that corresponds to
        // the car's configuration and describes all its features.
    }

    /// <summary>
    /// The builder interface specifies methods for creating the
    /// different parts of the product objects.
    /// </summary>
    public interface IBuilder
    {
        void Reset();
        void SetSeats(int number);
        void SetEngine();
        void SetTripComputer(bool isON);
        void SetGPS(bool isON);
    }

    public class CarBuilder: IBuilder
    {
        private Car2 car;

        public CarBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            this.car = new Car2();
        }

        public void SetSeats(int number)
        {
            Console.WriteLine($"Install: {number} seat(s).");
        }

        public void SetEngine()
        {
            Console.WriteLine("Install BMW Diesel Engine.");
        }

        public void SetTripComputer(bool isON)
        {
            Console.WriteLine($"Install Trip computer: {isON}");
        }

        public void SetGPS(bool isON)
        {
            Console.WriteLine($"Install GPS: {isON}");
        }

        public Car2 getProduct()
        {
            Car2 product = this.car;
            this.Reset();
            return product;
        }
    }

    public class CarManualBuilder: IBuilder
    {
        private Manual manual;

        public CarManualBuilder()
        {
            this.manual = new Manual();
        }

        public void Reset()
        {
            this.manual = new Manual();
        }

        public void SetSeats(int number)
        {
            Console.WriteLine($"The car has {number} seat(s).");
        }

        public void SetEngine()
        {
            Console.WriteLine("The car has BMW Diesel Engine.");
        }

        public void SetTripComputer(bool isON)
        {
            if (isON)
            {
                Console.WriteLine($"The car has Trip computer.");
            }
            else
            {
                Console.WriteLine($"The car does not have Trip computer.");
            }
        }

        public void SetGPS(bool isON)
        {
            if (isON)
            {
                Console.WriteLine($"The car has GPS.");
            }
            else
            {
                Console.WriteLine($"The car does not have GPS.");
            }
        }

        public Manual getProduct()
        {
            Manual product = this.manual;
            this.Reset();
            return product;
        }
    }

    public class Director
    {
        private IBuilder builder;
        
        public void ConstructSportsCar(IBuilder builder)
        {
            builder.Reset();
            builder.SetSeats(2);
            builder.SetEngine();
            builder.SetTripComputer(false);
            builder.SetGPS(true);
        }
    }

    public class CarBuilderExampler
    {
        public static void MakeCar()
        {
            Director director = new Director();

            CarBuilder builder = new CarBuilder();
            director.ConstructSportsCar(builder);
            Car2 car = builder.getProduct();

            CarManualBuilder builder2 = new CarManualBuilder();
            director.ConstructSportsCar(builder2);

            Manual manual = builder2.getProduct();
        }
    }
}
