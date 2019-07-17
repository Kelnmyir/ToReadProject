using System;
using System.Threading;
using System.Collections.Generic;

namespace FunWithAsyncDelegates
{
    class Program
    {
        private static int fcIsDone = 0;
        static void Main()
        {
            List<IAsyncResult> calculatings = new List<IAsyncResult>();
            IntToLong fc = new IntToLong(Factorial.Calculate);
            string rawInput;
            int input=0;
            do
            {
                if (input > 0)
                    calculatings.Add(fc.BeginInvoke(input, new AsyncCallback(CheckComplete), null));
                Console.WriteLine("Enter any int to calculate its factorial \nor anything else to quit.");
                rawInput = Console.ReadLine();
            }
            while (Int32.TryParse(rawInput, out input));
            while (fcIsDone<calculatings.Count)
            {
                Console.WriteLine("Wait, please...");
                Thread.Sleep(1000);
            }
            for (int i=0; i < calculatings.Count; i++)
            {
                Console.WriteLine($"Result number {i}:   {fc.EndInvoke(calculatings[i])}");
            }
        }
        static void CheckComplete(IAsyncResult iar)
        {
            fcIsDone += 1;
        }
    }
    public static class Factorial
    {
        public static long Calculate(int arg)
        {
            Console.WriteLine("Calling Factorial.Calculate()");
            long res = 1;
            for (int i = 1; i <= arg; i++)
            {
                res += i;
                //Console.WriteLine("Calculating...");
                Thread.Sleep(1000);
            }
            return res;
        }
    }
    public delegate long IntToLong(int i);
}
