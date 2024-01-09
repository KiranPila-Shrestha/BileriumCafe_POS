using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BileriumCafe_POS.Models
{
    public class AddIn
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ProductName { get; set; }
        public double Price { get; set; }
    }
}
