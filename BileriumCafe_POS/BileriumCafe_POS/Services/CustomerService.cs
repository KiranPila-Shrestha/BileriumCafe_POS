using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BileriumCafe_POS.Models;
using BileriumCafe_POS.Utils;

namespace BileriumCafe_POS.Services
{
    public class CustomerService
    {

        private OrderService _orderServices;

        public CustomerService(OrderService orderServices)
        {
            _orderServices = orderServices;
        }

        // Get list of customer from the JSON file.
        public List<Customer> GetCustomerListFromJsonFile()
        {
            string customersFilePath = AppUtils.GetCustomerListPath();

            if (!File.Exists(customersFilePath))
            {
                return new List<Customer>();
            }

            var json = File.ReadAllText(customersFilePath);

            return JsonSerializer.Deserialize<List<Customer>>(json);

        }

        // Saves customer list in json file.
        public void SaveCustomerListInJsonFile(List<Customer> customers)
        {
            string appDataDirPath = AppUtils.GetDesktopDirectoryPath();
            string customerListFilePath = AppUtils.GetCustomerListPath();

            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(customers);

            File.WriteAllText(customerListFilePath, json);
        }

        // Get Customer phone number from json file
        public Customer GetCustomerByPhoneNum(string customerPhoneNum)
        {
            List<Customer> customers = GetCustomerListFromJsonFile();
            Customer customer = customers.FirstOrDefault(c => c.CustomerPhoneNum == customerPhoneNum);
            return customer;
        }
        //for adding new Customer in list and update values
        public void AddCustomer(Customer _customer)
        {

            Customer isCustomerExists = GetCustomerByPhoneNum(_customer.CustomerPhoneNum);

            if (isCustomerExists != null)
            {
                throw new Exception("Customer Already exists");
            }

            List<Customer> customers = GetCustomerListFromJsonFile();

            customers.Add(_customer);

            SaveCustomerListInJsonFile(customers);
        }

  
        //update customer count and save it to json file
        public void UpdateRedeemedCoffeeCount(string customerPhoneNum, int redeemedCoffeeCount)
        {
            List<Customer> customers = GetCustomerListFromJsonFile();
            Customer customer = customers.FirstOrDefault(c => c.CustomerPhoneNum == customerPhoneNum);
            customer.RedeemedCoffeeCount = redeemedCoffeeCount;

            SaveCustomerListInJsonFile(customers);
        }

    
        //This method is for free coffee to customer 
        public bool CheckIfCustomerIsReguralMember(string customerPhoneNum)
        {
            List<Order> orders = _orderServices.GetOrdersFromJsonFile();

            
            // Check condition for date, Month n Year.
            int month = DateTime.Now.Month - 1;
            int year = month == 12 ? DateTime.Now.Year - 1 : DateTime.Now.Year;

            
            // here orders is sorted by customer phone and limit to those placed in previous month,
            //Then the order are group on daily basis which determine total count of order.
            int totalOrderCount = orders
                .Where(order => order.CustomerPhoneNum == customerPhoneNum && order.OrderDateTime.Month == month && order.OrderDateTime.Year == year)
                .GroupBy(order => order.OrderDateTime.Day)
                .ToList().Count();

            //Returns true if the total order count of customer who is regular.
            return totalOrderCount >= 26;
        }

        //  count the total free coffee of the customer.
        public int TotalFreeCoffeeCount(string customerPhoneNum)
        {

            List<Order> orders = _orderServices.GetOrdersFromJsonFile();

            int totalOrderCount = orders
                .Where(order => order.CustomerPhoneNum == customerPhoneNum)
                .ToList().Count();

            return totalOrderCount / 10;
        }

    }
}

