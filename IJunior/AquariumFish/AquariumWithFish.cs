using System;
using System.Collections.Generic;

namespace AquariumWithFish
{
    internal class AquariumWithFish
    {
        static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium(10);
            MenuAquarium menuAquarium = new MenuAquarium(aquarium);
            menuAquarium.Run();
        }
    }

    class MenuAquarium
    {
        private const string CommandAddFish = "AddFish";
        private const string CommandRemoveFish = "RemoveFish";
        private const string CommandExit = "Exit";
        private Aquarium _aquarium;
        private bool _isWork = true;

        public MenuAquarium(Aquarium aquarium)
        {
            _aquarium = aquarium;
        }

        private void PrintMenuItems()
        {
            Console.WriteLine($"[{CommandAddFish}]\t- Добавить рыбку в аквариум");
            Console.WriteLine($"[{CommandRemoveFish}]\t- Убрать рыбку из аквариума");
            Console.WriteLine($"[{CommandExit}]\t\t- Выйти из приложения");
        }

        public void Run()
        {
            while (_isWork)
            {
                Console.Clear();

                _aquarium.ShowInfoAllFish();
                PrintMenuItems();
                RunMenuLogic();
                _aquarium.SkipOneMonthFishLife();
            }
        }

        private void RunMenuLogic()
        {
            Console.Write("Выберите пункт меню: ");
            string userInputMeniItem = Console.ReadLine();

            switch (userInputMeniItem)
            {
                case CommandAddFish:
                    AddFish();
                    break;

                case CommandRemoveFish:
                    RemoveFish();
                    break;

                case CommandExit:
                    _isWork = false;
                    break;

                default:
                    Console.WriteLine("Вы выбрали не существующествующий пунк в меню!");
                    Console.ReadKey();
                    break;
            }
        }

        private void AddFish()
        {
            if (_aquarium.MaxCountFish > _aquarium.GetCountFish())
            {
                Console.Clear();
                _aquarium.ShowInfoAllFish();

                Console.Write("Введите имя рыбы: ");
                string userInputName = Console.ReadLine();

                Console.Write("Введите количесвто лет которое может прожить эта рыба: ");
                bool isConverted = int.TryParse(Console.ReadLine(), out int userInputMaxAge);

                if (isConverted)
                {
                    Fish tempFfish = new Fish(userInputName, userInputMaxAge);
                    _aquarium.AddFish(tempFfish);
                }
                else
                {
                    PrintMessageMistake();
                }
            }
            else
            {
                Console.WriteLine("Аквариум заполнен!");
            }

            Console.ReadKey();
        }

        private void RemoveFish()
        {
            if (_aquarium.GetCountFish() > 0)
            {
                Console.Clear();
                _aquarium.ShowInfoAllFish();
                bool isConverted;

                Console.Write("Выберите ID рыбы: ");
                isConverted = int.TryParse(Console.ReadLine(), out int numberFish);

                if (isConverted)
                {
                    if (_aquarium.GetCountFish() >= numberFish)
                    {
                        _aquarium.RemoveFish(numberFish - 1);
                    }
                    else
                    {
                        Console.WriteLine("Вы выбрали не существующую рыбу!");
                    }
                }
                else
                {
                    PrintMessageMistake();
                }
            }
            else
            {
                Console.WriteLine("В аквариуме нет рыб!");
            }

            Console.ReadKey();
        }

        private void PrintMessageMistake()
        {
            Console.WriteLine("Ошибка!");
        }
    }

    class Aquarium
    {
        private List<Fish> _fish = new List<Fish>();

        public int MaxCountFish { get; private set; }

        public Aquarium(int maxCountFish)
        {
            MaxCountFish = maxCountFish;
        }

        public void AddFish(Fish fish)
        {
            _fish.Add(fish);
        }

        public void RemoveFish(int id)
        {
            _fish.Remove(_fish[id]);
        }

        public int GetCountFish()
        {
            return _fish.Count;
        }

        public void ShowInfoAllFish()
        {
            int CursorPisitionLeft = 70;

            Console.SetCursorPosition(CursorPisitionLeft, 0);
            Console.WriteLine($"Рыб в аквариуме {_fish.Count}/{MaxCountFish}");

            for (int i = 0; i < _fish.Count; i++)
            {
                Console.SetCursorPosition(CursorPisitionLeft, i + 1);
                _fish[i].ShowInfo();
            }

            Console.SetCursorPosition(0, 0);
        }

        public void SkipOneMonthFishLife()
        {
            foreach (var fish in _fish)
            {
                fish.MissingMonth();
            }
        }
    }

    class Fish
    {
        private static int _ids;
        private bool _isAlive = true;
        private int _countMonthOneYear = 12;

        public string Name { get; private set; }

        public int Age { get; private set; }

        public int MaxAgeInMonth { get; private set; }

        public int Number { get; private set; }

        public Fish(string name, int maxAge)
        {
            Number = ++_ids;
            _isAlive = true;
            Age = 0;
            Name = name;
            MaxAgeInMonth = maxAge * _countMonthOneYear;
        }

        public void ShowInfo()
        {
            if (_isAlive)
            {
                Console.Write("#{0}\t|Age: {1}/{2}.мес\t| Name: {3} ", Number, Age, MaxAgeInMonth, Name);
            }
            else
            {
                Console.Write("#{0}\t|Рыбка померла, лучше её убрать!|", Number);
            }
        }

        public void MissingMonth()
        {
            Age++;

            if (Age > MaxAgeInMonth)
            {
                _isAlive = false;
            }
        }
    }
}