﻿using System;
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

            while (isWorkRailwayStation)
            {
                RailwayStation boxOffice = new RailwayStation();
                boxOffice.WorkOffice(ref isWorkRailwayStation);
            }
        }

        class RailwayStation
        {
            private Queue<Passenger> _passengers = new Queue<Passenger>();
            private List<Vagon> _train = new List<Vagon>();

            private Random _random = new Random();

            private string _route;

            public void WorkOffice(ref bool isWorkOffice)
            {
                CreateRoute();
                CreatePassengers();
                CreateTrain();
                ShowInfo();

                Console.WriteLine("Для выхода из программы нажмите 'e', либо любую другую клавишу для формирования нового маршрута");

                string userInput = Console.ReadLine();

                if (userInput == "e")
                {
                    isWorkOffice = false;
                }

                Console.Clear();
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
                    _train.Add(new Vagon(_random.Next(2, 6), number++));
                    maxSeatsCounte += _train[_train.Count - 1].NumberSeats; 
                }

                for (int i = 0; i < _train.Count; i++)
                {
                    _train[i].SeatPassenger(_passengers);
                }
            }

            private void ShowInfo()
            {
                Console.WriteLine($"Рейс : {_route} отправляется.");
                Console.WriteLine($"Поезд состоит из {_train.Count} вагонов");
                Console.WriteLine();

                foreach (var vagon in _train)
                {
                    Console.WriteLine($"Вагон № {vagon.NumberVagon} : Количество мест -{vagon.NumberSeats}");

                    vagon.ShowPassengers();
                }
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
            public int NumberVagon { get; private set; }
            private Passenger[] _passengersVagons;
            public int NumberSeats { get; private set; }

            public Vagon(int numberSeats, int numberVagon)
            {
                NumberVagon += numberVagon;
                NumberSeats = numberSeats;
                _passengersVagons = new Passenger[NumberSeats];
            }

            public void ShowPassengers()
            {
                for (int i = 0; i < _passengersVagons.Length; i++)
                {
                    if (_passengersVagons[i] == null)
                    {
                        Console.WriteLine("\tСвободное место");
                    }
                    else
                    {
                        Console.WriteLine($"\tПассажир № {i + 1}. Билет № {_passengersVagons[i].TicketNumber}");
                    }
                }
            }

            public void SeatPassenger( Queue<Passenger> _passengers)
            {
                for (int j = 0; j < _passengersVagons.Length; j++)
                {
                    if (_passengers.Count > 0)
                    {
                        _passengersVagons[j] = _passengers.Dequeue();
                    }
                }
            }
        }
    }
}
