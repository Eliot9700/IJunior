using System;
using System.Collections.Generic;
using System.Linq;

namespace AnarchyInHospital
{
    internal class AnarchyInHospital
    {
        static void Main(string[] args)
        {
            Hospital hospital = new Hospital();
            Menu menu = new Menu(hospital);
            menu.Run();
        }
    }

    class Menu
    {
        private const string SortLastname = "surname";
        private const string SortAge = "age";
        private const string ShowDisease = "disease";
        private const string Exit = "exit";
        private Hospital _hospital;

        public Menu(Hospital hospital)
        {
            _hospital = hospital;
        }

        public void Run()
        {
            bool isWork = true;

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine("Добро пожаловать!");
                Console.WriteLine($"[{SortLastname}]\t- Показать отсортированных пациентов по фамилии");
                Console.WriteLine($"[{SortAge}]\t\t- Показать отсортированных пациентов по возрасту");
                Console.WriteLine($"[{ShowDisease}]\t- Показать всех пациентов с указанным диагнозом");
                Console.WriteLine($"[{Exit}]\t\t- Выход");
                Console.Write("Введите команду: ");

                switch (Console.ReadLine())
                {
                    case SortLastname:
                        _hospital.SortByLastName();
                        break;
                    case SortAge:
                        _hospital.SortByAge();
                        break;
                    case ShowDisease:
                        _hospital.ShowByDisease();
                        break;
                    case Exit:
                        isWork = false;
                        Console.Clear();
                        Console.WriteLine("Досвидания!");
                        break;
                    default:
                        Console.WriteLine("Вы ввели несуществующую команду!");
                        break;
                }

                Console.ReadKey();
            }
        }
    }

    class Hospital
    {
        private static Random _random = new Random();
        private List<Patient> _patients = new List<Patient>();

        public Hospital()
        {
            CreateRandomPatients();
        }
        public void SortByLastName()
        {
            _patients.Sort((x, y) => string.Compare(x.Surname, y.Surname));
            ShowPatients(_patients);
        }

        public void SortByAge()
        {
            _patients.Sort(delegate (Patient x, Patient y) { return x.Age.CompareTo(y.Age); });
            ShowPatients(_patients);
        }

        public void ShowByDisease()
        {
            Console.Write("Введите название болезни: ");
            string userInput = Console.ReadLine();
            var filteredPatients = _patients.Where(patient => patient.Disease.ToLower() == userInput.ToLower());
            ShowPatients(filteredPatients.ToList());
        }

        private void CreateRandomPatients()
        {
            int minPatients = 10;
            int maxPatients = 20;
            int countPatients = _random.Next(minPatients, maxPatients);

            for (int i = 0; i < countPatients; i++)
            {
                _patients.Add(new Patient());
            }
        }

        private void ShowPatients(List<Patient> patients)
        {
            foreach (var patient in patients)
            {
                patient.ShowInfo();
            }
        }
    }

    class Patient
    {
        private static Random _random = new Random();
        private static string[] _disease = { "Астма", "Диабет", "Аденоиды", "Крапивница" };
        private static string[] _names = { "Андрей", "Максим", "Владислав", "Денис", "Даниил", "Игорь" };
        private static string[] _surnames = { "Иванов", "Смирнов", "Кузнецов", "Попов", "Васильев", "Петров" };
        private static string[] _fathernames = { "Андреевич", "Петрович", "Сергеевич", "Агапович", "Вонифатович", "Дмитриевич" };

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string Fathername { get;private set; }

        public int Age { get; private set; }

        public string Disease { get; private set; }

        public Patient()
        {
            Name = _names[_random.Next(_names.Length)];
            Age = _random.Next(10, 70);
            Surname = _surnames[_random.Next(_surnames.Length)];
            Fathername = _fathernames[_random.Next(_fathernames.Length)];
            Disease = _disease[_random.Next(_disease.Length)];
        }

        public void ShowInfo()
        {
            string fullName = $"{Surname} {Name} {Fathername}";
            int stringLength = 30;

            for (int i = fullName.Length; i < stringLength; i++)
            {
                fullName += " ";
            }

            Console.WriteLine($"ФИО: {fullName}\t|  Восраст: {Age}  |  Диагноз: {Disease}");
        }
    }
}
