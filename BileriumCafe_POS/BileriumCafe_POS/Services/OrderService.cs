using BileriumCafe_POS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using BileriumCafe_POS.Models;

namespace BileriumCafe_POS.Services
{
    public class OrderService
    {
        //Get order list from json file
        public List<Order> GetOrdersFromJsonFile()
        {
            string orderListFilePath = AppUtils.GetOrderListPath();

            if (!File.Exists(orderListFilePath))
            {
                return new List<Order>();
            }

            var json = File.ReadAllText(orderListFilePath);

            return JsonSerializer.Deserialize<List<Order>>(json);
        }

        //Append new order to the order list of json file
        public void PlaceOrder(Order order)
        {
            List<Order> orders = GetOrdersFromJsonFile();
            orders.Add(order);

            // json files stored path (AppUtils).
            string appDataDirPath = AppUtils.GetDesktopDirectoryPath();
            string orderListFilePath = AppUtils.GetOrderListPath();

            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(orders);

            File.WriteAllText(orderListFilePath, json);
        }
    }
}

