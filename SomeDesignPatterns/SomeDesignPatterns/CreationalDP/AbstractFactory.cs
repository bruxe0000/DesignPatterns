using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeDesignPatterns.CreationalDP
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICar
    {
        string Branch { get; set; }
        string Model { get; set; }
    }

    public abstract class CarFactoryBase
    {
        protected Car car;

        public abstract void ProduceCar();

        public string GetCarModel()
        {
            return car?.Model;
        }

        public string GetCarBranch()
        {
            return car?.Branch;
        }
    }

    public class Car : ICar
    {
        public string Branch { get; set; }
        public string Model { get; set; }

        public Car(string branch, string carModel)
        {
            Branch = branch;
            Model = carModel;
        }
    }

    public class PorscheFactory : CarFactoryBase
    {
        public override void ProduceCar()
        {
            car = new Car("Porsche", "Macan");
        }
    }

    public class ToyotaFactory : CarFactoryBase
    {
        public override void ProduceCar()
        {
            car = new Car("Toyota", "Camry");
        }
    }

    public class Store
    {
        private CarFactoryBase factory;
        public Store(CarFactoryBase factory)
        {
            this.factory = factory;
        }

        public void Sell()
        {
            factory.ProduceCar();
            Console.WriteLine($"Sell car branch: {factory.GetCarBranch()}");
            Console.WriteLine($"Sell car model: {factory.GetCarModel()}");
        }
    }

    public class AbstractFactoryExamples
    {
        public static void BuyCar(string choice)
        {
            CarFactoryBase factory;
            if (choice.Equals("Toyota", StringComparison.OrdinalIgnoreCase))
            {
                factory = new ToyotaFactory();
            }
            else
            {
                factory = new PorscheFactory();
            }

            Store store = new Store(factory);
            store.Sell();
        }
    }
}
