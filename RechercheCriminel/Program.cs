using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechercheCriminel
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
        public bool IsDetained { get; private set; }
        public double Height { get; private set; }
        public double Weigth { get; private set; }
        public string Nationality { get; private set; }

        public Criminal(string surname, bool isDetained, double height, double weigth, string nationality)
        {
            Surname = surname;
            IsDetained = isDetained;
            Height = height;
            Weigth = weigth;
            Nationality = nationality;
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
            Console.WriteLine("Для поиска преступников введите следующие параметры ");

            double height;
            double weight;
            bool succces = false;

            while (succces == false)
            {
                Console.WriteLine("Введите рост, а затем вес :");

                if (double.TryParse(Console.ReadLine(), out height) && double.TryParse(Console.ReadLine(), out weight))
                {
                    succces = true;

                    Console.Write("Введите национальность :");
                    string nationality = Console.ReadLine();

                    var filterCriminals = _criminals.Where(criminal => criminal.IsDetained == false).Where(criminal => criminal.Height == height
                    || criminal.Weigth == weight || criminal.Nationality.ToLower().Contains(nationality));

                    Console.WriteLine();

                    foreach (var criminal in filterCriminals)
                    {
                        Console.WriteLine(criminal.Surname);
                    }
                }
                else
                {
                    Console.WriteLine("Неправильный ввод данных, попробуйте еще раз");
                }
            }
        }

        private void CreateCriminals()
        {
            _criminals.Add(new Criminal("Семен Владимирович Шпак", false, 178, 95.3, "Русский"));
            _criminals.Add(new Criminal(" Адольф Штуцер Берг", true, 190, 86.4, "Немец"));
            _criminals.Add(new Criminal("Григорий Секанович Черный", true, 201, 104, "Албанец"));
            _criminals.Add(new Criminal("Рубен Хамлетович Мартиросян", false, 165, 112, "Армянин"));
        }
    }
}
