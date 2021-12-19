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
            Battlefield battlefield = new Battlefield();
            battlefield.Fight();
            Console.ReadKey();
        }
    }

    abstract class Unit
    {
        public int Number { get; protected set; }
        public string Name { get; protected set; }
        public int ChanceTriggeringSuperpowers { get; protected set; }
        public string Specialization { get; protected set; }
        public double Damage { get; protected set; }
        public double Health { get; protected set; }
        public double Armor { get; protected set; }

        public Unit(string name)
        {
            Name = name;
        }

        public abstract void DescribeAbility();

        public abstract  void UseSuperAbility(Unit unit, List<Unit> units);

        virtual public bool IsIgnorTank()
        {
            return false;
        }

        public void ShowStats()
        {
            Console.WriteLine($"Номер бойца {Number}, {Specialization}, Урон {Damage}, ЗДоровье {Health}, Броня {Armor}");
        }

        public void GiveHealing(double health)
        {
            Health += health;
            Console.WriteLine($"{Name} , {Specialization} Получил {health} здоровья");
        }

        public void BoostAttack(double damage)
        {
            Damage += damage;
        }

        public bool IsDied()
        {
            if (Health <= 0)
            {
                Console.WriteLine($"{Specialization} убит");
                return true;
            }
            else return false;
        }

        public void Attack(Unit unit, int procent, List<Unit> friendlyUnits)
        {
           if (procent < ChanceTriggeringSuperpowers)
           {
                UseSuperAbility(unit, friendlyUnits);
           }
           else
           {
                unit.TakeDamage(Damage);
           }
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
            Number = 1;
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

        public override void UseSuperAbility(Unit enemyUnit, List<Unit> friendlyUnits)
        {
            Console.WriteLine($"{Name} ,{Specialization} Использовал 'бешенство, атака увеличена'");

            Damage += Damage / 100 * _rabies;

            enemyUnit.TakeDamage(Damage);
        }
    }

    class Shooter : Unit
    {
        private double _aimedShot;

        public Shooter(string name) : base(name)
        {
            Number = 2;
            Specialization = "Стрелок";
            Damage = 400;
            Health = 800;
            Armor = 70;
            ChanceTriggeringSuperpowers = 30;
            _aimedShot = Damage * 1.5;
        }

        public override bool IsIgnorTank()
        {
            return true;
        }

        public override void DescribeAbility()
        {
            Console.WriteLine($"С шансом {ChanceTriggeringSuperpowers} наносит в полтора раза больше урона, Полностью игнорирует ЗАщитника вражесского отряда");
        }

        public override void UseSuperAbility(Unit enemyUnit, List<Unit> friendlyUnits)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{Name} ,{Specialization} Использовал 'Выстрел дуплетом' ");

            enemyUnit.TakeDamage(_aimedShot);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

    class Governor : Unit
    {
        private double _battleCry;
        private double _firstAidKit;

        public Governor(string name) : base(name)
        {
            Number = 3;
            Specialization = "Воевода";
            Damage = 290;
            Health = 1200;
            Armor = 120;
            ChanceTriggeringSuperpowers = 40;
            _battleCry = 20;
            _firstAidKit = 30;
        }

        private void IncreaseAllyAttack(List<Unit> friendlyUnits)
        {
            foreach (var unit in friendlyUnits)
            {
                unit.BoostAttack(_battleCry);
            }
        }

        public override void DescribeAbility()
        {
            Console.WriteLine($"С шансом {ChanceTriggeringSuperpowers} исцеляет себя на {_firstAidKit} ед. здоровья, Так же увеличивает урон всей группы на {_battleCry} ед. Является лидером группы");
        }

        public override void UseSuperAbility(Unit enemyUnit, List<Unit> friendlyUnits)
        {
            Console.WriteLine($"{Name} ,{Specialization} использовал аптечку, урон группы увеличен на {_battleCry}");

            IncreaseAllyAttack(friendlyUnits);

            Health += _firstAidKit;

            enemyUnit.TakeDamage(Damage);
        }
    }

    class Medic : Unit
    {
        private double _healingBandage;

        public Medic(string name) : base(name)
        {
            Number = 4;
            Specialization = "Медик";
            Damage = 220;
            Health = 900;
            Armor = 50;
            ChanceTriggeringSuperpowers = 80;
            _healingBandage = 70;
        }

        public override void DescribeAbility()
        {
            Console.WriteLine($"С шансом {ChanceTriggeringSuperpowers} процентов исцеляет союзника на {_healingBandage}. Является хилом группы");
        }

        private void HealAlly(List<Unit> friendlyUnits)
        {
            Random random = new Random();

            friendlyUnits[random.Next(0, friendlyUnits.Count)].GiveHealing(_healingBandage);
        }

        public override void UseSuperAbility(Unit enemyUnit, List<Unit> friendlyUnits)
        {
            Console.WriteLine($"{Name}, {Specialization} использовал исцелени");

            HealAlly(friendlyUnits);

            enemyUnit.TakeDamage(Damage);
        }
    }

    class Defender : Unit
    {
        private double _spikedArmor;

        public Defender(string name) : base(name)
        {
            Number = 5;
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

        public override void UseSuperAbility(Unit enemyUnit, List<Unit> friendlyUnits)
        {
            _spikedArmor = Damage + (enemyUnit.Damage / 3);

            Console.WriteLine($"{Name}, {Specialization} использовал 'Шипастый танк'");

            enemyUnit.TakeDamage(_spikedArmor);
        }
    }

    class Battlefield
    {
        private Detachment _detachment = new Detachment();
        private Detachment _detachment2 = new Detachment();

        private Random _random = new Random();

        public void Fight()
        {
            while (_detachment.IsAlive() && _detachment2.IsAlive() )
            {
                _detachment.JoinBattle(_detachment2.GetUnits(), _random);

                Console.WriteLine("---------------------------");

                _detachment2.JoinBattle(_detachment.GetUnits(), _random);

                Console.WriteLine("---------------------------");
            }

            if (!_detachment.IsAlive())
            {
                Console.WriteLine($"Армия {_detachment.Name} пала");
            }
            else if (!_detachment2.IsAlive())
            {
                Console.WriteLine($"Армия {_detachment2.Name} пала");
            }
        }
    }

    class Tavern
    {
        List<Unit> _fighers = new List<Unit> { new Warrior($"Боец"), new Shooter("Боец"), new Governor("Боец"), new Medic("Боец"), new Defender("Боец") };

        public void ShowFigters()
        {
            Console.WriteLine("Сейчас в таверне есть следующие бойцы : ");

            foreach (var figter in _fighers)
            {
                figter.ShowStats();
                figter.DescribeAbility();
            }
        }

        public Unit GetUnit(int number)
        {
            switch (number)
            {
                case 1:
                    return new Warrior(Console.ReadLine());
                case 2:
                    return new Shooter(Console.ReadLine());
                case 3:
                    return new Governor(Console.ReadLine());
                case 4:
                    return new Medic(Console.ReadLine());
                case 5:
                    return new Defender(Console.ReadLine());
            }

            return null;
        }
    }

    class Detachment
    {
        private List<Unit> _units = new List<Unit>();
        private int _size = 5;
        private Tavern _tavern = new Tavern();
        public string Name { get; private set; }

        public Detachment()
        {
            _tavern.ShowFigters();

            Console.Write("Введите имя вашей армии :");
            Name = Console.ReadLine();

            Create();
        }

        public List<Unit> GetUnits()
        {
            return _units;
        }

        public void JoinBattle(List<Unit> units, Random random)
        {
            Console.WriteLine($"Армия {Name} атакована");

            for (int i = 0; i < units.Count; i++)
            {
                int indexUnit = random.Next(0, _units.Count());

                int procent = random.Next(1, 101);

                if (IsDefenderAlive() && !units[i].IsIgnorTank())
                {
                    units[i].Attack(GetDefender(out indexUnit), procent, units);
                }
                else
                {
                    units[i].Attack(_units[indexUnit], procent, units);
                }

                if (_units[indexUnit].IsDied())
                {
                    _units.RemoveAt(indexUnit);
                }

                if (_units.Count == 0)
                {
                    break;
                }
            }
        }

        public bool IsAlive()
        {
            return _units.Count > 0;
        }

        private void Create()
        {
            while (_units.Count < _size)
            {
                Console.Write("ВЫберете бойца :");

                int number;

                if (int.TryParse(Console.ReadLine(), out number) && number > 0 && number <= _size)
                {
                    _units.Add(_tavern.GetUnit(number));
                }
                else
                {
                    Console.WriteLine("Такого номера не существует");
                }
            }
        }

        private Unit GetDefender(out int indexDefender)
        {
            for (int i = 0; i < _units.Count; i++)
            {
                if (_units[i].Specialization.Contains("Защитник"))
                {
                    indexDefender = i;
                    return _units[i];
                }
            }

            indexDefender = -1;
            return null;
        }

        private bool IsDefenderAlive()
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
}
