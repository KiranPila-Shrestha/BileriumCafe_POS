using BileriumCafe_POS.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BileriumCafe_POS.Models
{
    public class User
    {
        public string Role { get; set; }
        public string Password { get; set; }
    }
}