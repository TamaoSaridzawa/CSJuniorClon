using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExplanatoryDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> weekday = new Dictionary<string, string>();

            weekday.Add("1", "Понедельник");
            weekday.Add("2", "Вторник");
            weekday.Add("3", "Среда");
            weekday.Add("4", "Четверг");
            weekday.Add("5", "Пятница");
            weekday.Add("6", "Суббота");
            weekday.Add("7", "Воскресенье");

            DefineDay(weekday);

            Console.ReadKey();
        }

        static void DefineDay(Dictionary<string, string> weekday)
        {
            Console.WriteLine($"Введите номер дня недели от 1  до {weekday.Count}");
            string userInput = Console.ReadLine();

            if (weekday.ContainsKey(userInput))
            {
                Console.WriteLine("День недели: " + weekday[userInput]);
            }
            else
            {
                Console.WriteLine("Нет такого дня недели");
            }
        }
    }
}
