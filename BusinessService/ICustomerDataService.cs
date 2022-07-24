
using BusinessService.Models;
using BusinessService.ServiceResponse;

namespace BusinessService
{
    public interface ICustomerDataService
    {
        Task<CustomerTransaction> GetACustomerTransaction(string mobileNumber, int numberOfMonths);
        Task<IEnumerable<CustomerTransaction>> GetAllCustomersTransactions(int numberOfMonths);
        Task<ServiceResponseOf<CustomerTransactionWithBonus>> GetSingleCustomerRewardPointsData(string mobileNumber, int numberOfMonths);
        Task<ServiceResponseOf<IEnumerable<CustomerTransactionWithBonus>>> GetAllCustomersRewardPointsData(int numberOfMonths);
    }
}