using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BileriumCafe_POS.Models
{
    public class Order
    {
        public Guid OrderID { get; set; } = Guid.NewGuid();
        public Guid CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNum { get; set; }
        public string EmployeeName { get; set; }
        public DateTime OrderDateTime { get; set; } = DateTime.Now;
        public List<OrderItemModel> OrderItems { get; set; }
        public double OrderTotalAmount { get; set; }
        public double DiscountAmount { get; set; } = 0;
    }
}
