using BusinessService;
using BusinessService.Models;

namespace DataAccessLayerService
{
    public class CustomerTransactionDataRepository : ICustomerTransactionDataRepository
    {
        public async Task<IEnumerable<CustomerTransaction>> GetAllCustomersTransactions(int numberOfMonths)
        {
            var data = await GetAllTransaction(numberOfMonths);
            return data.Select((dto) => new CustomerTransaction
            {
                CustomerId = dto.CustomerId,
                CustomerName=   dto.CustomerName,
                CustomerMobileNumber = dto.CustomerMobileNumber,
                Amount = dto.Amount,
            });

        }
    public async Task<CustomerTransaction> GetACustomerTransaction(string mobileNumber, int numberOfMonths)
        {
            var dto = await GetOneTransaction(mobileNumber, numberOfMonths);
            return  new CustomerTransaction()
            {
                CustomerId = dto.CustomerId,
                CustomerName = dto.CustomerName,
                CustomerMobileNumber = dto.CustomerMobileNumber,
                Amount = dto.Amount,
            };
        }
        public async Task<CustomerTransactionDTO> GetOneTransaction(string mobileNumber, int numberOfMonths)
        {
            return await SqlHelper.GetTransactionForSingleCustomers(mobileNumber, numberOfMonths);

        }
        public async Task<IEnumerable<CustomerTransactionDTO>> GetAllTransaction(int numberOfMonths)
        {
            return  await SqlHelper.GetTransactionForAllCustomers(numberOfMonths);
           
        }
    }
}