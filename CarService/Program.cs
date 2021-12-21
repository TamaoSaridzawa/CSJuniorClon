using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class CarService
    {
        public int Balance { get; private set; }

        private List<Detail> _details = new List<Detail>();

        private Queue<Client> _clients = new Queue<Client>();

        public CarService(int balance)
        {
            Balance = balance;
            _details = GetDetails();
            _clients = GetClients();
        }

        private List<Detail> GetDetails()
        {
            _details.Add(new Engine());
            _details.Add(new Battery());
            _details.Add(new BrakePads());
            _details.Add(new Engine());
            _details.Add(new BrakePads());
            _details.Add(new Transmission());
            _details.Add(new Clutch());

            return _details;
        }

        private Queue<Client> GetClients()
        {
            _clients.Enqueue(new Client(new Car("Волга", "Двигатель"), 30000));
            _clients.Enqueue(new Client(new Car("Тойота", "Двигатель"), 13000));
            _clients.Enqueue(new Client(new Car("Рено", "Тормозные колодки"), 18000));
            _clients.Enqueue(new Client(new Car("Шкода", "Двигатель"), 40000));
            _clients.Enqueue(new Client(new Car("Лада", "Тормозные колодки"), 20000));

            return _clients;
        }
    }

    class Car
    {
        public string Name { get; set; }
        public string Malfunction { get; set; }

        public Car(string name, string malfunction)
        {
            Name = name;
            Malfunction = malfunction;
        }
    }

    class Detail
    {
        public string Name { get; set; }
        public int Price { get; protected set; }
        public int InstallationPrice { get; protected set; }

        public Detail()
        {

        }

        public int GetTotalCost()
        {
            return Price + InstallationPrice;
        }
    }

    class Engine : Detail
    {
        public Engine(): base()
        {
            Name = "Двигатель";
            Price = 20000;
            InstallationPrice = 12000;
        }
    }

    class BrakePads : Detail
    {
        public BrakePads(): base()
        {
            Name = "Торозные колодки";
            Price = 3000;
            InstallationPrice = 1800;
        }
    }

    class Transmission : Detail
    {
        public Transmission(): base()
        {
            Name = "Коробка передач";
            Price = 7000;
            InstallationPrice = 3500;
        }
    }

    class Battery : Detail
    {
        public Battery() : base()
        {
            Name = "Аккумулятор";
            Price = 3000;
            InstallationPrice = 1000;
        }
    }

    class Clutch : Detail
    {
        public Clutch() : base()
        {
            Name = "Сцепление";
            Price = 4500;
            InstallationPrice = 3000;
        }
    }

   class Client
    {
        public Car Car { get; private set; }
        public int Money { get; private set; }

        public Client(Car car, int money)
        {
            Car = car;
            Money = money;
        }
    }
}
