using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLotDAL.EF;
using AutoLotDAL.Models;
using AutoLotDAL.Models.Base;
using AutoLotDAL.Repos;


namespace AutoLotTestDrive
{
    class Program
    {
        static void Main(string[] args)
        {
            //Database.SetInitializer(new MyDataInitializer());
            Console.WriteLine("Database created and filled!\n\n");
            using (var context = new AutoLotEntities())
            {
                foreach (Inventory c in context.Cars)
                {
                    Console.WriteLine(c);
                }
            }

            Console.WriteLine("Using a repository");
            using (var repo = new InventoryRepo())
            {
                foreach(Inventory c in repo.GetAll())
                {
                    Console.WriteLine(c);
                }
            }

            Console.ReadLine();
        }

        private static void AddNewRecord (Inventory car)
        {
            using (var repo = new InventoryRepo())
            {
                repo.Add(car);
            }
        }

        private static void UpdateRecord (int carId)
        {
            using(var repo = new InventoryRepo())
            {
                var car = repo.GetOne(carId);
                if (car == null)
                    return;
                car.Color = "Blue";
                repo.Save(car);
            }
        }

        private static void RemoveRecordByCar(Inventory car)
        {
            using(var repo=new InventoryRepo())
            {
                repo.Delete(car);
            }
        }

        private static void RemoveRecordById(int carId, byte[] timeStamp)
        {
            using(var repo=new InventoryRepo())
            {
                repo.Delete(carId, timeStamp);
            }
        }

        private static void TestConcurrency()
        {
            var repo1 = new InventoryRepo();
            var repo2 = new InventoryRepo();
            var car1 = repo1.GetOne(1);
            var car2 = repo2.GetOne(1);
            car1.PetName = "New name";
            car2.PetName = "Other name";
            repo1.Save(car1);
            try
            {
                repo2.Save(car2);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var currentValues = entry.CurrentValues;
                var originalValues = entry.OriginalValues;
                var dbValues = entry.GetDatabaseValues();
                Console.WriteLine(" ******** Concurrency ************");
                Console.WriteLine("Type\tPetName");
                Console.WriteLine($"Current:\t{currentValues[nameof(Inventory.PetName)]}");
                Console.WriteLine($"Orig:\t{originalValues[nameof(Inventory.PetName)]}");
                Console.WriteLine($"db:\t{dbValues[nameof(Inventory.PetName)]}");
            }
        }
    }
}
