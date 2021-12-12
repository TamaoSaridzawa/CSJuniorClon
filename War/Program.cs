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
        public int ChanceTriggeringSuperpowers;
        public string _specialization { get; set; }
        public bool IgnoringTank;
        public double Damage;
        protected double Health { get; set; }
        protected double Armor { get; set; }

        public Unit()
        {
        }

        public void ShowStats()
        {
            Console.WriteLine($"{_specialization}, Урон {Damage}, ЗДоровье {Health}, Броня {Armor}");
        }

        public abstract void DescribeAbility();

        public void GiveHealing(double health)
        {
            Health += health;
            Console.WriteLine($"{_specialization} Получил {health} здоровья");
        }

        public bool CheckHealth()
        {
            if (Health <= 0)
            {
                Console.WriteLine($"{_specialization} убит");
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
            Console.WriteLine($"{_specialization} Получает {damage} урона");
        }

        public void TakeDamage(double damage)
        {
            Health -= damage - Armor;
            Console.WriteLine($"{_specialization} Получено {damage}  урона, заблокировано броней - {Armor}, Здоровье - {Health}");
        }

        abstract public void Skill(Unit unit);
    }

    class Warrior : Unit
    {
        private double _rabies;

        public Warrior() : base()
        {
            ChanceTriggeringSuperpowers = 80;
            _specialization = "Воин";
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
            Console.WriteLine($"{_specialization} Использовал 'бешенство'");

            Damage += Damage / 100 * _rabies;
            unit.TakeDamage(Damage);
        }
    }

    class Shooter : Unit
    {
        private double _aimedShot;

        public Shooter() : base()
        {
            _specialization = "Стрелок";
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
            Console.WriteLine($"{_specialization} Использовал 'Выстрел дуплетом' ");

            unit.TakeDamage(_aimedShot);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

    class Governor : Unit
    {
        private double _firstAidKit;

        public Governor() : base()
        {
            _specialization = "Воевода";
            Damage = 250;
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
            Console.WriteLine($"{_specialization} использовал аптечку");

            Health += _firstAidKit;

            unit.TakeDamage(Damage);
        }
    }

    class Medic : Unit
    {
        private double _healingBandage;

        public Medic() : base()
        {
            _specialization = "Медик";
            Damage = 220;
            Health = 900;
            Armor = 50;
            ChanceTriggeringSuperpowers = 80;
            _healingBandage = 50;
        }

        public override void DescribeAbility()
        {
            Console.WriteLine($"С шансом {80} процентов исцеляет союзника на {_healingBandage}. Является хилом группы");
        }

        public override void Skill(Unit unit)
        {
            Console.WriteLine($"{_specialization} Использовал 'Заживдя.зая повязка'");
            unit.GiveHealing(_healingBandage);
        }
    }

    class Defender : Unit
    {
        private double _spikedArmor;

        public Defender() : base()
        {
            _specialization = "Защитник";
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

            Console.WriteLine($"{_specialization} использовал 'Шипастый танк'");

            unit.ReturnDamage(_spikedArmor);

            unit.TakeDamage(Damage);
        }
    }

    class Battlefield
    {
        private Detachment _detachment = new Detachment() ;
        private Detachment _detachment2 = new Detachment();

        Random random1 = new Random();

        public void Fight()
        {
            while (_detachment.CheckForFighters() && _detachment2.CheckForFighters() )
            {
                _detachment.JoinBattle(_detachment2._units, random1);

                Console.WriteLine("---------------------------");

                _detachment2.JoinBattle(_detachment._units, random1);

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
        public List<Unit> _units = new List<Unit>();
        public string Name;
        public int  size = 5;
        private bool _buffAttacks;
        private bool _buffProtection;

        public Detachment()
        {
            Console.WriteLine("Введите имя вашей армии");
            Name = Console.ReadLine();

            for (int i = 0; i < size; i++)
            {
                Console.Write("ВЫберете бойца");

                string userAnswer = Console.ReadLine();

                switch (userAnswer)
                {
                    case "1":
                        _units.Add(new Warrior());
                        break;
                    case "2":
                        _units.Add(new Shooter());
                        break;
                    case "3":
                        _units.Add(new Governor());
                        break;
                    case "4":
                        _units.Add(new Medic());
                        break;
                    case "5":
                        _units.Add(new Defender());
                        break;
                    default:
                        break;
                }
            }
        }

        public void JoinBattle(List<Unit> units, Random random)
        {
            Console.WriteLine($"Армия {Name} атакована");

            for (int i = 0; i < units.Count; i++)
            {
                if (units[i]._specialization.Contains("Медик"))
                {
                    units[i].Skill(units[random.Next(0, units.Count())]);
                }

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

            //foreach (var unit in _units)
            //{
                
            //}
        }

        public bool CheckForFighters()
        {
            return _units.Count > 0;
        }

        public Unit GetDefender(ref int indexDefender)
        {
            for (int i = 0; i < _units.Count; i++)
            {
                if (_units[i]._specialization.Contains("Защитник"))
                {
                    indexDefender = i;
                    return _units[i];
                }  
            }
            
            return null;
        }

        public bool DefenderisAlive()
        {
            foreach (var unit in _units)
            {
                if (unit._specialization.Contains("Защитник"))
                {
                    return true;
                }
            }

            return false;
        }

        public void FindWoundedFighter()
        {

        }
    }

    class Tavern 
    {
        private List<Unit> _fighers = new List<Unit> { new Warrior(), new Shooter(), new Governor(), new Medic(), new Defender() };

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
