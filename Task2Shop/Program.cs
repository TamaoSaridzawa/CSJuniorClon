using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task2Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            Good iPhone12 = new Good("IPhone 12");
            Good iPhone11 = new Good("IPhone 11");

            Warehouse warehouse = new Warehouse();

            Shop shop = new Shop(warehouse);

            warehouse.Delive(iPhone12, 10);
            warehouse.Delive(iPhone11, 1);

            warehouse.ShowRemains();

            Cart cart = shop.Cart();

            cart.Add(iPhone12, 4);
            cart.Add(iPhone11, 3);

            cart.ShowGood();

            Console.WriteLine(cart.Order());

            cart.Add(iPhone12, 9);

            warehouse.ShowRemains();

            Console.ReadKey();
        }
    }

    class Good
    {
        public string Name { get; private set; }
        public int Counte { get; private set; }

        public Good(string name)
        {
            Name = name;
        }

        public void SetCounte(int counte)
        {
            if (counte <= 0)
            {
                Console.WriteLine("Введены некорректные данные");
            }
            else
            {
                Counte = counte;
            }
        }

        public void ChangeCounte(int counte)
        {
            Counte -= counte;
        }
    }

    class Shop
    {
        private Warehouse _wareHouse;

        private Cart _cart = new Cart();

        public Shop(Warehouse wareHouse)
        {
            _wareHouse = wareHouse;
        }

        public Cart Cart()
        {
            _cart.AcceptProduct(_wareHouse.SendProductStore());
            return _cart;
        }
    }

    class Warehouse
    {
        private readonly List<Good> _goods = new List<Good>();

        public void Delive(Good good, int counte)
        {
            good.SetCounte(counte);
            _goods.Add(good);
        }

        public void ShowRemains()
        {
            foreach (var good in _goods)
            {
                Console.WriteLine($"{good.Name} : Остаток на складе - {good.Counte}");
            }
        }

        public IReadOnlyList<Good> SendProductStore()
        {
            return _goods;
        }
    }

    class Cart
    {
        private IReadOnlyList<Good> _allgoodsShop = new List<Good>();

        private List<Good> _goodBuyers = new List<Good>();

        public void AcceptProduct(IReadOnlyList <Good> goods)
        {
            _allgoodsShop = goods;
        }

        public void Add(Good good, int counte)
        {
            foreach (var goodShop in _allgoodsShop)
            {
                if (good == goodShop)
                {
                    if (good.Counte >= counte)
                    {
                        good.ChangeCounte(counte);
                        Good newGood = new Good(good.Name);
                        newGood.SetCounte(counte);
                        _goodBuyers.Add(newGood);
                    }
                    else
                    {
                        Console.WriteLine($"{good.Name} : Ошибка, нет нужного колличества на складе");
                    }
                }
            }
        }

        public void ShowGood()
        {
            Console.WriteLine("Ваша корзина : ");

            foreach (var good in _goodBuyers)
            {
                Console.Write($"{good.Name} : Количество - {good.Counte} \n");
            }
        }

        public string Order()
        {
            return "Оплата прошла успешно";
        }
    }
}
