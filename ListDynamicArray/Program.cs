using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListDynamicArray
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> array = new List<int>();
            string userInput = "";
            int number;
            int result;

            Console.WriteLine("Для выхода из программы нажмите 'exit'\nДля вывода суммы введенных чисел нажмите 'sum'");

            while (userInput != "exit")
            {
                Console.Write("Введите данные: ");
                userInput = Console.ReadLine();

                if (int.TryParse(userInput, out number))
                {
                    array.Add(number);
                }
                else if (userInput == "sum")
                {
                  result = Fold(array);
                }
                else if (userInput == "exit")
                {
                    Console.WriteLine("Всего доброго!");
                }
                else
                {
                    Console.WriteLine("Некорректный ввод данных, попробуйте еще раз.");
                }
            }
        }

        static int Fold(List<int> array)
        {
            int sum = 0;

            foreach (int element in array)
            {
                sum += element;
            }

            Console.WriteLine($"Сумма чисел массима = {sum}");

            return sum;
        }
    }
}
