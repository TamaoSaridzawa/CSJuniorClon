using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopServerPlayers
{
    class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();

            database.DetermineTopThreePlayersLevel();

            database.DetermineTopThreePlayersStrength();

            Console.ReadKey();
        }
    }

    class Player
    {
        private string _name;
        public int Level { get; private set; }
        public int Power { get; private set; }

        public Player(string name, int level, int power)
        {
            _name = name;
            Level = level;
            Power = power;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{_name} : Уровень - {Level}, Сила - {Power}");
        }
    }

    class Database
    {
        private List<Player> _players = new List<Player>();

        public Database()
        {
            CreatePlayers();
        }

        public void DetermineTopThreePlayersLevel()
        {
            var filterPlayers = _players.OrderByDescending(player => player.Level).Take(3).ToList();

            Console.WriteLine("Топ 3 игроков по уровню на сегодняшний день :");

            ShowPlayers(filterPlayers);
        }

        public void DetermineTopThreePlayersStrength()
        {
            var filterPlayers = _players.OrderByDescending(player => player.Power).Take(3).ToList();

            Console.WriteLine("Топ 3 игроков по силе на сегодняшний день :");

            ShowPlayers(filterPlayers);
        }

        private void ShowPlayers(List<Player> filterPlayers)
        {
            foreach (var player in filterPlayers)
            {
                player.ShowInfo();
            }
        }

        private void CreatePlayers()
        {
            _players.Add(new Player("Sin", 56, 104));
            _players.Add(new Player("Apex", 66, 99));
            _players.Add(new Player("Tamao", 64, 129));
            _players.Add(new Player("TerPsihora", 44, 74));
            _players.Add(new Player("Friforol", 70, 86));
            _players.Add(new Player("Snegiri", 88, 144));
            _players.Add(new Player("Amoralka", 68, 128));
            _players.Add(new Player("Amstron", 73, 115));
        }
    }
}
