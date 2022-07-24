using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerService
{
    public class CustomerTransactionDTO
    {
        public string CustomerName { get; set; }
        public string CustomerMobileNumber { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
    }
}
