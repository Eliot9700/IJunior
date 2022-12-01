using System;
using System.Collections.Generic;

namespace Zoo
{
    internal class Program
    {
        /*  Доработать. 
         * + 1. В class Program оставляйте одну функцию Main (можно общую функцию по типу ReadInt()), 
         *      а для всего остального функционала выделяйте дополнительный класс. Вам нужен зоопарк. 
         * + 2. EnterAviary(aviaries); - метод не будет подходить, так как будет зоопарк, а методы указывают что делает класс. 
         * + 3. CreateAviaty() - Методы, которые должны вызывать только внутри класса, должны быть приватными (ведь не планируется, 
         *      что кто то извне сможет их вызвать, а значит и изменить класс через них). 
         * + 4. CreateAviaty() - не повторяйте имя класса внутри имени его методов/полей. 
         * + 5. private Animal _animal; 
         *      private int _minCountAnimal = 5; 
         *      private int _maxCountAnimal = 20; - не нужны поля, количество просто переменные в нужном методе, 
         *      а животное, передается в нужный метод. 
         *      У вас список отвечает за всех животных. 
         *      Может быть имя вольера. 
         *  6. if (gender == 1) { return "Male"; } else { return "Famale"; - сделайте проще. 
         *      Массив строк и из массива выбирать значение.
         */
        static void Main(string[] args)
        {
            bool isWork = true;

            Zoo zoo = new Zoo();

            while (isWork)
            {
                Console.Clear();

                zoo.ShowAllAviaries();
                zoo.SelectAviary();

                Console.ReadKey();
            }
        }
    }

    class Zoo
    {
        private List<Aviary> _aviaries = new List<Aviary>();

        public Zoo()
        {
            CreateAviaries();
        }

        private void CreateAviaries()
        {
            _aviaries.Add(new Aviary(new Animal("Лев", "Рычит")));
            _aviaries.Add(new Aviary(new Animal("Слон", "Трубит")));
            _aviaries.Add(new Aviary(new Animal("Тигр", "Рычит")));
            _aviaries.Add(new Aviary(new Animal("Жираф", "Хмыкает")));
            _aviaries.Add(new Aviary(new Animal("Обезьяна", "Хрюкает, Пыхтит, Лает, Хныкает")));
        }

        public void ShowAllAviaries()
        {
            Console.WriteLine("Вы пришли в зоопарк и видите {0} вольеров", _aviaries.Count);

            foreach (var aviarie in _aviaries)
            {
                Console.WriteLine("Вольер #: " + aviarie.ID);
            }
        }

        public void SelectAviary()
        {
            Console.Write("Выберите номер вальера чтоб подойти к нему: ");
            bool isConverted = int.TryParse(Console.ReadLine(), out int numberAviary);

            if (isConverted && numberAviary <= _aviaries.Count)
            {
                foreach (var aviarie in _aviaries)
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
        private string _aviaryName;

        public int ID { get; set; }

        public Aviary(Animal animal)
        {
            _aviaryName = animal.Name;
            ID = ++_ids;
            Create(animal);
        }

        private void Create(Animal animal)
        {
            int _minCountAnimal = 5;
            int _maxCountAnimal = 20;
            int randomCountAnimal = _random.Next(_minCountAnimal, _maxCountAnimal);

            for (int i = 0; i < randomCountAnimal; i++)
            {
                Animal tempAnimal = new Animal(animal.Name, animal.Sound);
                _animals.Add(tempAnimal);
            }
        }

        public void ShowAllInfo()
        {
            Console.WriteLine($"Вы подошли к вольеру\nВ нём находится {_aviaryName} в количестве {_animals.Count} штук!");
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
            Gender = EstablishGender();
            Sound = sound;
        }

        public void ShowInfo()
        {
            Console.WriteLine("Gender: {0}", Gender);
        }

        private string EstablishGender()
        {
            string[] gender = { "Male", "Famale" };

            return gender[random.Next(0, 2)];
        }
    }
}
