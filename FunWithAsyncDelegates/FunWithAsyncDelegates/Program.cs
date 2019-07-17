using System;
using System.Threading;

namespace FunWithAsyncDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter any int");
            string rawInput = Console.ReadLine();
            int input = Int32.Parse(rawInput);
            IntToLong fc = new IntToLong(Factorial.Calculate);
            IAsyncResult iar = fc.BeginInvoke(input, null, null);
            while (!iar.IsCompleted)
            {
                Console.WriteLine("Waiting...");
                Thread.Sleep(1000);
            }
            Console.WriteLine($"Answer is {fc.EndInvoke(iar)}");
        }
    }
    public static class Factorial
    {
        public static long Calculate (int arg)
        {
            Console.WriteLine("Calling Factorial.Calculate()");
            long res = 1;
            for (int i = 1; i <= arg; i++)
            {
                res *= i;
                Console.WriteLine("Calculating...");
                Thread.Sleep(1000);
            }
            return res;
        }
    }
    public delegate long IntToLong(int i);

}
