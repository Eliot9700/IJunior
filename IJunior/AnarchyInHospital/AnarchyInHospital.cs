using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        private const string SORTBYLASTNAME = "surname";
        private const string SORTBYAGE = "age";
        private const string SHOWBYDISEASE = "disease";
        private const string EXIT = "exit";
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
                Console.WriteLine($"[{SORTBYLASTNAME}]\t- Отсортировать по фамилии");
                Console.WriteLine($"[{SORTBYAGE}]\t\t- Отсортировать всех по возрасту");
                Console.WriteLine($"[{SHOWBYDISEASE}]\t- Показать всех с указанным диагнозом");
                Console.WriteLine($"[{EXIT}]\t\t- Выход");
                Console.Write("Введите команду: ");

                switch (Console.ReadLine())
                {
                    case SORTBYLASTNAME:
                        SortByLastName();
                        break;
                    case SORTBYAGE:
                        SortByAge();
                        break;
                    case SHOWBYDISEASE:
                        ShowByDisease();
                        break;
                    case EXIT:
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

        private void SortByLastName()
        {
            var patients = _hospital.GetAllPatients();
            patients.Sort((x, y) => string.Compare(x.Surname, y.Surname));
            ShowPatients(patients);
        }

        private void SortByAge()
        {
            var patients = _hospital.GetAllPatients();
            patients.Sort(delegate (Patient x, Patient y) { return x.Age.CompareTo(y.Age); });
            ShowPatients(patients);
        }

        private void ShowByDisease()
        {
            var patients = _hospital.GetAllPatients();

            Console.Write("Введите название болезни: ");
            string userInput = Console.ReadLine();
            var filteredPatients = patients.Where(patient => patient.Disease.ToLower() == userInput.ToLower());
            ShowPatients(filteredPatients.ToList());
        }

        private void ShowPatients(List<Patient> patients)
        {
            foreach (var patient in patients)
            {
                patient.ShowInfo();
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

        public void ShowPatientByDisease(string desease)
        {
            foreach (var patient in _patients)
            {
                if (patient.Disease.ToLower() == desease.ToLower())
                {
                    patient.ShowInfo();
                }
            }
        }

        public List<Patient> GetAllPatients()
        {
            return _patients;
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
    }

    class Patient
    {
        private static Random _random = new Random();
        private static string[] _disease = { "Астма", "Диабет", "Аденоиды", "Крапивница" };
        private static string[] _names = { "Андрей", "Максим", "Владислав", "Денис", "Даниил", "Игорь" };
        private static string[] _surnames = { "Иванов", "Смирнов", "Кузнецов", "Попов", "Васильев", "Петров" };
        private static string[] _fatherNames = { "Андреевич", "Петрович", "Сергеевич", "Агапович", "Вонифатович", "Дмитриевич" };

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
            Fathername = _fatherNames[_random.Next(_fatherNames.Length)];
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
