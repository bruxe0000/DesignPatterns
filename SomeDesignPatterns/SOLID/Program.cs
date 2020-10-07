using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOLID.SOLIDDefinitions;

namespace SOLID
{
    class Program
    {
        static void Main(string[] args)
        {
            /// **** S letter Example ****
            FileHandler1 fHandler = new FileHandler1();
            fHandler.Delete("Example.txt");
            FileHandler2 fHandler2 = new FileHandler2();
            fHandler.Delete("Example.txt");

            /// **** O letter Example ****
            Logger1 logger = new Logger1();
            logger.Log("[ERROR] Print failed");
            ErrorLogger2 logger2 = new ErrorLogger2();
            logger2.Log("[ERROR] Print failed");

            /// **** L letter Example ****
            Duck duck1 = new Duck();
            duck1.Fly();
            Ostrich ostrich1 = new Ostrich();
            ostrich1.Fly();

            Duck2 duck2 = new Duck2();
            duck2.Fly();
            duck2.GetWeight();

            Ostrich2 ostrich2 = new Ostrich2();
            ostrich2.GetWeight();

            /// **** I letter Example ****
            // Just interfaces, maybe show later if I'm free and in mood :D

            /// **** O letter Example ****
            FileHandler3 fHandler3 = new FileHandler3();
            fHandler3.Delete("Example.txt");

            FileHandler4 fHandler4 = new FileHandler4();
            fHandler4.GetLogger(new Logger2());
            fHandler4.Delete("Example.txt");

            FileHandler4 fHandler4_1 = new FileHandler4();
            fHandler4.GetLogger(new ErrorLogger2());
            fHandler4.Delete("Example.txt");


            Console.ReadKey();
        }
    }    
}
