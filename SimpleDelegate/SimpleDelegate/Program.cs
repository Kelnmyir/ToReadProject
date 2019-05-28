using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Delegates as event enablers");

            Car c1 = new Car("Jim", 100, 10);
            c1.RegisterWithCarEngine(new Car.CarEngineHandler(OnCarEngineEvent));

            Console.WriteLine("Speeding up");
            for (int i = 0; i < 10; i++)
                c1.Accelerate(20);
            Console.ReadLine();
        }
        public string OnCarEngineEvent()
        {

        }

    }
}
