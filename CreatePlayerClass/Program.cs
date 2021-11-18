using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatePlayerClass
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("Маг", 100, 100, 20, "Жезл");

            player.ShowInfo();

            Console.ReadKey();
        }

        class Player
        {
            private string _specialization;
            private int _damage;
            private int _health;
            private int _armor;
            private string _weapon;

            public Player(string specialization, int damage, int health, int armor, string weapon)
            {
                _specialization = specialization;
                _damage = damage;
                _health = health;
                _armor = armor;
                _weapon = weapon;
            }

            public void ShowInfo()
            {
                Console.WriteLine($"Специализация - {_specialization}\nУрон - {_damage}\nЗдоровье - {_health}\nБроня - {_armor}\nОружие - {_weapon} ");
            }
        }
    }
}
