using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefinitionDelay
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

    class Stew
    {
        public string Name { get; private set; }
        public int YearProduction { get; private set; }
        public int ExpirationDate { get; private set; }

        public Stew(string name, int yearProduction, int expirationDate)
        {
            Name = name;
            YearProduction = yearProduction;
            ExpirationDate = expirationDate;
        }
    }

    class Database
    {
        private int _currentYear;
        private List<Stew> _stews = new List<Stew>();

        public Database()
        {
            _currentYear = 2022;
            CreateStews();
        }

        public void Work()
        {
            Console.WriteLine($"На дату {_currentYear}, на складе лежат следующие банки тушонки:\n");

            foreach (var stew in _stews)
            {
                Console.WriteLine($"{stew.Name} : Дата изготовления - {stew.YearProduction}, Срок годности продукта - {stew.ExpirationDate}");
            }

            Console.WriteLine();

            List<Stew> _expiredStews = GetExpiredStews();

            foreach (var stew in _expiredStews)
            {
                Console.WriteLine($"Продукт от '{stew.Name}' просрочен.");
            }
        }

        private void CreateStews()
        {
            _stews.Add(new Stew("Буренка", 2012, 5));
            _stews.Add(new Stew("Наш продукт", 2021, 4));
            _stews.Add(new Stew("Магнит", 2015, 7));
            _stews.Add(new Stew("Гост", 2020, 8));
            _stews.Add(new Stew("Моя цена", 2015, 6));
        }

        private List<Stew> GetExpiredStews()
        {
            return _stews.Where(stew => stew.YearProduction + stew.ExpirationDate <= _currentYear).ToList();
        }
    }
}
