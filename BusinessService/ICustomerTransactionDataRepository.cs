
using BusinessService.Models;

namespace BusinessService
{
    public interface ICustomerTransactionDataRepository
    {
         Task<CustomerTransaction> GetACustomerTransaction(string mobileNumber, int numberOfMonths);
         Task<IEnumerable<CustomerTransaction>> GetAllCustomersTransactions(int numberOfMonths);
    }
}