using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace Amnesty
{
    internal class Amnesty
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.Run();
        }
    }

    class Menu
    {
        private static Random _random = new Random();
        private List<Offender> _offenders = new List<Offender>();
        private string _exoneratedFelony;

        public Menu()
        {
            _exoneratedFelony = "Антиправительственное";
            CreateRandomListOffenders();
        }

        public void Run()
        {
            Console.WriteLine("Добро пожаловать!");

            Console.WriteLine("Все преступники до амнистии!");
            ShowInfoOnAllOffenders(_offenders);
            Console.ReadKey();
            Console.Clear();

            _offenders = SetCriminalsFree(_exoneratedFelony);
            Console.WriteLine("Все преступники после амнистии!");
            ShowInfoOnAllOffenders(_offenders);

            Console.ReadKey();
        }

        private List<Offender> SetCriminalsFree(string exoneratedFelony)
        {
            var allCriminalsAfterRelease = _offenders.Where(offender => offender.Crime != exoneratedFelony);
            var freedOffenders = _offenders.Where(offender => offender.Crime == exoneratedFelony);

            foreach (var offender in freedOffenders)
            {
                offender.FreeOffenred();
            }

            Console.WriteLine("Освобождённые");
            ShowInfoOnAllOffenders(freedOffenders.ToList());
            Console.ReadKey();
            Console.Clear();

            return allCriminalsAfterRelease.ToList();
        }

        private void ShowInfoOnAllOffenders(List<Offender> offenders)
        {
            string[] allRaces = Offender.GetAllCrimes();

            foreach (var offender in offenders)
            {
                offender.ShowInfo();
            }
        }

        private void CreateRandomListOffenders()
        {
            int minNumberOffenders = 10;
            int maxNumberOffenders = 40;
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
        private static string[] _crimes = { "Антиправительственное", "Увийство\t\t", "Кража\t\t", "Вандализ\t\t" };
        private static string[] _names = { "Андрей", "Максим", "Алистер", "Дейв", "Кэнди", "Манул" };
        private static string[] _surnames = { "Мартин", "Гейст", "Ивонов", "Земалун", "Джобс", "Займан" };
        private static string[] _fatherNames = { "Андреевич", "Эндрю", "Томас", "Алимов", "Грейман", "Файман" };

        public string FullName { get; private set; }

        public bool IsConclusion { get; private set; }

        public string Crime { get; private set; }

        public Offender()
        {
            FullName = SetRandomFullName();
            Crime = SetRandomCrime();
            IsConclusion = true;
        }

        public static string[] GetAllCrimes()
        {
            return Offender._crimes;
        }

        public void ShowInfo()
        {
            string conslution = "No";

            if (IsConclusion == true)
            {
                conslution = "Yes";
            }

            Console.Write($"Имя: {FullName}  \t|\t Преступление: {Crime}\t|\tЗаключён под стражу: {conslution}\n");
        }

        public void FreeOffenred()
        {
            IsConclusion = false;
        }

        private string SetRandomFullName()
        {
            string name = _names[_random.Next(_names.Length)];
            string surName = _surnames[_random.Next(_surnames.Length)];
            string fatherName = _fatherNames[_random.Next(_fatherNames.Length)];

            return $"{surName} {name} {fatherName}";
        }

        private string SetRandomCrime()
        {
            return _crimes[_random.Next(_crimes.Length)];
        }
    }
}
