using BileriumCafe_POS.Utils;
using BileriumCafe_POS.Models;

using System.Text.Json;

namespace BileriumCafe_POS.Services
{
    public class CoffeeService
    {


        private readonly List<Coffee> _coffeeList = new()
        {
            new() { Id =  Guid.NewGuid(), CoffeeName = "Cappuccino", Price = 150.0 },
            new() {Id =  Guid.NewGuid(),  CoffeeName = "Latte", Price = 170.0 },
            new() {Id =  Guid.NewGuid(),  CoffeeName = "Mocha", Price = 140.0 },
            new() {Id =  Guid.NewGuid(),  CoffeeName = "Ristretto", Price = 130.0 },
            new() {Id =  Guid.NewGuid(),  CoffeeName = "Americano", Price = 120.0 },
            new() {Id =  Guid.NewGuid(),  CoffeeName = "Espresso", Price = 110.0 }

        };



        public List<Coffee> GetCoffee()
        {
            string coffeePath = AppUtils.GetCoffeFile();

            if (!File.Exists(coffeePath))
            {
                return new();
            }

            var json = File.ReadAllText(coffeePath);

            return JsonSerializer.Deserialize<List<Coffee>>(json);

        }


        public void SaveCoffeeInFile(List<Coffee> coffees)
        {
            string appDir = AppUtils.GetDesktopDirectoryPath();
            string filePath = AppUtils.GetCoffeFile();

            if (!Directory.Exists(appDir))
            {
                Directory.CreateDirectory(appDir);
            }

            var json = JsonSerializer.Serialize(coffees);

            File.WriteAllText(filePath, json);
        }


        // Seeds JSON file with coffees if JSON file is empty.
        public void SeedCofeeDetails()
        {
            List<Coffee> coffeeList = GetCoffee();

            //SaveCoffeeInFile(_coffeeList);

            if (coffeeList.Count == 0)
            {
                SaveCoffeeInFile(_coffeeList);
            }
        }

        // Get Retrieves a coffee by its ID from the JSON file.
        public Coffee GetCofeeByID(String coffeeID)
        {
            List<Coffee> coffeeList = GetCoffee();
            Coffee coffee = coffeeList.FirstOrDefault(coffee => coffee.Id.ToString() == coffeeID);
            return coffee;
        }

        //Coffee Update
        public void UpdateCoffeeDetails(Coffee coffee)
        {
            List<Coffee> coffeeList = GetCoffee();

            Coffee coffeeForUpdate = coffeeList.FirstOrDefault(_coffee => _coffee.Id.ToString() == coffee.Id.ToString());

            if (coffeeForUpdate == null)
            {
                throw new Exception("No Coffee Found");
            }


            coffeeForUpdate.CoffeeName = coffee.CoffeeName;
            coffeeForUpdate.Price = coffee.Price;
            SaveCoffeeInFile(coffeeList);
        }


    }
}
