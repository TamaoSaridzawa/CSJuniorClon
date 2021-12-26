using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnarchyHospital
{
    class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            database.Work();
            Console.ReadKey();
        }
    }

    class Patient
    {
        public string Surname { get; private set; }
        public int Age { get; private set; }
        public string Disease { get; private set; }

        public Patient(string surname, int age, string disease)
        {
            Surname = surname;
            Age = age;
            Disease = disease;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Surname}. Возраст - {Age}. Заболевание - {Disease}");
        }
    }

    class Database
    {
        private List<Patient> _patients = new List<Patient>();

        public Database()
        {
            CreatePatients();
        }

        public void Work()
        {
            bool closeProgram = false;

            while (closeProgram == false)
            {
                Console.WriteLine("1)Отсортировать всех больных по фио.\n2)Отсортировать всех больных по возрасту\n3)Вывести больных с определенным заболеванием\n4)Закрыть программу");

                Console.WriteLine("Введите команду : ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        SortAllPatientsSurname();
                        break;
                    case "2":
                        SortAllPatientsAge();
                        break;
                    case "3":
                        ShowPatientsWithCertainDisease();
                        break;
                    case "4":
                        closeProgram = true;
                        break;
                    default:
                        Console.WriteLine("Неправильный ввод команды. Попробуйте еще раз");
                        break;
                }
            }
        }

        private void SortAllPatientsSurname()
        {
            var filterPatients = _patients.OrderBy(patient => patient.Surname).ToList();

            ShowAllPatients(filterPatients);
        }

        private void SortAllPatientsAge()
        {
            var filterPatients = _patients.OrderBy(patient => patient.Age).ToList();

            ShowAllPatients(filterPatients);
        }

        private void ShowPatientsWithCertainDisease()
        {
            Console.WriteLine("Введите название болезни :");

            string userInput = Console.ReadLine();

            var filterPatients = _patients.Where(patient => patient.Disease.ToLower().Contains(userInput.ToLower())).ToList();

            ShowAllPatients(filterPatients);
        }

        private void ShowAllPatients(List<Patient> filterPatients)
        {
            if (filterPatients.Count == 0)
            {
                Console.WriteLine("По вашему запросу совпадений не найдено");
            }
            else
            {
                foreach (var patient in filterPatients)
                {
                    patient.ShowInfo();
                }
            }
        }

        private void CreatePatients()
        {
            _patients.Add(new Patient("Ларин Николай Александрович", 33, "гайморит"));
            _patients.Add(new Patient("Василенко Сергей Анатольевич", 44, "Грыжа"));
            _patients.Add(new Patient("Фейсханов Рамиль Юсулович", 32, "Сердце"));
            _patients.Add(new Patient("Яфаров Андрей Алексеевич", 31, "Грыжа"));
            _patients.Add(new Patient("Лысков Алексей Алексеевич", 28, "Гастрит"));
            _patients.Add(new Patient("Бирюкова Виктория Александровна", 15, "Остеохондроз"));
            _patients.Add(new Patient("Василенко Михаил Сергеевич", 23, "Коронавирус"));
            _patients.Add(new Patient("Весенев Михаил Николаевич", 24, "Сердце"));
            _patients.Add(new Patient("Бирюкова Ольга Анатольевна", 36, "Зрение"));
            _patients.Add(new Patient("Каев Денис Артемович", 33, "Почки"));
        }
    }
}
