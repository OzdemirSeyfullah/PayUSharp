using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayU;
using PayU.LiveUpdate;
using PayU.IPN;
using System.Collections.Specialized;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("======= Running the ALU Test =======");
            ALUTest.Run();
            Console.WriteLine("======= Running the LU  Test =======");
            LUTest.Run();
            Console.WriteLine("======= Running the IPN Test =======");
            IPNTest.Run();

            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
