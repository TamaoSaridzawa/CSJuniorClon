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
            Renderer player = new Renderer(new Player(2, 4, '@'));

            player.Draw();

            Renderer player1 = new Renderer(new Player (5, 1, '!'));

            player1.Draw();

            player.ChangePosition();

            player.Draw();

            Console.ReadKey();
        }
    }

    class Player
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public char Symbol { get; set ; }

        public Player(int positionX, int positionY, char symbol)
        {
            PositionX = positionX;
            PositionY = positionY;
            Symbol = symbol;
        }
    }

    class Renderer
    {
        public Player Player { get; private set; }

        public Renderer(Player player)
        {
            Player = player;
        }

        public void Draw()
        {
            Console.SetCursorPosition(Player.PositionY,Player.PositionX);
            Console.WriteLine(Player.Symbol);
        }

        public void ChangePosition()
        {
            int number1;
            int number2;

            Console.WriteLine("Введите позицию для координаты X и Y");

            if (int.TryParse(Console.ReadLine(), out number1) && int.TryParse(Console.ReadLine(), out number2))
            {
                Player.PositionX = number1;
                Player.PositionY = number2;
            }
            else
            {
                Console.WriteLine("Введены некорректные данные");
            }
        }
    }
}
