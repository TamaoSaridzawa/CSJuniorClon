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
            Renderer renderer = new Renderer();

            Player player = new Player(1, 9, '@') ;

            Player player1 = new Player(2, 9, '!');

            renderer.Draw(player.PositionX, player.PositionY, player.Symbol);

            renderer.Draw(player1.PositionX, player1.PositionY, player1.Symbol);

            player.ChangeСoordinates();

            renderer.Draw(player.PositionX, player.PositionY, player.Symbol);

            Console.ReadKey();
        }
    }

    class Player
    {
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
        public char Symbol { get; private set ; }

        public Player(int x, int y, char symbol)
        {
            PositionX = x;
            PositionY = y;
            Symbol = symbol;
        }

        public void ChangeСoordinates()
        {
            int number1;
            int number2;

            Console.WriteLine("Введите позицию для координаты X и Y");

            if (int.TryParse(Console.ReadLine(), out number1) && int.TryParse(Console.ReadLine(), out number2))
            {
                PositionX = number1;
                PositionY = number2;
            }
            else
            {
                Console.WriteLine("Введены некорректные данные");
            }
        }
    }

    class Renderer
    {
        public void Draw(int x, int y, char symbol)
        {
            Console.SetCursorPosition(y,x);
            Console.WriteLine(symbol);
        }
    }
}
