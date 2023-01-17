using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefinitionOfDelinquency
{
    internal class DefinitionOfDelinquency
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.Run();
        }
    }

    class Menu
    {
        private const int YearNow = 2022;
        private static Random _random = new Random();
        private List<CannedStew> _cansOfStew = new List<CannedStew>();

        public Menu()
        {
            CreateRandomCansOfStew();
        }

        public void Run()
        {
            ShowAllExpiredStews(YearNow);
            ShowAllFreshStew(YearNow);
            Console.ReadKey();
        }

        private void ShowAllExpiredStews(int yearNow)
        {
            var allSpoiledStew = _cansOfStew.Where(cannedStew => cannedStew.ProductionYear + cannedStew.ShelfLife < yearNow).ToList();

            if (allSpoiledStew.Count() == 0)
            {
                Console.WriteLine("Нет испорченной тушёнки!");
            }
            else
            {
                Console.WriteLine("Испорченная тушёнка:");
                ShowStew(allSpoiledStew);
            }
        }

        private void ShowAllFreshStew(int yearNow)
        {
            var allFreshStew = _cansOfStew.Where(cannedStew => cannedStew.ProductionYear + cannedStew.ShelfLife >= yearNow).ToList();

            if (allFreshStew.Count() == 0)
            {
                Console.WriteLine("Нет свежей тушёнки!");
            }
            else
            {
                Console.WriteLine("Свежая тушёнка: ");
                ShowStew(allFreshStew);
            }
        }

        private void ShowStew(List<CannedStew> cannedStews)
        {
            foreach (var cannedStew in cannedStews)
            {
                cannedStew.ShowInfo();
            }
        }

        private void CreateRandomCansOfStew()
        {
            int minCansOfStew = 10;
            int maxCansOfStew = 20;
            int cansOfStew = _random.Next(minCansOfStew,maxCansOfStew);

            for (int i = 0; i < cansOfStew; i++)
            {
                _cansOfStew.Add(new CannedStew());
            }
        }
    }

    class CannedStew
    {
        private static Random _random = new Random();

        public string Title { get; private set; }

        public int ProductionYear { get; private set;}

        public int ShelfLife { get; private set;}

        public CannedStew()
        {
            Title = SetRandomTitle();
            ProductionYear = SetRandomProductionYear();
            ShelfLife = SetRamdonShelfLife();
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Название: {Title}\t|\tСрок годности: {ShelfLife} лет\t|\tДата изготовления: {ProductionYear} год");
        }

        private string SetRandomTitle()
        {
            string[] titles = { "Говядина", "Курица", "Индюшка", "Свинина", "Телятина" };

            return titles[_random.Next(titles.Length)];
        }

        private int SetRandomProductionYear()
        {
            int minYear = 1980;
            int maxYear = 2022;

            return _random.Next(minYear,maxYear);
        }

        private int SetRamdonShelfLife()
        {
            int minShelfLife = 2;
            int maxShelfLife = 10;
            return _random.Next(minShelfLife, maxShelfLife);
        }
    }
}
