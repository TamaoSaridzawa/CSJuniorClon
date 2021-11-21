﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            int counteNumber = 1;
            bool operationDatabase = true;
            DataBase dataBase = new DataBase();
            Console.WriteLine("Для добавления нового игрока нажмите '1'\nДля блокировки игрока нажмите '2'\nДля разблокировки игрока нажмите '3'" +
                "\nДля вывода информации о игроках нажмите '4'\nДля улаоение игрока нажмите '5'\nДля выхода нажмите '6'");

            while (operationDatabase)
            {
                Console.Write("Введите команду :");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        dataBase.Add(ref counteNumber);
                        break;
                    case "2":
                        dataBase.Ban();
                        break;
                    case "3":
                        dataBase.UnBan();
                        break;
                    case "4":
                        dataBase.ShowInfo();
                        break;
                    case "5":
                        dataBase.Remove();
                        break;
                    case "6":
                        operationDatabase = false;
                        break;
                    default:
                        Console.WriteLine("Неверный ввод команды");
                        break;
                }
            }
        }
    }

    class Player
    {
        public int SerialNumber { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; }
        private bool _banned;

        public Player(int serialNumber)
        {
            SerialNumber = serialNumber;

            Console.Write("Введите имя персонажа :");
            Name = Console.ReadLine();
            Level = 1;
            _banned = false;
        }

        public bool Banned
        {
            get
            {
                return _banned;
            }
            set
            {
                _banned = value;
            }
        }
    }

    class DataBase
    {
        private List<Player> Players = new List<Player>();

        private int _number;

        public void Add(ref int counte)
        {
            Players.Add(new Player(counte++));
            Console.WriteLine("Пользователь добавлен.");
        }

        public void Ban()
        {
            if (Convert(ref _number))
            {
                foreach (var player in Players)
                {
                    if (_number == player.SerialNumber)
                    {
                        player.Banned = true;
                        Console.WriteLine($"Персонаж заблокирован");
                    }
                }
            }
        }

        public void UnBan()
        {
            if (Convert(ref _number))
            {
                foreach (var player in Players)
                {
                    if (_number == player.SerialNumber)
                    {
                        player.Banned = false;
                        Console.WriteLine("Персонаж разблокирован");
                    }
                }
            }
        }

        public void Remove()
        {
            if (Convert(ref _number))
            {
                for (int i = 0; i < Players.Count; i++)
                {
                    if (_number == Players[i].SerialNumber)
                    {
                        Players.RemoveAt(i);
                        Console.WriteLine("Игрок удален");
                    }
                }
            }
        }

        public void ShowInfo()
        {
            foreach (var player in Players)
            {
                Console.WriteLine($"Порядковый номер - {player.SerialNumber}; Имя - {player.Name}; Уровень - {player.Level}; Блокировка - {player.Banned}");
            }
        }

        public bool Convert(ref int _number)
        {
            Console.Write("Введите порядковый номер :");

            if (int.TryParse(Console.ReadLine(), out _number) && _number > 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Введены некорректные данные!");
                return false;
            }
        }
    }
}