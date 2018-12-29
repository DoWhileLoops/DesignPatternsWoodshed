using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    public class SingletonClass
    {

        private string name;

        private SingletonClass()
        {
            name = "I am a singleton class.";
        }

        private static Lazy<SingletonClass> instance = new Lazy<SingletonClass>(() => new SingletonClass());

        public static SingletonClass Instance => instance.Value;

    }

    class Program
    {
        static void Main(string[] args)
        {
            var obj1 = SingletonClass.Instance;
            var obj2 = SingletonClass.Instance;

            Console.WriteLine("obj1 == obj 2 ?");
            Console.WriteLine(obj1 == obj2);
            Console.ReadKey();

        }
    }
}
