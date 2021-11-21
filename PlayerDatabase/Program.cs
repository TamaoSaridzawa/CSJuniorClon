using System;
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
            bool isOperationDatabase = true;
            Database dataBase = new Database();

            Console.WriteLine("Для добавления нового игрока нажмите '1'\nДля блокировки игрока нажмите '2'\nДля разблокировки игрока нажмите '3'" +
                "\nДля вывода информации о игроках нажмите '4'\nДля удаление игрока нажмите '5'\nДля выхода нажмите '6'");

            while (isOperationDatabase)
            {
                Console.Write("Введите команду :");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        dataBase.Add();
                        break;
                    case "2":
                        dataBase.BanPlayer();
                        break;
                    case "3":
                        dataBase.UnBanPlayer();
                        break;
                    case "4":
                        dataBase.ShowInfo();
                        break;
                    case "5":
                        dataBase.RemovePlayer();
                        break;
                    case "6":
                        isOperationDatabase = false;
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
        public static int CounteNumber;

        public int SerialNumber { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; }
        private bool _banned;

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

        public Player()
        {
            SerialNumber = ++CounteNumber;

            Console.Write("Введите имя персонажа :");
            Name = Console.ReadLine();
            Level = 1;
            _banned = false;
        }
    }

    class Database
    {
        private List<Player> _players = new List<Player>();

        int number;

        Player player;

        public void Add()
        {
            _players.Add(new Player());
            Console.WriteLine("Пользователь добавлен.");
        }

        public void BanPlayer()
        {
            if (TryReadInt(out number))
            {
                if (TryGetPlayer(out player))
                {
                    player.Banned = true;
                    Console.WriteLine($"Персонаж заблокирован");
                }
                else
                {
                    InputMessage();
                }
            }
        }

        public void UnBanPlayer()
        {
            if (TryReadInt(out number))
            {
                if (TryGetPlayer(out player))
                {
                    player.Banned = false;
                    Console.WriteLine("Персонаж разблокирован");
                }
                else
                {
                    InputMessage();
                }
            }
        }

        public void RemovePlayer()
        {
            if (TryReadInt(out number))
            {
                if (TryGetPlayer(out player))
                {
                    _players.Remove(player);
                    Console.WriteLine("Игрок удален");
                }
                else
                {
                    InputMessage();
                }
            }
        }

        public void ShowInfo()
        {
            foreach (var player in _players)
            {
                Console.WriteLine($"Порядковый номер - {player.SerialNumber}; Имя - {player.Name}; Уровень - {player.Level}; Блокировка - {player.Banned}");
            }
        }

        public bool TryReadInt(out int number)
        {
            Console.Write("Введите порядковый номер :");

            if (int.TryParse(Console.ReadLine(), out number) && number > 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Введены некорректные данные!");
                return false;
            }
        }

        private bool TryGetPlayer(out Player player)
        {
            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].SerialNumber == number)
                {
                    player = _players[i];
                    return true;
                }
            }
            player = null;
            return false;
        }

        private void InputMessage()
        {
            Console.WriteLine("Не существует игрока с таким номером");
        }
    }
}
