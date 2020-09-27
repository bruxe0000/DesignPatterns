using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeDesignPatterns.CreationalDP
{
    /// <summary>
    /// Pros:
    /// - You can clone objects without coupling to their concrete classes.
    /// - You can get rid of repeated initialization code in favor of cloning pre-built prototypes.
    /// - You can produce complex objects more conveniently.
    /// - You get an alternative to inheritance when dealing with configuration presets for complex objects.
    /// 
    /// Cons:
    /// - Cloning complex objects that have circular references might be very tricky.
    /// </summary>

    /// <summary>
    /// For more information, please read this blog: https://kipalog.com/posts/Design-Pattern--Prototype-Pattern---C-
    /// </summary>
    abstract class GirlFriend
    {
        public string name { get; set; }
        public string say { get; set; }
        public string myResponse { get; set; }

        public GirlFriend(string name, string say, string myResponse)
        {
            this.name = name;
            this.say = say;
            this.myResponse = myResponse;
        }
    }

    class Uyen : GirlFriend, ICloneable
    {
        public Uyen(string name, string say, string myResponse) : base(name, say, myResponse)
        {
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }


    public class PrototypeExample
    {
        public static void AskingUyen()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            Uyen uyen = new Uyen("Uyên", "And ăn cơm chưa?", "Anh ăn cơm rồi");
            Console.WriteLine($"Original {uyen.name} says: \"{uyen.say}\". \n My reponse: \"{uyen.myResponse}\" \n");

            Uyen cloneOfUyen = (Uyen)uyen.Clone();
            cloneOfUyen.say = "Anh ghét em lắm";
            cloneOfUyen.myResponse = "Whatever! I love you, bae!";
            Console.WriteLine($"Clone {cloneOfUyen.name} says: \"{cloneOfUyen.say}\". \n My reponse: \"{cloneOfUyen.myResponse}\" ");
        }
    }    
}
