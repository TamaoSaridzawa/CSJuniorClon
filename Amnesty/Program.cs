using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amnesty
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

    class Criminal
    {
        public string Surname { get; private set; }
        public string Сrime { get; private set; }

        public Criminal(string surname,string сrime)
        {
            Surname = surname;
            Сrime = сrime;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"ФИО - {Surname}, Преступление - {Сrime}");
        }
    }

    class Database
    {
        private List<Criminal> _criminals = new List<Criminal>();

        public Database()
        {
            CreateCriminals();
        }

        public void Work()
        {
            Console.WriteLine("Список преступников до амнистии :");

            foreach (var criminal in _criminals)
            {
                criminal.ShowInfo();
            }

            Console.WriteLine();
            Console.WriteLine("Список преступников после амнистии :");

            Amnesty();

            foreach (var criminal in _criminals)
            {
                criminal.ShowInfo();
            }
        }

        private void Amnesty()
        {
            _criminals = _criminals.Where(criminal => criminal.Сrime != "Антиправительственное").ToList();
        }

        private void CreateCriminals()
        {
            _criminals.Add(new Criminal("Семен Владимирович Шпак", "Антиправительственное"));
            _criminals.Add(new Criminal(" Адольф Штуцер Берг", "Убийство"));
            _criminals.Add(new Criminal("Григорий Секанович Черный", "Антиправительственное"));
            _criminals.Add(new Criminal("Рубен Хамлетович Мартиросян", "Воровство"));
        }
    }
}
