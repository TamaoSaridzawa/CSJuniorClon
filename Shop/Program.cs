using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("Job", 1400);
            Seller seller = new Seller(0);
            bool isDeal = true;

            Console.WriteLine("Нажмите :");
            Console.WriteLine($"1 - открытие своего инвентаря\n2 - обзор товара продавца\n3 - покупка товара\n4 - выход из магазина");

            while (isDeal)
            {
                Console.Write("Введите команду: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        player.ShowItems();
                        break;
                    case "2":
                        seller.ShowItems();
                        break;
                    case "3":
                        seller.Sell(player);
                        break;
                    case "4":
                        isDeal = false;
                        break;
                    default:
                        Console.WriteLine("Введены некорректные данные");
                        break;
                }
            }
        }
    }

    abstract class Character
    {
        protected List<Item> Items = new List<Item>();
        public int Money { get;  set; }

        public Character(int money)
        {
            Money = money;
        }

        abstract public void ShowItems();
    }

    class Item
    {
        public string Name { get; private set; }
        public int Price { get; private set; }

        public Item(string name, int price)
        {
            Name = name;
            Price = price;
        }
    }

    class Player : Character
    {
        public string Name { get; private set; }

        public Player( string name, int money) : base (money) 
        {
            Name = name;
        }

        public void Bay(int price, Item item)
        {
            Money -= price;
            Items.Add(item);
        }

        override public void  ShowItems()
        {
            Console.WriteLine($"Денег в кармане - {Money}");

            if (Items.Count <= 0)
            {
                Console.WriteLine("В рюкзаке пусто ^_^");
            }
            else
            {
                Console.WriteLine("Вещи в рюкзаке:");

                foreach (var item in Items)
                {
                    Console.WriteLine($"{item.Name}");
                }
            }   
        }
    }

    class Seller : Character
    {
        public Seller(int money) : base(money)
        {
            ExposeItems();
        }

        public void Sell(Player player)
        {
            Console.Write("Какой товар вам продать? ");
            string userInput = Console.ReadLine();

            if (FindItem(userInput, out Item item))
            {
                if (player.Money >= item.Price)
                {
                    player.Bay(item.Price, item);

                    Money += item.Price;

                    Console.WriteLine("Поздравляю с покупкой!");

                    Items.Remove(item);
                }
                else
                {
                    Console.WriteLine("У вас недостаточно средств");
                }
            }
            else
            {
                Console.WriteLine("Такого товара у меня нет!");
            }
        }

        override public void ShowItems()
        {
            Console.WriteLine($"Денежные средства магазина :{Money}");

            if (Items.Count > 0)
            {
                Console.WriteLine("Сейчас у продавца есть следуюшие товары.");

                foreach (var item in Items)
                {
                    Console.WriteLine($"{item.Name}, цена - {item.Price}");
                }
            }
            else
            {
                Console.WriteLine("Товара больше нет.");
            }
        }

        private bool FindItem(string userInput, out Item item)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (userInput.ToLower() == Items[i].Name.ToLower())
                {
                    item = Items[i];
                    return true;
                } 
            }

            item = null;
            return false;
        }

        private void ExposeItems()
        {
            Items.Add(new Item("Хлеб", 30));
            Items.Add(new Item("Аптечка", 60));
            Items.Add(new Item("Водка", 40));
            Items.Add(new Item("Винтовка", 1200));
            Items.Add(new Item("Пистолет", 600));
            Items.Add(new Item("Дробовик", 800));
        }
    }
}



