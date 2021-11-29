using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainConfigurator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветствует конфигуратор пассажирских поездов");
            Console.WriteLine("Нажмите любую клавишу для начала работы :");
            Console.ReadKey();

            bool isWorkRailwayStation = true;

            RailwayStation railwayStation = new RailwayStation();

            while (isWorkRailwayStation)
            {
                railwayStation.WorkOffice();

                Console.WriteLine("Для выхода из программы нажмите 'e', либо любую другую клавишу для формирования нового маршрута");

                string userInput = Console.ReadLine();

                if (userInput == "e")
                {
                    isWorkRailwayStation = false;
                }
                Console.Clear();
            }
        }
    }

    class RailwayStation
    {
        private Queue<Passenger> _passengers = new Queue<Passenger>();
        private List<Vagon> _vagons = new List<Vagon>();

        private Random _random = new Random();

        private string _route;

        public void WorkOffice()
        {
            CreateRoute();
            CreatePassengers();
            CreateTrain();
            ShowInfo();
            SendTrain();
        }

        private void CreateRoute()
        {
            Console.Write("Укажите точку отправления: ");
            string pointDeparture = Console.ReadLine();

            Console.Write("Укажите точку прибытия: ");
            string pointArrival = Console.ReadLine();

            Console.Clear();

            _route = pointDeparture + " - " + pointArrival;
            Console.WriteLine("Маршрут создан!");
        }

        private void CreatePassengers()
        {
            int passengersCounte = _random.Next(5, 30);

            for (int i = 0; i < passengersCounte; i++)
            {
                _passengers.Enqueue(new Passenger(_random.Next(500, 1500)));
            }

            Console.WriteLine($"Количество пассажиров : {passengersCounte}");
        }

        private void CreateTrain()
        {
            int maxSeatsCounte = 0;
            int number = 1;

            while (_passengers.Count > maxSeatsCounte)
            {
                _vagons.Add(new Vagon(_random.Next(2, 6), number++));
                maxSeatsCounte += _vagons[_vagons.Count - 1].QuantitySeats;
            }

            for (int i = 0; i < _vagons.Count; i++)
            {
                for (int j = 0; j < _vagons[i].QuantitySeats; j++)
                {
                    if (_passengers.Count > 0)
                    {
                        _vagons[i].SeatPassenger(_passengers.Dequeue(), j);
                    }
                }
            }
        }

        private void ShowInfo()
        {
            Console.WriteLine($"Рейс : {_route} отправляется.");
            Console.WriteLine($"Поезд состоит из {_vagons.Count} вагонов");
            Console.WriteLine();

            foreach (var vagon in _vagons)
            {
                Console.WriteLine($"Вагон № {vagon.Number} : Количество мест -{vagon.QuantitySeats}");

                vagon.ShowPassengers();
            }
        }

        private void SendTrain()
        {
            _vagons.Clear();
        }
    }

    class Passenger
    {
        public int TicketNumber { get; private set; }

        public Passenger(int ticketNumber)
        {
            TicketNumber = ticketNumber;
        }
    }

    class Vagon
    {
        private Passenger[] _passengers;
        public int Number { get; private set; }
        public int QuantitySeats { get; private set; }

        public Vagon(int quantitySeats, int numberVagon)
        {
            QuantitySeats = quantitySeats;
            Number += numberVagon;
            _passengers = new Passenger[QuantitySeats];
        }

        public void ShowPassengers()
        {
            for (int i = 0; i < _passengers.Length; i++)
            {
                if (_passengers[i] != null)
                {
                    Console.WriteLine($"\tПассажир № {i + 1}. Билет № {_passengers[i].TicketNumber}");
                }
                else
                {
                    Console.WriteLine("\tСвободное место");
                }
            }
        }

        public void SeatPassenger(Passenger passenger, int numberSeats)
        {
            _passengers[numberSeats] = passenger;
        }
    }
}
