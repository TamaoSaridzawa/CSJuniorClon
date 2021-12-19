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

        public void Live()
        {
            LifeSpan--;
        }

        public void ViewStatus()
        {
            if (LifeSpan > 0)
            {
                Console.WriteLine($"{View} по имени {_name} - осталось жить {LifeSpan} дней");
            }
            else
            {
                Console.WriteLine($"{View} по имени {_name} плавает к верху брюхом...");
            }
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

        private List<Fish> _fishes = new List<Fish>();

        public Aquarium()
        {
            _capacity = 10;
        }

        public void AddFish()
        {
            if (_fishes.Count < _capacity)
            {
                _fishes.Add(CreateFish());
            }
            else
            {
                Console.WriteLine("В аквариуме больше нет мест!");
            }
        }

        public void EndDay()
        {
            foreach (var fish in _fishes)
            {
                fish.Live();
            }
        }

       public void RemoveFish()
        {
            if (IsEmpty())
            {
                _fishes.RemoveAt(GetIndex());
            }
            else
            {
                Console.WriteLine("А вытаскивать то некого!");
            }
           
        }

        public void ShowFishes()
        {
            if (!IsEmpty())
            {
                int counte = 1;

                foreach (var fish in _fishes)
                {
                    Console.Write($"{counte} - ");
                    counte++;

                    fish.ViewStatus();
                }
            }
            else
            {
                Console.WriteLine("В аквариуме пусто");
            }
        }

        private int GetIndex()
        {
            int numberFish = _fishes.Count();
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

        private Fish CreateFish()
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

        private bool IsEmpty()
        {
            return _fishes.Count < 0;
        }
    }

    class Aquarist
    {
        private Aquarium _aquarium = new Aquarium();

        public void Service()
        {
            bool isStop = true;

            while (isStop)
            {
                Console.WriteLine("Выберете действие : \n1.Проверить рыбок\n2.Добавить новую рыбку\n3.Вытащить рыбку из аквариума\nЛюбая клавиша.Забить сегодня на аквариум и пойти спать (очистить консоль)" +
                    "\n4.Выкинуть аквариум в окно (выход из программы)");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        _aquarium.ShowFishes();
                        break;
                    case "2":
                        _aquarium.AddFish();
                        break;
                    case "3":
                        _aquarium.RemoveFish();
                        break;
                    case "4":
                        isStop = false;
                        break;
                    default:
                        Console.Clear();
                        break;
                }

                _aquarium.EndDay();
            }
        }
    }
}
