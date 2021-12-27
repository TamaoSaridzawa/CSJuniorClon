using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    class Soldier
    {
        public string Name { get; private set; }
        public string Weapon { get; private set; }
        public string Title { get; private set; }
        public int ServiceLife { get; private set; }

        public Soldier(string name, string weapon, string title, int serviceLife)
        {
            Name = name;
            Weapon = weapon;
            Title = title;
            ServiceLife = serviceLife;
        }
    }

    class Database
    {
        private List<Soldier> _soldiers = new List<Soldier>();

        public Database()
        {
            CreateSolidier();
        }

        public void Work()
        {
            Console.WriteLine("Представляю вам вашу команду");

            ShowNameAndRankSoldier();
        }

        private void CreateSolidier()
        {
            _soldiers.Add(new Soldier("Гектор", "Винтовка", "Сержант", 24));
            _soldiers.Add(new Soldier("Дэни", "Автомат", "Капрал", 18));
            _soldiers.Add(new Soldier("Рэд", "Пистолет Кольт", "Капитан", 44));
            _soldiers.Add(new Soldier("Шэри", "Пулемет", "Рядовой", 24));
            _soldiers.Add(new Soldier("Расти", "Автомат", "Сержант", 24));
        }

        private void ShowNameAndRankSoldier()
        {
            var filterSoliders = _soldiers.Select(soildier => new
            {
                NameAndRank = soildier.Title + " " + soildier.Name 
            });

            foreach (var soildier in filterSoliders)
            {
                Console.WriteLine(soildier.NameAndRank);
            }
        }
    }
}
