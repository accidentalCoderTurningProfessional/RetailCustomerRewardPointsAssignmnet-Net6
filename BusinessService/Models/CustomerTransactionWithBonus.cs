using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Models
{
    public class CustomerTransactionWithBonus : CustomerTransaction
    {
        public CustomerTransactionWithBonus(CustomerTransaction customer)
        {
            this.Amount = customer.Amount;
            this.CustomerName = customer.CustomerName;
            this.CustomerMobileNumber = customer.CustomerMobileNumber;
            this.CustomerId = customer.CustomerId;
                
        }
         public decimal RewardPoints { get; set; }
    }
}
