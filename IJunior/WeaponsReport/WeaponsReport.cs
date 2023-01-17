using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponsReport
{
    internal class WeaponsReport
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
        private List<Soldier> _soliders = new List<Soldier>();

        public Menu()
        {
            CreateRandomSoliders();
        }

        public void Run()
        {
            ShowNamesAndRanksAllSoliders();
        }

        private void ShowNamesAndRanksAllSoliders()
        {
            var newSoliders = _soliders.Select(solired => new
            {
                Name = solired.Name,
                Rank = solired.Rank,
            });

            foreach (var solider in newSoliders)
            {
                Console.WriteLine($"Name: {solider.Name}\t|\tRank: {solider.Rank}");
            }
        }

        private void CreateRandomSoliders()
        {
            int minSoliders = 10;
            int maxSoliders = 20;
            int soliders = _random.Next(minSoliders, maxSoliders);
            _soliders.Clear();

            for (int i = 0; i < soliders; i++)
            {
                _soliders.Add(new Soldier());
            }
        }
    }

    class Soldier
    {
        private static Random _random = new Random();

        public string Name { get; private set; }

        public string Armament { get; private set; }

        public string Rank { get; private set; }

        public int LifetimeInMonths { get; private set; }

        public Soldier()
        {
            Name = SetRandomName();
            Armament = SetRandomArmament();
            Rank = SetRandomRank();
            LifetimeInMonths = SetRandomServiceLifeInMonths();
        }

        private string SetRandomName()
        {
            string[] names = { "Марк", "Алек", "Том", "Джеймс", "Джон", "Дин", "Рик" };
            return names[_random.Next(names.Length)];
        }

        private string SetRandomArmament()
        {
            string[] armaments = { "Лёгкое", "Среднее", "Тяжёлое" };
            return armaments[_random.Next(armaments.Length)];
        }

        private string SetRandomRank()
        {
            string[] ranks = {"Рядовой", "Ефрейтор", "Младший сержант", "Сержант", "Старший сержан",
                              "Старшина", "Прапорщик", "Старший прапорщик", "Младший лейтенант",
                              "Лейтенант", "Старший лейтенант", "Капитан", "Майор", "Подполковник",
                              "Полковник", "Генерал-майор", "Генерал-лейтенант" };

            return ranks[_random.Next(ranks.Length)];
        }

        private int SetRandomServiceLifeInMonths()
        {
            return _random.Next(24);
        }
    }
}
