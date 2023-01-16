using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOffenders
{
    internal class ListOffenders
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.Run();
        }
    }

    class Menu
    {
        private Random _random = new Random();
        private List<Offender> _offenders = new List<Offender>();

        public Menu()
        {
            CreateRandomListOffenders();
        }

        public void Run()
        {
            bool isWork = true;
            Console.WriteLine("Добро пожаловать, для поиска преступника нужно ввести его данные!");
            Console.ReadKey();

            while (isWork)
            {
                Console.Clear();

                Console.Write("Введите приблезительный Рост: ");
                bool isParsedHeight = int.TryParse(Console.ReadLine(), out int growth);

                Console.Write("Введите приблезительный Вес: ");
                bool isParsedWidth = int.TryParse(Console.ReadLine(), out int width);

                ShowAllNationality();
                Console.Write("Выберите Рассу: ");
                bool isParsedRace = int.TryParse(Console.ReadLine(), out int numberRace);

                if (isParsedHeight && isParsedWidth && isParsedRace)
                {
                    var filteredOffenders = FindOffenderOnList(growth, width, numberRace - 1, _offenders);

                    foreach (var offender in filteredOffenders)
                    {
                        offender.ShowInfo();
                    }
                }

                Console.ReadKey();
            }
        }

        private List<Offender> FindOffenderOnList(int growth, int width, int numberRace, List<Offender> offenders)
        {
            int permissibleErrorOfHeight = 3;
            int minHeight = growth - permissibleErrorOfHeight;
            int maxHeight = growth + permissibleErrorOfHeight;
            var filteredByHeight = _offenders.Where(offender => offender.Height >= minHeight && offender.Height <= maxHeight);

            int permissibleErrorOfWidth = 3;
            int minWidth = width - permissibleErrorOfWidth;
            int maxWidth = width + permissibleErrorOfWidth;
            var filteredByWidth = filteredByHeight.Where(offender => offender.Width >= minWidth && offender.Width <= maxWidth);

            string[] allRaces = Offender.GetAllRaces();
            string race = allRaces[numberRace];
            var filteredOffenders = filteredByWidth.Where(offender => offender.Race == race && offender.IsConclusion == false);

            return filteredOffenders.ToList();
        }

        private void ShowAllNationality()
        {
            string[] allRaces = Offender.GetAllRaces();

            for (int i = 0; i < allRaces.Length; i++)
            {
                Console.WriteLine($"ID: {i + 1} - [{allRaces[i]}]");
            }
        }

        private void CreateRandomListOffenders()
        {
            int minNumberOffenders = 500;
            int maxNumberOffenders = 1000;
            int numberOffenders = _random.Next(minNumberOffenders, maxNumberOffenders);

            for (int i = 0; i < numberOffenders; i++)
            {
                _offenders.Add(new Offender());
            }
        }
    }

    class Offender
    {
        private static Random _random = new Random();
        private static string[] _races = { "Европиоид", "Монголоид", "Негроид", "Австралоид" };
        private static string[] _names = { "Андрей", "Максим", "Алистер", "Дейв", "Кэнди", "Манул" };
        private static string[] _surnames = { "Мартин", "Гейст", "Ивонов", "Земалун", "Джобс", "Займан" };
        private static string[] _fatherNames = { "Андреевич", "Эндрю", "Томас", "Алимов", "Грейман", "Файман" };

        public string FullName { get; private set; }

        public bool IsConclusion { get; private set; }

        public int Height { get; private set; }

        public int Width { get; private set; }

        public string Race { get; private set; }

        public Offender()
        {
            FullName = SetRandomFullName();
            Height = SetRandomHeight();
            Width = SetRandomWidth();
            Race = SetRandomRace();
            IsConclusion = Convert.ToBoolean(_random.Next(0, 2));
        }

        public static string[] GetAllRaces()
        {
            return Offender._races;
        }

        public void ShowInfo()
        {
            string conslution = "No";

            if (IsConclusion == true)
            {
                conslution = "Yes";
            }

            Console.Write($"Имя: {FullName} \t| Рост: {Height} |\tВес: {Width} \t|\tРасса: {Race} \t|\tЗаключён под стражу: {conslution}\n");
        }

        private string SetRandomFullName()
        {
            string fullName;

            string name = _names[_random.Next(_names.Length)];
            string surName = _surnames[_random.Next(_surnames.Length)];
            string fatherName = _fatherNames[_random.Next(_fatherNames.Length)];

            return fullName = $"{surName} {name} {fatherName}";
        }

        private string SetRandomRace()
        {
            int numberRace = _random.Next(_races.Length);
            return _races[numberRace];
        }

        private int SetRandomHeight()
        {
            int minHeight = 140;
            int maxHeight = 220;
            return _random.Next(minHeight, maxHeight);

        }

        private int SetRandomWidth()
        {
            int minWidth = 60;
            int maxWidth = 120;
            return _random.Next(minWidth, maxWidth);
        }
    }
}
