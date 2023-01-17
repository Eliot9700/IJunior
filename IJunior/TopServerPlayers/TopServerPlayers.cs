using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TopServerPlayers
{
    internal class TopServerPlayers
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
        private List<Player> _players = new List<Player>();

        public Menu()
        {
            CreateRandomPlayers();
        }

        public void Run()
        {
            int topPlayers = 3;
            Console.Clear();
            ShowPlayersByLevel(topPlayers);
            ShowPlayersByPover(topPlayers);
            Console.ReadKey();
        }

        private void ShowPlayersByLevel(int topPlayers)
        {
            var filteredPlayers = _players.OrderByDescending(_patients => _patients.Level).Take(topPlayers).ToList();
            Console.WriteLine($"Топ {topPlayers} игрока по уровню:");
            ShowPlayer(filteredPlayers);
        }

        private void ShowPlayersByPover(int topPlayers)
        {
            var filteredPlayers = _players.OrderByDescending(_patients => _patients.Power).Take(topPlayers).ToList();
            Console.WriteLine($"Топ {topPlayers} игрока по силе:");
            ShowPlayer(filteredPlayers);
        }

        private void ShowPlayer(List<Player> players)
        {
            foreach (var player in players)
            {
                player.ShowInfo();
            }
        }

        private void CreateRandomPlayers()
        {
            int minPlayers = 10;
            int maxPlayers = 20;
            int players = _random.Next(minPlayers, maxPlayers);

            for (int i = 0; i < players; i++)
            {
                _players.Add(new Player("Player" + (i + 1)));
            }
        }
    }

    class Player
    {
        private static Random _random = new Random();

        public string Nickname { get; private set; }

        public int Level { get; private set; }

        public int Power { get; private set; }

        public Player(string nickname)
        {
            Nickname = nickname;
            Level = _random.Next(100);
            Power = _random.Next(100);
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Nickname: {Nickname}\t|  Уровень: {Level}\t|  Сила: {Power}");
        }
    }
}
