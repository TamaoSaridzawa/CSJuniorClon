using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    class Program
    {
        static void Main(string[] args)
        {
            Visitor visitor = new Visitor();

            Zoo zoo = new Zoo();

            Console.WriteLine("Вас приветствует программа Зоопарк");

            visitor.Watch(zoo);
            

            bool isOpenZoo = true;

            while (isOpenZoo)
            {
                int userAnswer;

                Console.Write("К какому вольеру вы хотите подойти поближе? Введите номер :");

                if (int.TryParse(Console.ReadLine(), out userAnswer) && userAnswer > 0 && userAnswer <= zoo.GetAviaries().Count)
                {
                    Console.WriteLine();

                    visitor.Watch(zoo.GetAviaries()[userAnswer - 1]);
                }
                else
                {
                    Console.WriteLine("Введены некорректные данные, попробуйте еще раз");
                }
            }
        }
    }

    interface IHasInfo
    {
        void ShowInfo();
    }

    abstract class Animal : IHasInfo
    {
        private string _name;
        private bool _sex;

        public Animal(string name, bool sex)
        {
            _name = name;
            _sex = sex;
        }

        public abstract void MakeSound();

        public void ShowInfo()
        {
            Console.Write($"{_name}, пол - {DetermineGender()}. ");
        }

        private string DetermineGender()
        {
            return _sex ? "Мужской" : "Женский";
        }
    }

    class Wolf : Animal
    {
        public Wolf(string name, bool sex) : base(name, sex) { }

        public override void MakeSound()
        {
            Console.WriteLine("Протяжно завывает");
        }
    }

    class Leon : Animal
    {
        public Leon(string name, bool sex) : base(name, sex) { }

        public override void MakeSound()
        {
            Console.WriteLine("Грозно рычит");
        }
    }

    class Panda : Animal
    {
        public Panda(string name, bool sex) : base(name, sex) { }

        public override void MakeSound()
        {
            Console.WriteLine("Свистит и фыркает");
        }
    }

    class Korosten : Animal
    {
        public Korosten(string name, bool sex) : base(name, sex) { }

        public override void MakeSound()
        {
            Console.WriteLine("Звонко верещит");
        }
    }

    class HoneyBadger : Animal
    {
        public HoneyBadger(string name, bool sex) : base(name, sex) { }

        public override void MakeSound()
        {
            Console.WriteLine("Всех убью, один останусь!");
        }
    }

    class Aviary : IHasInfo
    {
        private string _description;

        private List<Animal> _animals = new List<Animal>();

        public Aviary(string description, List<Animal> animals)
        {
            _description = description;
            _animals = animals;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{_description}. Количество животных в вольере - {GetNumberAnimals()}");

            foreach (var animal in _animals)
            {
                animal.ShowInfo();
                animal.MakeSound();
            }
        }

        private int GetNumberAnimals()
        {
            return _animals.Count;
        }
    }

    class Zoo : IHasInfo
    {
        private List<Aviary> _aviaries = new List<Aviary>();

        public Zoo()
        {
            _aviaries = CreateAviaries();
        }

        public void ShowInfo()
        {
            Console.WriteLine($"На данных момент открытых вольров - {_aviaries.Count}");
        }

        public IReadOnlyList<Aviary> GetAviaries()
        {
            return _aviaries;
        }

        private List<Aviary> CreateAviaries()
        {
            _aviaries.Add(new Aviary("Здесь живут волки", new List<Animal> { new Wolf("Волк - Вольф", true), new Wolf("Волчица - Соня", false) }));
            _aviaries.Add(new Aviary("Здесь живет медоед", new List<Animal> { new HoneyBadger("Медоед - Хаос", true) }));
            _aviaries.Add(new Aviary("Здесь живет прайд львов", new List<Animal> { new Leon("Король Лев - Кинг", true), new Leon("Лев - Панки", true), new Leon("Львица - Сири", false), new Leon("Львица - Кира", false) }));
            _aviaries.Add(new Aviary("Здесь живут панды и коростени", new List<Animal> { new Panda("Панда - Мякиш", true), new Korosten("Коростень - Сэнди", false) }));

            return _aviaries;
        }
    }

    class Visitor
    {
        public void Watch(IHasInfo hasInfo)
        {
            hasInfo.ShowInfo();
        }
    }
}
