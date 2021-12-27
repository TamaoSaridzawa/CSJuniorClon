using System;
using System.Collections.Generic;
using System.Linq;

namespace WeaponsReport
{
    class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            database.Work();

            Console.ReadKey();
        }
    }

    class Soildier
    {
        public string Name { get; private set; }
        public string Weapon { get; private set; }
        public string Title { get; private set; }
        public int ServiceLife { get; private set; }

        public Soildier(string name, string weapon, string title, int serviceLife)
        {
            Name = name;
            Weapon = weapon;
            Title = title;
            ServiceLife = serviceLife;
        }
    }

    class Database
    {
        private List<Soildier> _soldiers = new List<Soildier>();

        public Database()
        {
            CreateSoldiers();
        }

        public void Work()
        {
            Console.WriteLine("Представляю вам вашу команду");

            ShowNameAndRankSoldier();
        }

        private void CreateSoldiers()
        {
            _soldiers.Add(new Soildier("Гектор", "Винтовка", "Сержант", 24));
            _soldiers.Add(new Soildier("Дэни", "Автомат", "Капрал", 18));
            _soldiers.Add(new Soildier("Рэд", "Пистолет Кольт", "Капитан", 44));
            _soldiers.Add(new Soildier("Шэри", "Пулемет", "Рядовой", 24));
            _soldiers.Add(new Soildier("Расти", "Автомат", "Сержант", 24));
        }

        private void ShowNameAndRankSoldier()
        {
            var filterSoldiers = _soldiers.Select(soildier => new
            {
                NameAndRank = soildier.Title + " " + soildier.Name 
            });

            foreach (var soildier in filterSoldiers)
            {
                Console.WriteLine(soildier.NameAndRank);
            }
        }
    }
}
