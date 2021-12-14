using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aquarium
{
    class Program
    {
        static void Main(string[] args)
        {
            Aquarist aquarist = new Aquarist();
            aquarist.Service();
        }
    }

     class Fish
    {
        private string _name ;
        public int LifeSpan { get; protected set; }
        public string View { get; protected set; }

        public Fish()
        {
            Console.Write("Дайте имя новой рыбке :");
            _name = Console.ReadLine();
        }

        public void Life()
        {
            LifeSpan--;
        }

        public void CheckHealth()
        {
            if (LifeSpan > 0)
            {
                ShowIndicators();
            }
            else
            {
                Console.WriteLine($"{View} по имени {_name} плавает к верху брюхом...");
            }
        }

        public void ShowIndicators()
        {
            Console.WriteLine($"{View} по имени {_name} - осталось жить {LifeSpan} дней");
        }
    }

    class Сockerel : Fish
    {
        public Сockerel() : base()
        {
            View = "Петушок";
            LifeSpan = 10;
        }
    }

    class Angelfish : Fish
    {
        public Angelfish() : base()
        {
            View = "Скалярия";
            LifeSpan = 8;
        }
    }

    class Barbus : Fish
    {
        public Barbus() : base()
        {
            View = "Барбус";
            LifeSpan = 13;
        }
    }

    class Aquarium
    {
        private int _capacity;

        private List<Fish> _fishs = new List<Fish>();

        public Aquarium()
        {
            _capacity = 10;
        }

        public void AddFish()
        {
            if (_fishs.Count < _capacity)
            {
                _fishs.Add(GetFish());
            }
            else
            {
                Console.WriteLine("В аквариуме больше нет мест!");
            }
        }

        public void EndDay()
        {
            foreach (var fish in _fishs)
            {
                fish.Life();
            }
        }

       public void RemoveFish()
        {
            if (CheckForFish())
            {
                _fishs.RemoveAt(GetIndex());
            }
            else
            {
                Console.WriteLine("А вытаскивать то некого!");
            }
           
        }

        public void MonitorFish()
        {
            if (CheckForFish())
            {
                int counte = 1;

                foreach (var fish in _fishs)
                {
                    Console.Write($"{counte} - ");
                    counte++;

                    fish.CheckHealth();
                }
            }
            else
            {
                Console.WriteLine("В аквариуме пусто");
            }
        }

        private int GetIndex()
        {
            int numberFish = _fishs.Count();
            int number;
            bool success = false;

            Console.Write($"Рыбок в аквариуме {numberFish}. Введите номер рыбки :");

            while (success == false)
            {
                if (int.TryParse(Console.ReadLine(), out number) && number <= numberFish && number > 0)
                {
                    Console.WriteLine("Вы успешно вытащили рыбку!");

                    return number - 1;
                }
                else
                {
                    Console.WriteLine("Введены некорректные данные");
                }
            }
            return -1;
        }

        private Fish GetFish()
        {
            bool success = false;
           
            while (success == false)
            {
                Console.WriteLine("На выбор есть следующие рыбки :\n1.Петушок\n2.Скалярия\n3.Барбус");
                Console.Write("Введите номер рыбки, которую вы хотите добавить :");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        return new Сockerel();
                    case "2":
                        return new Angelfish();
                    case "3":
                        return new Barbus();
                    default:
                        Console.WriteLine("Введены не корректные данные");
                        break;
                }
            }

            return null;
        }

        private bool CheckForFish()
        {
            return _fishs.Count > 0;
        }
    }

    class Aquarist
    {
        Aquarium aquarium = new Aquarium();

        public void Service()
        {
            bool isStop = true;
            int number;

            while (isStop)
            {
                Console.WriteLine("Выберете действие : \n1.Проверить рыбок\n2.Добавить новую рыбку\n3.Вытащить рыбку из аквариума\n4.Забить сегодня на аквариум и пойти спать (очистить консоль)" +
                    "\n5.Выкинуть аквариум в окно (выход из программы)");

                if (int.TryParse(Console.ReadLine(), out number) && number > 0 && number <= 5)
                {
                    switch (number)
                    {
                        case 1:
                            aquarium.MonitorFish();
                            break;
                        case 2:
                            aquarium.AddFish();
                            break;
                        case 3:
                            aquarium.RemoveFish();
                            break;
                        case 4:
                            Console.Clear();
                            break;
                        case 5:
                            isStop = false;
                            break;
                    }

                    aquarium.EndDay();
                }
            }
        }
    }
}
