using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class CustomerTransaction
    {
        public string CustomerName { get; set; }
        public string CustomerMobileNumber { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
    }
}
