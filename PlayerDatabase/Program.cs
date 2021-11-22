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
            Database database = new Database();

            Console.WriteLine("Для добавления нового игрока нажмите '1'\nДля блокировки игрока нажмите '2'\nДля разблокировки игрока нажмите '3'" +
                "\nДля вывода информации о игроках нажмите '4'\nДля удаление игрока нажмите '5'\nДля выхода нажмите '6'");

            while (isOperationDatabase)
            {
                Console.Write("Введите команду :");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        database.AddPlayer();
                        break;
                    case "2":
                        database.BanPlayer();
                        break;
                    case "3":
                        database.UnBanPlayer();
                        break;
                    case "4":
                        database.ShowInfo();
                        break;
                    case "5":
                        database.RemovePlayer();
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
        private static int CounteNumber;

        public bool Banned { get; private set; }
        public int SerialNumber { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; }
       

        public Player()
        {
            SerialNumber = ++CounteNumber;

            Console.Write("Введите имя персонажа :");
            Name = Console.ReadLine();
            Level = 1;
            Banned = false;
        }

        public void Ban()
        {
            Banned = true;
        }

        public void UnBan()
        {
            Banned = false;
        }
    }

    class Database
    {
        private List<Player> _players = new List<Player>();

        public void AddPlayer()
        {
            _players.Add(new Player());
            Console.WriteLine("Пользователь добавлен.");
        }

        public void BanPlayer()
        {
            if (TryReadInt(out  int number))
            {
                if (TryGetPlayer(out Player player, number))
                {
                    player.Ban();
                    Console.WriteLine($"Персонаж заблокирован");
                }
                else
                {
                    ShowErrorMessage();
                }
            }
        }

        public void UnBanPlayer()
        {
            if (TryReadInt(out int number))
            {
                if (TryGetPlayer(out Player player, number))
                {
                    player.UnBan();
                    Console.WriteLine("Персонаж разблокирован");
                }
                else
                {
                    ShowErrorMessage();
                }
            }
        }

        public void RemovePlayer()
        {
            if (TryReadInt(out int number))
            {
                if (TryGetPlayer(out Player player, number))
                {
                    _players.Remove(player);
                    Console.WriteLine("Игрок удален");
                }
                else
                {
                    ShowErrorMessage();
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

        private bool TryReadInt(out int number)
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

        private bool TryGetPlayer(out Player player, int number)
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

        private void ShowErrorMessage()
        {
            Console.WriteLine("Не существует игрока с таким номером");
        }
    }
}
