using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    public class Musician
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Musician(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public Musician DeepCopyJson()
        {
            string json = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Musician>(json);
        }

        public override string ToString()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}");
            return null;
        }
    }

    public class Prototype
    {
        static void Main(string[] args)
        {
            Musician youngOne = new Musician("Justin Bieber", 17);
            Musician oldOne = youngOne.DeepCopyJson();

            oldOne.Name = "Ludwig Van";
            oldOne.Age = 352;

            youngOne.ToString();
            oldOne.ToString();

            Console.ReadKey();
        }
    }
}
