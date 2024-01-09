using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BileriumCafe_POS.Models;

namespace BileriumCafe_POS.Services
{
    public class ReportService
    {
        private OrderService _orderServices { get; set; }

        public ReportService(OrderService orderServices)
        {
            _orderServices = orderServices;
        }

        //Generates the order list monthly or daily
        public List<Order> GenerateOrderList(string reportType, string reportDate)
        {
            List<Order> orders = _orderServices.GetOrdersFromJsonFile();

            if (reportType.ToLower() == "monthly")
            {
                orders = orders.Where(x => reportDate == x.OrderDateTime.ToString("MM-yyyy")).ToList();
            }
            else if (reportType.ToLower() == "daily")
            {
                orders = orders.Where(x => reportDate == x.OrderDateTime.ToString("dd-MM-yyyy")).ToList();
            }

            return orders;
        }

        public Dictionary<string, List<OrderItemModel>> GenerateMostPurchasedCoffeeAndAddInsList(List<Order> orders)
        {
            //Get all order items into one list from all Orders
            List<OrderItemModel> allOrderItems = orders
            .SelectMany(order => order.OrderItems)
            .ToList();

            //Get all coffee and add-ins into separate lists
            List<OrderItemModel> coffeeList = allOrderItems.Where(item => item.ItemType == "coffee").ToList();
            List<OrderItemModel> addInsList = allOrderItems.Where(item => item.ItemType == "add-in").ToList();

            //Get the most ordered coffee with the total quantity
            List<OrderItemModel> mostOrderedCoffee = coffeeList
            .GroupBy(coffee => coffee.ItemName)
            .Select(group =>
            {
                var itemName = group.Key;
                int totalQuantity = group.Sum(orderItem => orderItem.Quantity);

                return new OrderItemModel
                {
                    ItemName = itemName,
                    Quantity = totalQuantity,
                };
            }).ToList();

            //Get the most ordered add-ins with the total quantity
            List<OrderItemModel> mostOrderedAddInsItem = coffeeList
            .GroupBy(addIn => addIn.ItemName)
            .Select(group =>
            {
                var itemName = group.Key;
                int totalQuantity = group.Sum(orderItem => orderItem.Quantity);

                return new OrderItemModel
                {
                    ItemName = itemName,
                    Quantity = totalQuantity,
                };
            }).ToList();

            //Return the dictionary of the most ordered coffee and add-ins
            return new Dictionary<string, List<OrderItemModel>>
            {
                { "coffees", mostOrderedCoffee.Take(5).ToList() },
                { "add-ins", mostOrderedAddInsItem.Take(5).ToList() }
            };
        }


    }
}

