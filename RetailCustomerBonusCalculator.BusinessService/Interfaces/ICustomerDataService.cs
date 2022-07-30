
using RetailCustomerBonusCalculator.BusinessService.Models;
using RetailCustomerBonusCalculator.BusinessService.ServiceResponse;

namespace RetailCustomerBonusCalculator.BusinessService
{
    public interface ICustomerDataService
    {
        Task<CustomerTransaction> GetSingleCustomerTransaction(string mobileNumber, int numberOfMonths);
        Task<IEnumerable<CustomerTransaction>> GetAllCustomersTransactions(int numberOfMonths);
        Task<ServiceResponseOf<CustomerTransactionWithBonus>> GetSingleCustomerRewardPointsData(string mobileNumber, int numberOfMonths);
        Task<ServiceResponseOf<IEnumerable<CustomerTransactionWithBonus>>> GetAllCustomersRewardPointsData(int numberOfMonths);
    }
}