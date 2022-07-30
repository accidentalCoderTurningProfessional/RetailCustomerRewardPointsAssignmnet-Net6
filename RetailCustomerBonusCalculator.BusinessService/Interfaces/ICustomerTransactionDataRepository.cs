
using RetailCustomerBonusCalculator.BusinessService.Models;

namespace RetailCustomerBonusCalculator.BusinessService
{
    public interface ICustomerTransactionDataRepository
    {
         Task<CustomerTransaction> GetSingleCustomerTransaction(string mobileNumber, int numberOfMonths);
         Task<IEnumerable<CustomerTransaction>> GetAllCustomersTransactions(int numberOfMonths);
    }
}