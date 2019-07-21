using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLotConsoleApp.EF;
using AutoLotConsoleApp.Models;
using static System.Console;
using System.Data.Entity;

namespace AutoLotConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Fun with EF!\n\n");
            int carID = AddNewRecord();
            WriteLine(carID);
            ReadKey();
        }
        private static int AddNewRecord()
        {
            using (var context = new AutoLotEntities())
            {
                try
                {
                    var car = new Car() { Make = "Yugo", Color = "Brown", CarNickName = "Brownie" };
                    context.Cars.Add(car);
                    context.SaveChanges();
                    return car.CarID;
                }
                catch (Exception ex)
                {
                    WriteLine(ex.InnerException?.Message);
                    return 0;
                }
            }
        }
        private static void AddNewRecords(IEnumerable<Car> carsToAdd)
        {
            using (var context = new AutoLotEntities())
            {
                context.Cars.AddRange(carsToAdd);
                context.SaveChanges();
            }
        }
        private static void PrintAllInventory()
        {
            using (var context = new AutoLotEntities())
            {
                foreach (Car c in context.Cars.Where(c => c.Make == "BMW"))
                    WriteLine(c);
            }
        }
        private static void ChainingLinqQueries()
        {
            using (var context = new AutoLotEntities())
            {
                DbSet<Car> allData = context.Cars;
                var colorsMakes =
                    from item
                    in allData
                    select new
                    {
                        item.Color,
                        item.Make
                    };
                foreach (var item in colorsMakes)
                    WriteLine(item);
            }
        }
    }
}
