using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int partPrice = 155;
            int _costWorkInPercent = 20;
            int repairCosts = ((partPrice * _costWorkInPercent) / 100) + partPrice;
            Console.WriteLine(repairCosts); 
        }
    }
}
