using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War
{
    class Program
    {
        static void Main(string[] args)
        {
            Tavern tavern = new Tavern();
            tavern.ShowFihters();
            Battlefield battlefield = new Battlefield();
            battlefield.Fight();
            Console.ReadKey();
        }
    }

    abstract class Unit
    {
        public string Name { get; protected set; }
        public int ChanceTriggeringSuperpowers { get; protected set; }
        public string Specialization { get;protected set; }
        public bool IgnoringTank { get; protected set; }
        public double Damage { get; protected set; }
        public double Health { get; protected set; }
        public double Armor { get; protected set; }

        public Unit(string name)
        {
            Name = name;
        }

        public void ShowStats()
        {
            Console.WriteLine($"{Specialization}, Урон {Damage}, ЗДоровье {Health}, Броня {Armor}");
        }

        public abstract void DescribeAbility();

        public abstract  void Skill(Unit unit);

        public void Heal(Unit unit)
       {
            Skill(unit);
       }

        public void GiveHealing(double health)
        {
            Health += health;
            Console.WriteLine($"{Name} , {Specialization} Получил {health} здоровья");
        }

        public bool CheckHealth()
        {
            if (Health <= 0)
            {
                Console.WriteLine($"{Specialization} убит");
                return true;
            }
            else return false;
        }

        public void Attack(Unit unit, int procent)
        {
           if (procent < ChanceTriggeringSuperpowers)
           {
                Skill(unit);
           }

           else
           {
                unit.TakeDamage(Damage);
           }
        }

        public void ReturnDamage(double damage)
        {
            Health -= damage;
            Console.WriteLine($"{Name} , {Specialization} Получает {damage} урона");
        }

        public void TakeDamage(double damage)
        {
            Health -= damage - Armor;
            Console.WriteLine($"{Name}, {Specialization} Получено {damage}  урона, заблокировано броней - {Armor}, Здоровье - {Health}");
        }
    }

    class Warrior : Unit
    {
        private double _rabies;

        public Warrior(string name) : base(name)
        {
            ChanceTriggeringSuperpowers = 80;
            Specialization = "Воин";
            Damage = 300;
            Health = 1000;
            Armor = 100;
            _rabies = 10;
        }

        public override void DescribeAbility()
        {
            Console.WriteLine($"С шансом {ChanceTriggeringSuperpowers}процентов,  повышает свою атаку на {_rabies} процентов");
        }

        public override void Skill(Unit unit)
        {
            Console.WriteLine($"{Name} ,{Specialization} Использовал 'бешенство, атака увеличена'");

            Damage += Damage / 100 * _rabies;
            unit.TakeDamage(Damage);
        }
    }

    class Shooter : Unit
    {
        private double _aimedShot;

        public Shooter(string name) : base(name)
        {
            Specialization = "Стрелок";
            Damage = 400;
            Health = 800;
            Armor = 70;
            ChanceTriggeringSuperpowers = 30;
            IgnoringTank = true;
            _aimedShot = Damage * 1.5;
        }

        public override void DescribeAbility()
        {
            Console.WriteLine($"С шансом {ChanceTriggeringSuperpowers} наносит в полтора раза больше урона, Полностью игнорирует ЗАщитника вражесского отряда");
        }

        public override void Skill(Unit unit)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{Name} ,{Specialization} Использовал 'Выстрел дуплетом' ");

            unit.TakeDamage(_aimedShot);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

    class Druid : Unit
    {
        private double _firstAidKit;

        public Druid(string name) : base(name)
        {
            Specialization = "Друид";
            Damage = 290;
            Health = 1200;
            Armor = 120;
            ChanceTriggeringSuperpowers = 40;
            _firstAidKit = 30;
        }

        public override void DescribeAbility()
        {
            Console.WriteLine($"С шансом {ChanceTriggeringSuperpowers} исцеляет себя на {_firstAidKit} ед. здоровья");
        }

        public override void Skill(Unit unit)
        {
            Console.WriteLine($"{Name} ,{Specialization} использовал аптечку");

            Health += _firstAidKit;

            unit.TakeDamage(Damage);
        }
    }

    class Medic : Unit
    {
        private double _healingBandage;

        public Medic(string name) : base(name)
        {
            Specialization = "Медик";
            Damage = 220;
            Health = 900;
            Armor = 50;
            ChanceTriggeringSuperpowers = 90;
            _healingBandage = 70;
        }

        public override void DescribeAbility()
        {
            Console.WriteLine($"С шансом {ChanceTriggeringSuperpowers} процентов исцеляет союзника на {_healingBandage}. Является хилом группы");
        }

        public override void Skill(Unit unit)
        {
            Console.WriteLine($"{Name}, {Specialization} использовал исцелени");
            unit.GiveHealing(_healingBandage);
        }
    }

    class Defender : Unit
    {
        private double _spikedArmor;

        public Defender(string name) : base(name)
        {
            Specialization = "Защитник";
            Damage = 240;
            Health = 1500;
            Armor = 200;
            ChanceTriggeringSuperpowers = 50;
        }

        public override void DescribeAbility()
        {
            Console.WriteLine($"С шансом {ChanceTriggeringSuperpowers} наносит дополнительного урона зависящего от атаки противника, является танком группы и берет весь вражесский урон на себя");
        }

        public override void Skill(Unit unit)
        {
            _spikedArmor = unit.Damage / 2;

            Console.WriteLine($"{Name}, {Specialization} использовал 'Шипастый танк'");

            unit.ReturnDamage(_spikedArmor);

            unit.TakeDamage(Damage);
        }
    }

    class Battlefield
    {
        private Detachment _detachment = new Detachment();
        private Detachment _detachment2 = new Detachment();

        Random random1 = new Random();

        public void Fight()
        {
            while (_detachment.CheckForFighters() && _detachment2.CheckForFighters() )
            {
                _detachment.JoinBattle(_detachment2.GetUnits(), random1);

                Console.WriteLine("---------------------------");

                _detachment2.JoinBattle(_detachment.GetUnits(), random1);

                Console.WriteLine("---------------------------");
            }

            if (!_detachment.CheckForFighters())
            {
                Console.WriteLine($"Армия {_detachment.Name} пала");
            }
            else if (!_detachment2.CheckForFighters())
            {
                Console.WriteLine($"Армия {_detachment2.Name} пала");
            }
        }
    }

    class Detachment
    {
        private List<Unit> _units = new List<Unit>();
        private int _size = 5;
        public string Name { get; private set; }
       
        public List<Unit> GetUnits()
        {
            return _units;
        }

        public Detachment()
        {
            Create();
        }

        private void Create()
        {
            Console.WriteLine("Введите имя вашей армии");
            Name = Console.ReadLine();

            for (int i = 0; i < _size; i++)
            {
                Console.Write("ВЫберете бойца");

                string userAnswer = Console.ReadLine();

                switch (userAnswer)
                {
                    case "1":
                        _units.Add(new Warrior(Console.ReadLine()));
                        break;
                    case "2":
                        _units.Add(new Shooter(Console.ReadLine()));
                        break;
                    case "3":
                        _units.Add(new Druid(Console.ReadLine()));
                        break;
                    case "4":
                        _units.Add(new Medic(Console.ReadLine()));
                        break;
                    case "5":
                        _units.Add(new Defender(Console.ReadLine()));
                        break;
                    default:
                        Console.WriteLine("Введены некорректные данные, повторите попытку");
                        i--;
                        break;
                }
            }
        }

        public void JoinBattle(List<Unit> units, Random random)
        {
            Console.WriteLine($"Армия {Name} атакована");

            for (int i = 0; i < units.Count; i++)
            {
                if (units[i].Specialization.Contains("Медик"))
                {
                    units[i].Heal(units[random.Next(0, units.Count())]);
                }
                else
                {
                    int indexUnit = random.Next(0, _units.Count());

                    int procent = random.Next(1, 101);

                    if (DefenderisAlive() && !units[i].IgnoringTank)
                    {
                        units[i].Attack(GetDefender(ref indexUnit), procent);
                    }
                    else
                    {
                        units[i].Attack(_units[indexUnit], procent);
                    }

                    if (_units[indexUnit].CheckHealth())
                    {
                        _units.RemoveAt(indexUnit);
                    }

                    if (_units.Count == 0)
                    {
                        break;
                    }
                }
            }
        }

        public bool CheckForFighters()
        {
            return _units.Count > 0;
        }

        private Unit GetDefender(ref int indexDefender)
        {
            for (int i = 0; i < _units.Count; i++)
            {
                if (_units[i].Specialization.Contains("Защитник"))
                {
                    indexDefender = i;
                    return _units[i];
                }  
            }

            return null;
        }

        private bool DefenderisAlive()
        {
            foreach (var unit in _units)
            {
                if (unit.Specialization.Contains("Защитник"))
                {
                    return true;
                }
            }

            return false;
        }
    }

    class Tavern 
    {
        private List<Unit> _fighers = new List<Unit> { new Warrior("Боец № 1"), new Shooter("Боец № 2"), new Druid("Боец № 3"), new Medic("Боец № 4"), new Defender("Боец № 5") };

        public void ShowFihters()
        {
            Console.WriteLine("Сейчас в таверне есть следующие бойцы : ");

            foreach (var figters in _fighers)
            {
                figters.ShowStats();
                figters.DescribeAbility();
            }
        }
    }
}
