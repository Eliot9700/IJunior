using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice2
{
    internal class Autoservice2
    {
        static void Main(string[] args)
        {
            Autoservice autoservice = new Autoservice();
            MenuAutoservice menu = new MenuAutoservice(autoservice);
            menu.Run();
        }        
    }

    class MenuAutoservice
    {
        private const string StartWork = "start";
        private const string EndWork = "end";
        private Autoservice _autoservice;

        public MenuAutoservice(Autoservice autoservice)
        {
            _autoservice = autoservice;
        }

        public void Run()
        {
            bool isWork = true;

            while (isWork)
            {
                if (_autoservice.GetCountClients() > 0)
                {
                    Console.Clear();
                    ShowInitialInfo();
                    Console.Write("Введите команду: ");
                    string userInput = Console.ReadLine();

                    switch (userInput)
                    {
                        case StartWork:
                            _autoservice.StartRepairs();
                            break;
                        case EndWork:
                            isWork = false;
                            break;
                        default:
                            Console.WriteLine("Вы выбрали несуществующую команду, повторите выбор!");
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Вы обслужили всех клиентов!");
                    isWork = false;
                }

                Console.ReadKey();
            }
        }

        private void ShowInitialInfo()
        {
            _autoservice.ShowCashInfo();
            Console.WriteLine("Клиентов в очереди: {0}\n", _autoservice.GetCountClients());
            Console.WriteLine($"[{StartWork}] - Начать ремонт");
            Console.WriteLine($"[{EndWork}]\t- Завершить работу автосервиса");
        }
    }

    class Autoservice
    {       
        private static Random _random = new Random(); 
        private Queue<Client> _clients = new Queue<Client>();
        private Storage _storage;
        private int _costWorkInPercent = 20;

        public int Money { get; private set; }

        public Autoservice()
        {
            Money = 10000;
            CreateRandomStorage();
            CreateRandomQueueClients();
        }

        public void ShowCashInfo()
        {
            Console.SetCursorPosition(Console.BufferWidth - 25, 0);
            Console.Write($"Денег в кассе: {Money}");
            Console.SetCursorPosition(0, 0);
        }

        public void StartRepairs()
        {
            Console.Clear();

            Client client = _clients.Dequeue();
            Car clientCar = client.GetCar();
            Part ruinedPart = clientCar.GetDestroyedPart();
            int repairCosts = GetRepairCosts(ruinedPart.Price); 

            clientCar.ShowInfoBrokenPart();
            Console.WriteLine($"Стоимость ремонта - [{repairCosts}]\n");

            if (client.Money >= repairCosts)
            {
                ShowStorageInfo();

                Console.Write("Выберите какую деталь вы хотите поменять: ");

                if (int.TryParse(Console.ReadLine(), out int numberBox))
                {
                    bool isPartFound = _storage.TrytGetPart(out Part functionalPart, numberBox - 1);
                     
                    if (isPartFound)
                    {
                        bool isRepaired = TryRepairPart(functionalPart, ruinedPart);

                        if (isRepaired)
                        {
                            CalculateСlient(repairCosts, client);
                        }
                        else
                        {
                            Console.WriteLine("Вы поменяли не ту запчасть, и заплатили клиенту за ущерб");
                            PayFine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Вы выбрали не ту деталь и клиент уехал!");
                        PayFine();
                    }
                }
                else
                {
                    Console.WriteLine("Вы допустили ошибку во время выбора запчасти и потеряли клиента!");
                    PayFine();
                }
            }
            else
            {
                Console.WriteLine("У клиента не хватило денег на ремонт и он уехал!");
            }
        }

        public int GetCountClients()
        {
            return _clients.Count();
        }

        private void ShowStorageInfo()
        {
            _storage.ShowInfo();
        }

        private void PayFine()
        {
            int fineAmount = 1000;
            Money -= fineAmount;
            Console.WriteLine($"Выплата штрафа составила {fineAmount}");
        }

        private bool TryRepairPart(Part functionalPart, Part ruinedPart)
        {
            bool isRepairedCorrectly = false;

            if (functionalPart.Name == ruinedPart.Name)
            {
                _storage.RemovePart(functionalPart);
                ruinedPart.Repair();
                Console.WriteLine("Ремонт успешно завершён!");
                isRepairedCorrectly = true;
            }

            return isRepairedCorrectly;
        }

        private int GetRepairCosts(int partPrice)
        {
            return ((partPrice * _costWorkInPercent) / 100) + partPrice;
        }

        private void CalculateСlient(int sum, Client client)
        {
            client.Pay(sum);
            Money += sum;
            Console.WriteLine($"Вы заработали {sum}");
        }

        private void CreateRandomQueueClients()
        {
            int minCoumtClients = 1;
            int maxCountClients = 5;
            int countClients = _random.Next(minCoumtClients, maxCountClients);

            for (int i = 0; i < countClients; i++)
            {
                _clients.Enqueue(new Client());
            }
        }

        private void CreateRandomStorage()
        {
            List<Box> boxes = new List<Box>();
            int minParts = 10;
            int maxParts = 30;

            int headlights = _random.Next(minParts, maxParts);
            int engines = _random.Next(minParts, maxParts);
            int locks = _random.Next(minParts, maxParts);
            int radiators = _random.Next(minParts, maxParts);
            int batteries = _random.Next(minParts, maxParts);

            boxes.Add(new Box(headlights, new Headlight()));
            boxes.Add(new Box(engines, new Engine()));
            boxes.Add(new Box(locks, new Lock()));
            boxes.Add(new Box(radiators, new Radiator()));
            boxes.Add(new Box(batteries, new Battary()));

            _storage = new Storage(boxes);
        }
    }

    class Box
    {
        private List<Part> _parts = new List<Part>();
        
        public string Type { get; private set; }

        public Box(int counntParts, Part part)
        {
            Type = part.Name;
            Fill(counntParts, part);
        }

        public void RemovePart(Part part)
        {
            if (_parts.Count > 0)
            {
                _parts.Remove(_parts.First());
            }
            else
            {
                Console.WriteLine("Коробка пуста!");
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{_parts[0].Name} - {_parts.Count}шт.");
        }

        public Part GetPart()
        {
            return _parts.Count > 0 ? _parts.First() : null;
        }

        private void Fill(int countParts, Part part)
        {
            for (int i = 0; i < countParts; i++)
            {
                _parts.Add(part);
            }
        }
    }

    class Storage
    {
        private List<Box> _boxes = new List<Box>();

        public Storage(List<Box> boxes)
        {
            _boxes = boxes;
        }

        public void RemovePart(Part part)
        {
            foreach (var box in _boxes)
            {
                if (box.Type == part.Name)
                {
                    box.RemovePart(part);
                    break;
                }
            }
        }

        public void ShowInfo()
        {
            for (int i = 0; i < _boxes.Count; i++)
            {
                Console.Write($"ID: {i + 1} ");
                _boxes[i].ShowInfo();
            }
        }

        public bool TrytGetPart(out Part part, int numberBox)
        {
            if (numberBox < _boxes.Count && numberBox > 0)
            {
                part = _boxes[numberBox].GetPart();
                return true;
            }
            else
            {
                part = null;
                return false;
            }
        }
    }

    class Client
    {
        private static Random _random = new Random();
        private int _maxMoney = 70000;
        private Car _car = new Car();

        public int Money { get; private set; }

        public Client()
        {
            Money = _random.Next(0, _maxMoney);
        }

        public void Pay(int sum)
        {
            Money -= sum;
        }

        public Car GetCar()
        {
            return _car;
        }
    }

    class Car
    {
        private static Random _random = new Random();
        private List<Part> _parts = new List<Part>();

        public Car()
        {
            CreateCar();
            DestroyRandomPart();
        }

        public void ShowInfoBrokenPart()
        {
            foreach (var part in _parts)
            {
                if (part.IsFunctional == false)
                {
                    part.PrintStatus();
                    break;
                }
            }
        }

        public void RepairBrokenPart()
        {
            foreach (var part in _parts)
            {
                if (part.IsFunctional == false)
                {
                    part.Repair();
                    break;
                }
            }
        }

        public Part GetDestroyedPart()
        {
            Part tempPart = null;

            foreach (var part in _parts)
            {
                if (part.IsFunctional == false)
                {
                    tempPart = part;
                    break;
                }
            }

            return tempPart;
        }

        private void CreateCar()
        {
            _parts.Add(new Headlight());
            _parts.Add(new Engine());
            _parts.Add(new Radiator());
            _parts.Add(new Lock());
            _parts.Add(new Headlight());
        }

        private void DestroyRandomPart()
        {
            int numberRandomPart = _random.Next(_parts.Count);
            _parts[numberRandomPart].Break();
        }
    }

    abstract class Part
    {
        public string Name { get; protected set; }

        public int Price { get; protected set; }

        public bool IsFunctional { get; protected set; } = true;

        public void Repair()
        {
            IsFunctional = true;
        }

        public void Break()
        {
            IsFunctional= false;
        }

        public void PrintStatus()
        {
            if (IsFunctional == false)
            {
                Console.WriteLine("{0}\t- [Сломано]", Name);
            }
         }
    }

    class Headlight : Part
    {
        public Headlight()
        {
            Name = "Фара";
            Price = 1555;
        }
    }

    class Engine : Part
    {
        public Engine()
        {
            Name = "Двигатель";
            Price = 30000;
        }
    }

    class Lock : Part
    {
        public Lock()
        {
            Name = "Замок";
            Price = 500;
        }
    }

    class Battary : Part
    {
        public Battary()
        {
            Name = "Батарея";
            Price = 2000;
        }
    }

    class Radiator : Part
    {
        public Radiator()
        {
            Name = "Радиатор";
            Price = 2500;
        }
    }
}
