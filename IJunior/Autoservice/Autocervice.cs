using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice2
{
    internal class Autocervice
    {
        static void Main(string[] args)
        {
            Autoservice autoservice = new Autoservice();
            autoservice.StartWork();
        }
    }

    class Autoservice
    {
        private static Random _random = new Random();
        private int _days;
        private int _money;
        private Queue<Client> _clients;

        public int Day { get; private set; }

        public Autoservice()
        {
            _money = 0;
            CreateClientsQueue();
        }

        public void StartWork()
        {
            Day = ++_days;

            for (int i = _clients.Count; i > 0; i--)
            {
                Console.Clear();
                Console.WriteLine($"День : {Day}");

                Client tempClient = _clients.Dequeue();
                Car tempCar = tempClient.GetCar();

                PrintCashInfo();
                PrintClientInfo(tempClient);
                PrintClientCarInfo(tempCar);

                if (tempCar.GetStatus() == false && tempClient.Money > tempCar.GetAmountRepair())
                {
                    RepairCar(tempCar);
                }
                else
                {
                    Console.WriteLine("У клиента не хватило денег!");
                }

                Console.ReadKey();
            }
        }

        private void RepairCar(Car car)
        {
            Console.Write("Выберите запчасть для ремонта: ");
            bool isConverted = int.TryParse(Console.ReadLine(), out int id);

            if (isConverted)
            {

            }
        }

        private void PrintCashInfo()
        {
            string cashInfo = $"Денег в кассе: {_money}\n";
            Console.SetCursorPosition(Console.BufferWidth - cashInfo.Length, 0);
            Console.Write(cashInfo);
        }

        private void PrintClientInfo(Client client)
        {
            string clientsInfo = $"Клментов в очереди: {_clients.Count}\n";
            Console.Write(clientsInfo);
        }

        private void PrintClientCarInfo(Car car)
        {
            if (car.GetStatus() == true)
            {
                Console.WriteLine("Автомобиль криента оказался исправным и он уехал!");
            }
            else
            {
                car.ShowInfoDiagnostic();
            }
        }

        private void CreateClientsQueue()
        {
            int minCountClients = 10;
            int maxCountClients = 30;
            int countClients = _random.Next(minCountClients, maxCountClients);
            _clients = new Queue<Client>();

            for (int i = 0; i < countClients; i++)
            {
                _clients.Enqueue(new Client());
            }
        }
    }

    class Client
    {
        private static Random _random = new Random();
        private Car _car;

        public int Money { get; private set; }

        public Client()
        {
            _car = new Car();
            Money = _random.Next(0, 100000);
        }

        public bool TryPay(int sum)
        {
            if (Money >= sum)
            {
                Money -= sum;
                return true;
            }
            else
            {
                return false;
            }
        }

        public Car GetCar()
        {
            return _car;
        }
    }

    class Car
    {
        private static Random _random = new Random();
        private List<CarPart> _parts = new List<CarPart>();

        public Car()
        {
            Create();
            DestroyRandomParst();
        }

        public void ShowInfoDiagnostic()
        {
            int repairSum = 0;

            for (int i = 0; i < _parts.Count; i++)
            {
                if (_parts[i].IsServiceable == false)
                {
                    repairSum += _parts[i].Price;
                }

                Console.Write($"ID: {i + 1}\t");
                _parts[i].ShowInfo();
                Console.WriteLine();
            }
        }

        public bool GetStatus()
        {
            bool status = true;

            foreach (var part in _parts)
            {
                if (part.IsServiceable == false)
                {
                    status = false;
                }
            }

            return status;
        }

        public int GetAmountRepair()
        {
            int repairSum = 0;

            for (int i = 0; i < _parts.Count; i++)
            {
                if (_parts[i].IsServiceable == false)
                {
                    repairSum += _parts[i].Price;
                }
            }

            return repairSum;
        }

        private void Create()
        {
            int countWheel = 4;

            for (int i = 0; i < countWheel; i++)
            {
                _parts.Add(new Wheel());
            }

            _parts.Add(new Engine());
            _parts.Add(new Battary());
            _parts.Add(new Radiator());
            _parts.Add(new Body());
        }

        private void DestroyRandomParst()
        {
            int failureProbability = 25;
            int maxPercentage = 100;

            foreach (var part in _parts)
            {
                int percentage = _random.Next(maxPercentage);
                bool isServiceable = percentage < failureProbability ? true : false;

                if (isServiceable)
                {
                    part.Destroy();
                }
            }
        }
    }

    abstract class CarPart
    {
        private static Random _random = new Random();
        private string _condition = "";

        public bool IsServiceable { get; private set; }

        public string Name { get; protected set; }

        public int Price { get; protected set; }

        public CarPart()
        {
            IsServiceable = true;
        }

        public void Fix()
        {
            IsServiceable = true;
        }

        public void Destroy()
        {
            IsServiceable = false;
        }

        public void ShowInfo()
        {
            Console.Write($"[{Name}] \t- ");

            if (IsServiceable == true)
            {
                _condition = "[Исправно]";
                PrintConditionPart(_condition, ConsoleColor.Green);
            }
            else
            {
                _condition = "[Сломано]";
                PrintConditionPart(_condition, ConsoleColor.Red);
            }
        }

        private void PrintConditionPart(string condition, ConsoleColor color)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(condition);
            Console.ForegroundColor = defaultColor;
        }
    }

    class Wheel : CarPart
    {
        private int _price = 1500;
        private string _name = "Колесо";

        public Wheel()
        {
            Name = _name;
            Price = _price;
        }
    }

    class Engine : CarPart
    {
        private int _price = 30000;
        private string _name = "Двигатель";

        public Engine()
        {
            Name = _name;
            Price = _price;
        }
    }

    class Body : CarPart
    {
        private int _price = 5000;
        private string _name = "Кузов";

        public Body()
        {
            Name = _name;
            Price = _price;
        }
    }

    class Battary : CarPart
    {
        private int _price = 2000;
        private string _name = "Баттарея";

        public Battary()
        {
            Name = _name;
            Price = _price;
        }
    }

    class Radiator : CarPart
    {
        private int _price = 2500;
        private string _name = "Радиатор";

        public Radiator()
        {
            Name = _name;
            Price = _price;
        }
    }
}
