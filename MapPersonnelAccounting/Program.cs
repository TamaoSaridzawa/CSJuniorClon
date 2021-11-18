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
            Dictionary<int,string> dosiers = new Dictionary<int, string>();
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
                        AddDosier(dosiers, ref serialNumber);
                        break;
                    case "2":
                        OutputAllDossier(dosiers);
                        break;
                    case "3":
                        RemoveDosier(dosiers);
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

        static void AddDosier(Dictionary<int, string> dosiers, ref int serialNumber)
        {
            Console.Write("Введите инициалы и должность :");
            string inputInitial = Console.ReadLine();

            dosiers.Add(serialNumber++, inputInitial);   

            Console.WriteLine("Пользователь добавлен!");
        }

        static void OutputAllDossier(Dictionary<int, string> dosiers)
        {
            foreach (var item in dosiers)
            {
                Console.WriteLine($"Порядковый номер - {item.Key}, Досье - {item.Value}");
            }
        }

        static void RemoveDosier(Dictionary<int, string> dosiers)
        {
            int number;

            Console.Write("Введите порядковый номер человека, чье досье вы хотите удалить :");
            string index = Console.ReadLine();

            if (int.TryParse(index, out number))
            {
                if (dosiers.ContainsKey(number))
                {
                    dosiers.Remove(number);

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
