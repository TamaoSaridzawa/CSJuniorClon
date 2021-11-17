using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapPersonnelAccounting
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int,string> dosier = new Dictionary<int, string>();
            string userInput = "";
            int serialNumber = 1;

            Console.WriteLine("Меню :");

            while (userInput != "4")
            {
                Console.WriteLine($"1) добавить досье\n2) Вывысти все досье\n3) Удалить досье\n4) Выход");
                Console.Write("Выберите пункт :");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        AddDosier(ref dosier, ref serialNumber);
                        break;
                    case "2":
                        OutputDossier(dosier);
                        break;
                    case "3":
                        RemoveDosier(ref dosier);
                        break;
                    case "4":
                        break;
                    default:
                        Console.WriteLine("Неверный ввод данных");
                        break;
                }
            }

            Console.ReadKey();
        }

        static void AddDosier(ref Dictionary<int, string> dosier, ref int serialNumber)
        {
            Console.Write("Введите инициалы и должность :");
            string inputInitial = Console.ReadLine();

            dosier.Add(serialNumber++, inputInitial);   

            Console.WriteLine("Пользователь добавлен!");

        }

        static void OutputDossier(Dictionary<int, string> dosier)
        {
            foreach (var item in dosier)
            {
                Console.WriteLine($"Порядковый номер - {item.Key}, Досье - {item.Value}");
            }
        }

        static void RemoveDosier(ref Dictionary<int, string> dosier)
        {
            int number;

            Console.Write("Введите порядковый номер человека, чье досье вы хотите удалить :");
            string index = Console.ReadLine();

            if (int.TryParse(index, out number))
            {
                if (dosier.ContainsKey(number))
                {
                    dosier.Remove(number);

                    Console.WriteLine("Пользователь удален");
                }
                else
                {
                    Console.WriteLine("Пользователя с таким номером не существует!");
                }
            }
            else
            {
                Console.WriteLine("Введены некорректные данные");
            }
        }
    }
}
