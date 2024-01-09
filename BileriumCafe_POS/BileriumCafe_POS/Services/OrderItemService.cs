using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BileriumCafe_POS.Models;

namespace BileriumCafe_POS.Services
{
    public class OrderItemService
    {
        // Adds an item to the list of order items and Increases the quantity if duplicates items exists.
        public void AddItemInOrderItemsList(List<OrderItemModel> _orderItems, Guid itemID, string itemName, String itemType, Double Price)
        {

            OrderItemModel orderItem = _orderItems.FirstOrDefault(x => x.ItemID.ToString() == itemID.ToString() && x.ItemType == itemType);

            if (orderItem != null)
            {
                orderItem.Quantity++;
                orderItem.TotalPrice = orderItem.Quantity * Price;

                return;
            }

            orderItem = new()
            {
                ItemID = itemID,
                ItemName = itemName,
                ItemType = itemType,
                Quantity = 1,
                Price = Price,
                TotalPrice = Price
            };

            _orderItems.Add(orderItem);

        }

        // Deletes an item from the list of order items.
        public void DeleteItemInOrderItemsList(List<OrderItemModel> _orderItems, Guid orderItemID)
        {
            OrderItemModel orderItem = _orderItems.FirstOrDefault(x => x.OrderItemID == orderItemID);

            if (orderItem != null)
            {
                _orderItems.Remove(orderItem);
            }
        }

        // Manages the quantity of an existing order item(add or subtract).
        public void ManageQuantityOfOrderItem(List<OrderItemModel> _orderItems, Guid orderItemID, string action)
        {
            OrderItemModel orderItem = _orderItems.FirstOrDefault(x => x.OrderItemID == orderItemID);

            if (orderItem != null)
            {
                if (action == "add")
                {
                    orderItem.Quantity++;
                    orderItem.TotalPrice = orderItem.Quantity * orderItem.Price;
                }
                else if (action == "subtract" && orderItem.Quantity > 1)
                {
                    orderItem.Quantity--;
                    orderItem.TotalPrice = orderItem.Quantity * orderItem.Price;
                }
            }
        }

        // Calculates the total amount of all order items.
        public double CalculateTotalAmount(IEnumerable<OrderItemModel> Elements)
        {
            double totalAmount = 0;

            foreach (var item in Elements)
            {
                totalAmount += item.TotalPrice;
            }
            return totalAmount;
        }



    }
}
