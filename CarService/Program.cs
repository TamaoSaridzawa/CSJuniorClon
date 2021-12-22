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
            CarService carService = new CarService(10000);

            carService.Work();

            Console.ReadKey();
        }
    }

    interface IHasInformation
    {
        void ShowBalance();
    }

    class CarService : IHasInformation
    {
        private int _balance;

        private int _penaltyPrice;

        private List<Detail> _details = new List<Detail>();

        private Queue<Client> _clients = new Queue<Client>();

        public CarService(int balance)
        {
            _penaltyPrice = 5000;
            _balance = balance;
            _details = GetDetails();
            _clients = GetClients();
        }

        public void Work()
        {
            ServeClient();
        }

        public void ShowBalance()
        {
            if (_balance < 0)
            {
                Console.WriteLine($"Пришлось взять кредит . Баланс - {_balance}");
            }
            else
            {
                Console.WriteLine($"Баланс Автосервиса {_balance}");
            }
        }

        private void ServeClient()
        {
            Console.WriteLine("Идет работа автосервиса........");

            while (_clients.Count > 0)
            {
                Console.WriteLine();
                IdentifyMalfunction(_clients.Dequeue());
            }
        }

        private void IdentifyMalfunction(Client client)
        {
            Console.WriteLine($"Автомобиль - {client.Car.Name}, Неисправность - {client.Car.Malfunction}, у клиента {client.Money} денежных средств");

            int indexDetail;

            bool fraud = false;

            if (IsThereDetail(client.Car.Malfunction, out indexDetail, ref fraud))
            {
                if (IsCustomerSolvent(client.Money, _details[indexDetail].GetTotalCost()))
                {
                    MakeProfit(client, _details[indexDetail].GetTotalCost());

                    UsePartRepair(indexDetail);

                    if (fraud)
                    {
                        Console.WriteLine($"После ремонта, автомобиль перестал подавать признаки жизни....\nКлиент потребовал возместить ущерб в размере {client.Car.CostNewPart} рублей");

                        CompensateDamage(client);
                    }
                }
                else
                {
                    Console.WriteLine("У тебя не хватает денег, подкопи и возвращайся!");
                }
            }
            else
            {
                PayFine(client);
            }

            ShowBalance();
            client.ShowBalance();
        }

        private bool IsCustomerSolvent(int clientMoney,int detailPrice)
        {
          return clientMoney >= detailPrice; 
        }

        private void CompensateDamage(Client client)
        {
            _balance -= client.Car.CostNewPart;

            client.ReceiveCompensation(client.Car.CostNewPart);
        }

        private void MakeProfit(Client client,int price)
        {
            _balance += price;

            client.PayRepairs(price);
        }

        private void PayFine(Client client)
        {
            _balance -= _penaltyPrice;

            client.ReceiveCompensation(_penaltyPrice);
        }

        private void UsePartRepair(int index)
        {
            _details.RemoveAt(index);
        }

        private bool IsThereDetail(string malfunction, out int indexDetail, ref bool fraud)
        {
            for (int i = 0; i < _details.Count; i++)
            {
                if (_details[i].Name.Contains(malfunction))
                {
                    Console.WriteLine($"Такая деталь есть в наличии, стоимость ремонта - {_details[i].GetTotalCost()}");

                    indexDetail = i;
                    return true;
                }

                if (i == _details.Count - 1)
                {
                    bool isInputCorrect = false;

                    while (isInputCorrect == false)
                    {
                        Console.Write("Остутствует деталь на складе : Введите 1 - Сказать правду. Введите 2 - обмануть клиента :");

                        string userAnswer = Console.ReadLine();

                        switch (userAnswer)
                        {
                            case "1":
                                Console.WriteLine($"В данный момент, такой детали в наличии нет, но мы готовы выплатить компенсацию в размере {_penaltyPrice} рублей");

                                isInputCorrect = true;
                                break;
                            case "2":
                                Random random = new Random();
                                Console.WriteLine("Все в норме, Покалечим.... ваш авто, можете покурить");

                                fraud = true;

                                indexDetail = random.Next(0, _details.Count);
                                return true;
                            default:
                                Console.WriteLine("Введены некорректные данные");
                                break;
                        }
                    }
                }
            }
            indexDetail = -1;
            return false;
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
            _clients.Enqueue(new Client(new Car("Волга", "Двигатель", 50000), 30000));
            _clients.Enqueue(new Client(new Car("Тойота", "Коробка передач", 20000), 13000));
            _clients.Enqueue(new Client(new Car("Рено", "Тормозные колодки",15000 ), 18000));
            _clients.Enqueue(new Client(new Car("Шкода", "Сцепление",20000 ), 40000));
            _clients.Enqueue(new Client(new Car("Лада", "Аккумулятор", 7000), 20000));

            return _clients;
        }
    }

    class Car
    {
        public string Name { get; private set; }
        public string Malfunction { get; private set; }
        public int CostNewPart { get; private set; }

        public Car(string name, string malfunction,int costNewPart)
        {
            Name = name;
            Malfunction = malfunction;
            CostNewPart = costNewPart;
        }
    }

    class Detail
    {
        public string Name { get; protected set; }
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

    class Client : IHasInformation
    {
        public Car Car { get; private set; }
        public int Money { get; private set; }

        public Client(Car car, int money)
        {
            Car = car;
            Money = money;
        }

        public void ReceiveCompensation(int compensation)
        {
            Money += compensation;
        }

        public void PayRepairs(int price)
        {
            Money -= price;
        }

        public void ShowBalance()
        {
            Console.WriteLine($"Остаток денег у клиента {Money}");
        }
    }
}
