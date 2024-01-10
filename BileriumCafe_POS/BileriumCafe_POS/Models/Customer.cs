using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BileriumCafe_POS.Models
{
    public class Customer
    {
        public Guid CustomerID { get; set; } = Guid.NewGuid();
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhoneNum { get; set; }
        public bool RegularMember { get; set; } = false;
        public int RedeemedCoffeeCount { get; set; } = 0;
    }
}
