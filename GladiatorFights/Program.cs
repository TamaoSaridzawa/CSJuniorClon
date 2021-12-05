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
            arena.ChooseGladiators();
            Console.ReadKey();
        }
    }

    abstract class Gladiator
    {
        protected internal string Name;
        protected internal double Damage;
        protected internal double Health;
        protected internal double Armor;
        private static int _counter;
        private int _number;
        public int ChanceTriggeringSuperpowers { get; private set; }

        public Gladiator(string name, int damage, int health, int armor, int chanceTriggeringSuperpowers)
        {
            _number += ++_counter;
            Name = name;
            Damage = damage;
            Health = health;
            Armor = armor;
            ChanceTriggeringSuperpowers = chanceTriggeringSuperpowers;
        }

        public abstract void UseSuperAbility(Gladiator gladiator);

        public abstract void DescribeAbility();

        public void Attack(Gladiator gladiator, int procent)
        {
            if (procent < ChanceTriggeringSuperpowers)
            {
                UseSuperAbility(gladiator);
            }
            else
            {
               gladiator.TakeDamage(Damage);
            }
        }

        public void TakeDamage(double damage)
        {
            Health -= damage - Armor;

            Console.WriteLine($"{Name} Получено {damage}  урона, заблокировано броней - {Armor}, Здоровье - {Health}");
        }

        public void ShowStats()
        {
            Console.WriteLine($"Боец №{_number}: Имя - {Name}, Здоровье - {Health}, Урон - {Damage}, Броня {Armor}");
        }
    }

    class Assasin : Gladiator
    {
        private int _doubleDamage;

        public Assasin(string name, int damage, int health, int armor, int chanceTriggeringSuperpowers) : base(name, damage, health, armor, chanceTriggeringSuperpowers)
        {
            _doubleDamage = damage * 2;
        }

        public override void UseSuperAbility(Gladiator gladiator)
        {
            gladiator.Health -= _doubleDamage - gladiator.Armor;

            Console.WriteLine($"{Name} использовал Коварный удар, Нанесено урона {_doubleDamage}, заблокировано броней - {gladiator.Armor}, Здоровье {gladiator.Name} - {gladiator.Health}");
        }

        public override void DescribeAbility()
        {
            Console.WriteLine($"Класс - Убийца. Способность : Коварный удар - Подлый удар со спины наносящий {_doubleDamage}ед. фиксированного урона (не подлежит воздействию положительных или отрицательных эффектов), шанс срабатывания {ChanceTriggeringSuperpowers}%");
        }
    }

    class Warrior : Gladiator
    {
        private int _percentageBloodlust;

        public Warrior(string name, int damage, int health, int armor, int percentageBloodlust, int chanceTriggeringSuperpowers) : base(name, damage, health, armor, chanceTriggeringSuperpowers)
        {
            _percentageBloodlust = percentageBloodlust;
        }

        public override void UseSuperAbility(Gladiator gladiator)
        {
            gladiator.Health -= Damage - gladiator.Armor;
            Health += (Damage - gladiator.Armor) / 100 * _percentageBloodlust;

            Console.WriteLine($"{Name} использовал Кровожадный удар, Нанесено урона {Damage}, заблокировано броней - {gladiator.Armor}, Здоровье {gladiator.Name} - {gladiator.Health}");
        }

        public override void DescribeAbility()
        {
            Console.WriteLine($"Класс - 'Воин'. Способность - Кровожадный удар : Восстанавливает {_percentageBloodlust}% от нанесенного урона, шанс срабатывания {ChanceTriggeringSuperpowers}%");
        }
    }

    class Shaman : Gladiator
    {
        private int _debuffPercentage;

        public Shaman(string name, int damage, int health, int armor, int chanceTriggeringSuperpowers, int debuffPercentage) : base(name, damage, health, armor, chanceTriggeringSuperpowers) 
        {
            _debuffPercentage = debuffPercentage;
        }

        public override void UseSuperAbility(Gladiator gladiator)
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

    class Hunter : Gladiator
    {
        public Hunter(string name, int damage, int health, int armor, int chanceTriggeringSuperpowers) : base(name, damage, health, armor, chanceTriggeringSuperpowers) { }

        public override void UseSuperAbility(Gladiator gladiator)
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

        private List<Gladiator> _gladiators = new List<Gladiator> { new Hunter("Reksar", 190, 1300, 90, 35), new Warrior("Aragorn", 150, 1800, 120, 50, 70),
               new Assasin("Riki", 200, 1000, 60, 40), new Shaman("Tral", 130, 1200, 80 , 80, 10) };

        public void ChooseGladiators()
        {
            foreach (var gladiator in _gladiators)
            {
                gladiator.ShowStats();
                gladiator.DescribeAbility();
                Console.WriteLine();
            }

            int numberOne;
            int numberTwo;
            bool success = false;

            while (success == false)
            {
                Console.WriteLine("Выберете двух бойцов : ");

                if (int.TryParse(Console.ReadLine(), out numberOne) && int.TryParse(Console.ReadLine(), out numberTwo))
                {
                    if (numberOne != numberTwo && numberOne <= _gladiators.Count && numberTwo <= _gladiators.Count && numberOne > 0 && numberTwo > 0)
                    {
                        Figth(_gladiators[numberOne - 1], _gladiators[numberTwo - 1]);

                        success = true;
                    }
                    else
                    {
                        Console.WriteLine("Введены неверные номера героев!");
                    }
                }
                else
                {
                    Console.WriteLine("Введены некорректные данные");
                }
            }
        }

        private void Figth(Gladiator gladiatorOne, Gladiator gladiatorTwo)
        {
            while (gladiatorOne.Health >= 0 && gladiatorTwo.Health >= 0)
            {
                int procent = _random.Next(1, 101);

                gladiatorOne.Attack(gladiatorTwo, procent);
                gladiatorTwo.Attack(gladiatorOne, procent);

                Console.WriteLine();
            }

            if (gladiatorOne.Health <= 0 && gladiatorTwo.Health <= 0)
            {
                Console.WriteLine("Оба героя пали");
            }
            else if (gladiatorOne.Health <= 0)
            {
                Console.WriteLine($"Победил {gladiatorTwo.Name}");
            }
            else if (gladiatorTwo.Health <= 0)
            {
                Console.WriteLine($"Победил {gladiatorOne.Name}");
            }
        }
    }
}
