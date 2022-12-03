using System;
using System.Collections.Generic;

namespace Zoo
{
    internal class ZooProgram
    {
        /*
         * + 1. public void GetStarted() -метод ничего не возвращает. Он просто работает. 
         * + 2. private void SelectAviary() - у вас коллекция клеток. Выбирайте клетку по индексу в коллекции. 
         *      Id клетки может отличаться от индекса в коллекции и в этом задании не нужно. 
         *  3. Переменные именуются с маленькой буквы, 
         *      только приватные поля с символа _ и маленькой буквы (исключение - константы), 
         *      а всё остальное с большой буквы. 
         * + 4. return gender[random.Next(0, 2)]; } - не 2, а длина массива.
         * 
         */
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();
            zoo.Run();
        }
    }

    class Zoo
    {
        private List<Aviary> _aviaries;

        public void Run()
        {
            bool isWork = true;

            CreateAviaries();            

            while (isWork)
            {
                Console.Clear();

                ShowAllAviaries();
                SelectAviary();

                Console.ReadKey();
            }
        }

        private void ShowAllAviaries()
        {
            Console.WriteLine("Вы пришли в зоопарк и видите {0} вольеров", _aviaries.Count);

            for (int i = 0; i < _aviaries.Count; i++)
            {
                Console.WriteLine($"Вольер #: {i + 1}");
            }
        }

        private void SelectAviary()
        {
            Console.Write("Выберите номер вольера чтоб подойти к нему: ");
            bool isConverted = int.TryParse(Console.ReadLine(), out int numberAviary);

            if (isConverted && numberAviary <= _aviaries.Count)
            {
                Console.Clear();
                _aviaries[numberAviary - 1].ShowAllInfo();
            }
            else
            {
                Console.WriteLine("Введено неверное значение!");
            }
        }

        private void CreateAviaries()
        {
            _aviaries = new List<Aviary>();

            _aviaries.Add(new Aviary(new Animal("Лев", "Рычит")));
            _aviaries.Add(new Aviary(new Animal("Слон", "Трубит")));
            _aviaries.Add(new Aviary(new Animal("Тигр", "Рычит")));
            _aviaries.Add(new Aviary(new Animal("Жираф", "Хмыкает")));
            _aviaries.Add(new Aviary(new Animal("Обезьяна", "Хрюкает, Пыхтит, Лает, Хныкает")));
        }
    }

    class Aviary
    {
        private static Random _random = new Random();
        private List<Animal> _animals = new List<Animal>();
        private string _aviaryName;

        public Aviary(Animal animal)
        {
            _aviaryName = animal.Name;
            FillOut(animal);
        }

        private void FillOut(Animal animal)
        {
            int minCountAnimal = 5;
            int maxCountAnimal = 20;
            int randomCountAnimal = _random.Next(minCountAnimal, maxCountAnimal);

            for (int i = 0; i < randomCountAnimal; i++)
            {
                Animal tempAnimal = new Animal(animal.Name, animal.Sound);
                _animals.Add(tempAnimal);
            }
        }

        public void ShowAllInfo()
        {
            Console.WriteLine($"Вы подошли к вольеру\nВ нём находится {_aviaryName} в количестве {_animals.Count} особей!");
            Console.WriteLine($"Он {_animals[0].Sound}");

            foreach (var animal in _animals)
            {
                animal.ShowInfo();
            }
        }
    }

    class Animal
    {
        private static Random random = new Random();

        public string Name { get; private set; }

        public string Gender { get; private set; }

        public string Sound { get; private set; }

        public Animal(string name, string sound)
        {
            Name = name;
            Gender = SpecifyGender();
            Sound = sound;
        }

        public void ShowInfo()
        {
            Console.WriteLine("Gender: {0}", Gender);
        }

        private string SpecifyGender()
        {
            string[] gender = { "Male", "Famale" };
            return gender[random.Next(gender.Length)];
        }
    }
}
