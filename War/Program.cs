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
        }
    }

    abstract class Unit
    {
        public string _specialization { get; private set; }
        protected double Damage;
        protected double Health;
        protected double Armor { get; private set; }

        public Unit(string specialization, double damage, double health, double armor)
        {
            _specialization = specialization;
            Damage = damage;
            Health = health;
            Armor = armor;
        }

        public void TakeDamage(double damage)
        {
            Health -= damage - Armor;
        }

        abstract public void Skill();
    }

    class Warrior : Unit
    {
        public Warrior(string specialization, int damage, int health, int armor) : base(specialization, damage, health, armor)
        {
            
        }

        public override void Skill()
        {
            throw new NotImplementedException();
        }
    }

    class Shooter : Unit
    {
        private double _aimedShot;

        public shooter(string specialization, int damage, int health, int armor) : base(specialization, damage, health, armor)
        {
            Damage = damage * 1.5;
        }

        public override void Skill()
        {
            Health -= _aimedShot - Armor;
        }
    }

    class Governor : Unit
    {
        private double _battleCry;

        public Governor(string specialization, int damage, int health, int armor) : base(specialization, damage, health, armor)
        {
            _battleCry = 30;
        }

        public override void Skill()
        {
            Damage += Damage / 100 / _battleCry ;
        }
    }

    class Medic : Unit
    {
        private double _healingBandage;

        public Medic(string specialization, int damage, int health, int armor) : base(specialization, damage, health, armor)
        {
            _healingBandage = 50;
        }

        public override void Skill()
        {
            Health += _healingBandage;
        }
    }

    class Defender : Unit
    {
        private double _ironSkin;

        public Defender(string specialization, int damage, int health, int armor) : base(specialization, damage, health, armor)
        {
            _healingBandage = 50;
        }

        public override void Skill()
        {
            //Health += _healingBandage;
        }
    }

    class Battlefield
    {
        private List<Unit> _alliance = new List<Unit> {new Warrior("Воин", 200, 1000, 100), new Warrior("Воин", 200, 1000, 100), new Shooter("Стрелок", 300, 800, 70), new Shooter("Стрелок", 300, 800, 70), new Defender("Защитник", 50, 1500, 200) };
        private List<Unit> _elves = new List<Unit>() { new Warrior("Воин", 200, 1000, 100), new Governor("Вревода", 120, 1200, 120), new Shooter("Стрелок", 300, 800, 70), new Medic("Медик", 20, 800, 50), new Defender("Защитник", 50, 1500, 200) };

        private void Fight()
        {
            while (_alliance.Count > 0 && _elves.Count > 0)
            {

            }
        }
    }
}
