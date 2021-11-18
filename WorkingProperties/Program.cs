using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingProperties
{
    class Program
    {
        static void Main(string[] args)
        {
            char symbol = '%';
            Player player = new Player(1, 5);

            player.DrawPlayer(symbol);

            Player player1 = new Player(player.PositionX + 1, player.PositionY);

            player1.DrawPlayer(symbol);

            player.ChangePosition();

            player.DrawPlayer(symbol);

            Console.ReadKey();
        }
    }

    class Player
    {
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }

        public Player(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }

        public void DrawPlayer(char symbol)
        {
            Console.SetCursorPosition(PositionY,PositionX);
            Console.WriteLine(symbol);
        }

        public void ChangePosition()
        {
            Console.WriteLine("Введите позицию для координаты X");
            PositionX = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите позицию для координаты Y");
            PositionY = int.Parse(Console.ReadLine());
        }
    }
}
