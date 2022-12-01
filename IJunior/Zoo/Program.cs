using System;
using System.Collections.Generic;

namespace ZOO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isWork = true;

            List<Aviary> aviaries = new List<Aviary>();
            aviaries.Add(new Aviary(new Animal("Лев", "Рычит")));
            aviaries.Add(new Aviary(new Animal("Слон", "Трубит")));
            aviaries.Add(new Aviary(new Animal("Тигр", "Рычит")));
            aviaries.Add(new Aviary(new Animal("Жираф", "Хмыкает")));
            aviaries.Add(new Aviary(new Animal("Обезьяна", "Хрюкает, Пыхтит, Лает, Хныкает")));

            while (isWork)
            {
                Console.Clear();

                ShowListAviaries(aviaries);
                EnterAviary(aviaries);

                Console.ReadKey();
            }
        }

        static void ShowListAviaries(List<Aviary> aviaries)
        {
            Console.WriteLine("Вы пришли в зоопарк и видите {0} вальеров", aviaries.Count);

            foreach (var aviarie in aviaries)
            {
                Console.WriteLine("№" + aviarie.ID);
            }
        }

        static void EnterAviary(List<Aviary> aviaries)
        {
            Console.Write("Выберите номер вальера чтоб подойти к нему: ");
            bool isConverted = int.TryParse(Console.ReadLine(), out int numberAviary);

            if (isConverted && numberAviary <= aviaries.Count)
            {
                foreach (var aviarie in aviaries)
                {
                    if (aviarie.ID == numberAviary)
                    {
                        Console.Clear();
                        aviarie.ShowAllInfo();
                    }
                }
            }
            else
            {
                Console.WriteLine("Введено неверное значение!");
            }
        }
    }

    class Aviary
    {
        private static Random _random = new Random();
        private static int _ids;
        private List<Animal> _animals = new List<Animal>();
        private Animal _animal;
        private int _minCountAnimal = 5;
        private int _maxCountAnimal = 20;

        public int ID { get; set; }

        public Aviary(Animal animal)
        {
            ID = ++_ids;
            _animal = animal;
            CreateAviaty();
        }

        public void CreateAviaty()
        {
            int randomCountAnimal = _random.Next(_minCountAnimal, _maxCountAnimal);

            for (int i = 0; i < randomCountAnimal; i++)
            {
                Animal tempAnimal = new Animal(_animal.Name, _animal.Sound);
                _animals.Add(tempAnimal);
            }
        }

        public void ShowAllInfo()
        {
            Console.WriteLine("Вы подошли к вольеру!");
            Console.WriteLine($"В вольере: {_animal.Name} в количестве {_animals.Count} штук!");
            Console.WriteLine($"Он {_animal.Sound}");

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
            Gender = EstablishGender();
            Sound = sound;
        }

        public void ShowInfo()
        {
            Console.WriteLine("Gender: {0}", Gender);
        }

        private string EstablishGender()
        {
            int gender = random.Next(0, 2);

            if (gender == 1)
            {
                return "Male";
            }
            else
            {
                return "Famale";
            }
        }
    }
}
