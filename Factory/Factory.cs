using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    class MusicianFactory
    {
        public interface IMusician
        {
            void Perform();
        }

        public class Bassist : IMusician
        {
            public void Perform()
            {
                Console.WriteLine("Lead Bass - engage");
            }
        }

        public class Guitarist : IMusician
        {
            public void Perform()
            {
                Console.WriteLine("Lead Guitar - engage");
            }
        }

        public interface IMusicianFactory
        {
            IMusician TuneUp();
        }

        public class BassistFactory : IMusicianFactory
        {
            public IMusician TuneUp()
            {
                Console.WriteLine("Tuning 4 strings...");
                return new Bassist();
            }
        }

        public class GuitaristFactory : IMusicianFactory
        {
            public IMusician TuneUp()
            {
                Console.WriteLine("Tuning 6 strings...");
                return new Guitarist();
            }
        }

        public class BandMachine
        {
            private List<Tuple<string, IMusicianFactory>> factories = new List<Tuple<string, IMusicianFactory>>();

            public BandMachine()
            {
                foreach (var t in typeof(BandMachine).Assembly.GetTypes())
                {
                    if (typeof(IMusicianFactory).IsAssignableFrom(t) && !t.IsInterface)
                    {
                        factories.Add(Tuple.Create(
                            t.Name.Replace("Factory", string.Empty),
                            (IMusicianFactory)Activator.CreateInstance(t)));
                    }
                }
            }

            public IMusician ChooseMember()
            {
                Console.WriteLine("Available Instruments:");
                for (int i = 0; i < factories.Count; i++)
                {
                    var tuple = factories[i];
                    Console.WriteLine($"{i}: {tuple.Item1}");
                }

                while (true)
                {
                    Console.WriteLine("Choose your destiny. Enter 'Q' to exit.");
                    string entry = Console.ReadLine();
                    if (entry.ToUpper() == "Q")
                    {
                        Environment.Exit(0);
                    }
                    else if (int.TryParse(entry, out int chosenIndex) && chosenIndex >= 0 && chosenIndex < factories.Count)
                    {
                        return factories[chosenIndex].Item2.TuneUp();
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice, please try again.");
                    }
                }

            }

            static void Main(string[] args)
            {
                BandMachine bm = new BandMachine();
                var member = bm.ChooseMember();
                member.Perform();

                Console.ReadKey();
            }
        }
    }
}
