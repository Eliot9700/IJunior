using System;
using System.Collections.Generic;

namespace War
{
    internal class War
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(120, 45);
            Console.SetBufferSize(120, 45);
            Console.Title = "War, war never changes!";

            Battlefield battlefield = new Battlefield();
            battlefield.StartWar();
            battlefield.ShowResultWar();
        }
    }

    class Battlefield
    {
        private Platoon _firstPlatoon;
        private Platoon _secondPlatoon;

        public Battlefield()
        {
            int _leftIndentFirstCountry = 0;
            int _leftIndentSecondCountry = 55;
            _firstPlatoon = new Platoon(_leftIndentFirstCountry);
            _secondPlatoon = new Platoon(_leftIndentSecondCountry);
        }

        public void StartWar()
        {
            _firstPlatoon.ShowInfoForAllWarriors();
            _secondPlatoon.ShowInfoForAllWarriors();

            while (_firstPlatoon.GetCount() > 0 && _secondPlatoon.GetCount() > 0)
            {
                Console.ReadKey();
                Console.Clear();

                Attack(_firstPlatoon, _secondPlatoon);
                Attack(_secondPlatoon, _firstPlatoon);

                _firstPlatoon.ShowInfoForAllWarriors();
                _secondPlatoon.ShowInfoForAllWarriors();

                RemoveAllDeadWarriors(_firstPlatoon);
                RemoveAllDeadWarriors(_secondPlatoon);
            }

            Console.Clear();
        }

        public void Attack(Platoon attackingPlatoon, Platoon attackedPlatoon)
        {
            for (int i = 0; i < attackingPlatoon.GetCount(); i++)
            {
                Warrior attackingWarrior = attackingPlatoon.GetWarrior(i);

                Warrior attackedWarrior = attackedPlatoon.GetWarrior(i);

                if (attackedWarrior != null)
                {
                    if (attackedWarrior.IsAlive)
                    {
                        attackedWarrior.TakeDamage(attackingWarrior.Damage);
                    }
                }
            }
        }

        public void RemoveAllDeadWarriors(Platoon platoon)
        {
            for (int i = platoon.GetCount() - 1; i >= 0; i--)
            {
                Warrior tempWarrior = platoon.GetWarrior(i);

                if (tempWarrior.IsAlive == false)
                {
                    platoon.RemoveWarrior(tempWarrior);
                }
            }
        }

        public void ShowResultWar()
        {
            if (_firstPlatoon.GetCount() == 0 && _secondPlatoon.GetCount() == 0)
            {
                Console.WriteLine("Ничья!");
            }
            else if (_firstPlatoon.GetCount() > 0)
            {
                Console.WriteLine("Победила первая страна!");
            }
            else
            {
                Console.WriteLine("Победила вторая страна!");
            }
        }
    }

    class Platoon
    {
        private static Random _random = new Random();
        private int _idWarrior;
        private int _leftIndent;
        private List<Warrior> _warriors = new List<Warrior>();

        public Platoon(int leftIndent)
        {
            int _minCountWarriors = 25;
            int _maxCountWarriors = 40;
            int _countWarriors;
            _idWarrior = 0;
            _leftIndent = leftIndent;
            _countWarriors = _random.Next(_minCountWarriors, _maxCountWarriors);

            for (int i = 0; i < _countWarriors; i++)
            {
                _warriors.Add(new Warrior(++_idWarrior));
            }
        }

        public void ShowInfoForAllWarriors()
        {
            int positionX = Console.CursorTop;

            foreach (var warrior in _warriors)
            {
                warrior.ShowAllInfo(_leftIndent, positionX++);
            }
        }

        public int GetCount()
        {
            return _warriors.Count;
        }

        public Warrior GetWarrior(int positionInList)
        {
            if (positionInList >= _warriors.Count)
            {
                return null;
            }
            else
            {
                return _warriors[positionInList];
            }
        }

        public void RemoveWarrior(Warrior warrior)
        {
            _warriors.Remove(warrior);
        }
    }

    class Warrior
    {
        private static Random _random = new Random();

        public int ID { get; protected set; }

        public int Health { get; protected set; }

        public int Armor { get; protected set; }

        public int Damage { get; protected set; }

        public bool IsAlive { get; protected set; }

        public Warrior(int id)
        {
            int minHealth = 70;
            int maxHealth = 120;
            int minDamage = 15;
            int maxDamage = 25;
            int minArmor = 5;
            int maxArmor = 15;

            ID = id;
            IsAlive = true;
            Health = _random.Next(minHealth, maxHealth);
            Damage = _random.Next(minDamage, maxDamage);
            Armor = _random.Next(minArmor, maxArmor);
        }

        public void TakeDamage(int damage)
        {
            damage -= Armor;

            if (damage > 0)
            {
                Health -= damage;

                if (Health <= 0)
                {
                    Health = 0;
                    IsAlive = false;
                }
            }
        }

        public void ShowAllInfo(int positionLeft, int positionTop)
        {
            Console.SetCursorPosition(positionLeft, positionTop);

            if (IsAlive)
            {
                Console.Write("|ID:{0}\tHealth: {1}\t Armor: {2}\tDamage: {3}|", ID, Health, Armor, Damage);
            }
            else
            {
                Console.Write("|ID:{0}\t|-------------------DEAD------------------|", ID);
            }

            Console.SetCursorPosition(0, 0);
        }
    }
}