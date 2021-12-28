using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnificationTroops
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Soildier> soildiersOne = new List<Soildier> { new Soildier("Дэн"), new Soildier("Раф"), new Soildier("Бэн"), new Soildier("Боб") };
            List<Soildier> soildiersTwo = new List<Soildier> { new Soildier("Зик"), new Soildier("Георг"), new Soildier("Барак"), new Soildier("Алекс") };

            soildiersTwo = soildiersOne.Where(soildier => soildier.Name.ToUpper().StartsWith("Б")).Union(soildiersTwo).ToList();

            soildiersOne = soildiersOne.Where(soildier => !soildier.Name.ToUpper().StartsWith("Б")).ToList();

            foreach (var soildier in soildiersOne)
            {
                Console.WriteLine(soildier.Name);
            }

            Console.WriteLine();

            foreach (var soildier in soildiersTwo)
            {
                Console.WriteLine(soildier.Name);
            }

            Console.ReadKey();
        }
    }

    class Soildier
    {
        public string Name { get; private set; }

        public Soildier(string name)
        {
            Name = name;
        }
    }
}
