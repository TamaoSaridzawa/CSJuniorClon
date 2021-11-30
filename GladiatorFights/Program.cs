using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladiatorFights
{
    class Program
    {
        static void Main(string[] args)
        {
            Arena arena = new Arena();
            arena.Figth();
            Console.ReadKey();
        }
    }

    abstract class SuperAbility
    {
        abstract public void UseSuperAbility(Gladiators gladiator);
        abstract public void DescribeAbility();
    }

    class Gladiators : SuperAbility
    {
        private static int _counter;
        private int _number;
        protected internal string Name { get; set; }
        protected internal double Damage { get; set; }
        protected internal double Health { get; set; }
        protected internal double Armor { get; set; }
        public int ChanceTriggeringSuperpowers { get; private set; }

        public Gladiators(string name, int damage, int health, int armor, int chanceTriggeringSuperpowers)
        {
            _number += ++_counter;
            Name = name;
            Damage = damage;
            Health = health;
            Armor = armor;
            ChanceTriggeringSuperpowers = chanceTriggeringSuperpowers;
        }

        public override void UseSuperAbility(Gladiators gladiator) { }

        public override void DescribeAbility() { }

        public void Attack(Gladiators gladiator)
        {
            gladiator.Health -= Damage - gladiator.Armor;

            Console.WriteLine($"{Name} Нанес {Damage}  урона, заблокировано броней - {gladiator.Armor}, Здоровье  {gladiator.Name} - {gladiator.Health}");
        }

        public void ShowStats()
        {
            Console.WriteLine($"Боец №{_number}: Имя - {Name}, Здоровье - {Health}, Урон - {Damage}, Броня {Armor}");
        }
    }

    class Assasin : Gladiators
    {
        private int _doubleDamage;

        public Assasin(string name, int damage, int health, int armor, int chanceTriggeringSuperpowers) : base(name, damage, health, armor, chanceTriggeringSuperpowers)
        {
            _doubleDamage = damage * 2;
        }

        public override void UseSuperAbility(Gladiators gladiator)
        {
            gladiator.Health -= _doubleDamage - gladiator.Armor;

            Console.WriteLine($"{Name} использовал Коварный удар, Нанесено урона {_doubleDamage}, заблокировано броней - {gladiator.Armor}, Здоровье {gladiator.Name} - {gladiator.Health}");
        }

        public override void DescribeAbility()
        {
            Console.WriteLine($"Класс - Убийца. Способность : Коварный удар - Подлый удар со спины наносящий {_doubleDamage}ед. фиксированного урона (не подлежит воздействию положительных или отрицательных эффектов), шанс срабатывания {ChanceTriggeringSuperpowers}%");
        }
    }

    class Warrior : Gladiators
    {
        private int _percentageBloodlust;

        public Warrior(string name, int damage, int health, int armor, int percentageBloodlust, int chanceTriggeringSuperpowers) : base(name, damage, health, armor, chanceTriggeringSuperpowers)
        {
            _percentageBloodlust = percentageBloodlust;
        }

        public override void UseSuperAbility(Gladiators gladiator)
        {
            gladiator.Health -= Damage - gladiator.Armor;
            Health += (Damage - gladiator.Armor) / 100 * _percentageBloodlust;

            Console.WriteLine($"{Name} использовал Кровожадный удар, Нанесено урона {Damage}, заблокировано броней - {gladiator.Armor}, Здоровье {gladiator.Name} - {gladiator.Health}");
        }

        public override void DescribeAbility()
        {
            Console.WriteLine($"Класс - 'Воин'. Способность - Кровожадный удар : Подлый удар со спины наносящий {Damage} урона, шанс срабатывания {ChanceTriggeringSuperpowers}%");
        }
    }

    class Shaman : Gladiators
    {
        private int _debuffPercentage;

        public Shaman(string name, int damage, int health, int armor, int chanceTriggeringSuperpowers, int debuffPercentage) : base(name, damage, health, armor, chanceTriggeringSuperpowers) 
        {
            _debuffPercentage = debuffPercentage;
        }

        public override void UseSuperAbility(Gladiators gladiator)
        {
            gladiator.Health -= Damage - gladiator.Armor;
            gladiator.Damage -= gladiator.Damage / 100 * _debuffPercentage;

            Console.WriteLine($"{Name} использовал Сглаз, урон {gladiator.Name} снижен на {_debuffPercentage} процентов, Нанесено урона {Damage},  заблокировано броней - {gladiator.Armor}, Здоровье - {gladiator.Name} {gladiator.Health}");
        }

        public override void DescribeAbility()
        {
            Console.WriteLine($"Класс - 'Шаман'. Способность - Сглаз : Проклятие наносящее {Damage} урона и снижает показатель вражеской атаки на {_debuffPercentage} процентов, шанс срабатывания {ChanceTriggeringSuperpowers}%");
        }
    }

    class Hunter : Gladiators
    {
        public Hunter(string name, int damage, int health, int armor, int chanceTriggeringSuperpowers) : base(name, damage, health, armor, chanceTriggeringSuperpowers) { }

        public override void UseSuperAbility(Gladiators gladiator)
        {
            gladiator.Health -= Damage;

            Console.WriteLine($"{Name} использовал Меткий выстрел, Нанесено урона {Damage}, Игнорирование брони , Здоровье - {gladiator.Name} {gladiator.Health}");
        }

        public override void DescribeAbility()
        {
            Console.WriteLine($"Класс - Охотник . Способность - Меткий выстрел : Находит уязвимое место в броне противника и делает точный выстрел наносящий {Damage} урона и полностю игнорирует броню противника," +
                $" шанс срабатывания {ChanceTriggeringSuperpowers}%");
        }
    }

    public class Arena
    {
        private Random _random = new Random();

        private int _numberFigtersOne;
        private int _numberFigtersTwo;

        private List<Gladiators> _gladiators = new List<Gladiators> { new Hunter("Reksar", 190, 1300, 90, 35), new Warrior("Aragorn", 150, 1800, 120, 50, 70),
               new Assasin("Riki", 200, 1000, 60, 40), new Shaman("Tral", 130, 1200, 80 , 80, 10) };

        public void Figth()
        {
            bool success = false;

            foreach (var gladiator in _gladiators)
            {
                gladiator.ShowStats();
                gladiator.DescribeAbility();
                Console.WriteLine();
            }

            int numberOne;
            int numberTwo;

            while (success == false)
            {
                Console.WriteLine("Выберете первого и второго бойца");

                if (int.TryParse(Console.ReadLine(), out numberOne) && int.TryParse(Console.ReadLine(), out numberTwo))
                {
                    _numberFigtersOne = numberOne - 1;
                    _numberFigtersTwo = numberTwo - 1;
                    success = true;
                }
                else
                {
                    Console.WriteLine("Введены некорректные данные");
                }
            }

            while (_gladiators[_numberFigtersOne].Health >= 0 && _gladiators[_numberFigtersTwo].Health >= 0)
            {
                int procent = _random.Next(1, 101);

                if (procent < _gladiators[_numberFigtersOne].ChanceTriggeringSuperpowers)
                {
                    _gladiators[_numberFigtersOne].UseSuperAbility(_gladiators[_numberFigtersTwo]);
                }
                else
                {
                    _gladiators[_numberFigtersOne].Attack(_gladiators[_numberFigtersTwo]);
                }

                if (procent < _gladiators[_numberFigtersTwo].ChanceTriggeringSuperpowers)
                {
                    _gladiators[_numberFigtersTwo].UseSuperAbility(_gladiators[_numberFigtersOne]);
                }
                else
                {
                    _gladiators[_numberFigtersTwo].Attack(_gladiators[_numberFigtersOne]);
                }

                Console.WriteLine();
            }

            if (_gladiators[_numberFigtersOne].Health <= 0 && _gladiators[_numberFigtersTwo].Health <= 0)
            {
                Console.WriteLine("Оба героя пали");
            }
            else if (_gladiators[_numberFigtersOne].Health <= 0)
            {
                Console.WriteLine($"Победил {_gladiators[_numberFigtersTwo].Name}");
            }
            else if (_gladiators[_numberFigtersTwo].Health <= 0)
            {
                Console.WriteLine($"Победил {_gladiators[_numberFigtersOne].Name}");
            }
        }
    }
}
