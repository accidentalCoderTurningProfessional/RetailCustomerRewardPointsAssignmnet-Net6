using RetailCustomerBonusCalculator.BusinessService.Models;
using RetailCustomerBonusCalculator.BusinessService.ServiceResponse;

namespace RetailCustomerBonusCalculator.BusinessService
{
    public class CustomerDataService : ICustomerDataService
    {
        private readonly ICustomerTransactionDataRepository repository;
        public CustomerDataService(ICustomerTransactionDataRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<CustomerTransaction>> GetAllCustomersTransactions(int numberOfMonths)
        {
            return await repository.GetAllCustomersTransactions(numberOfMonths);
        }
        public async Task<CustomerTransaction> GetSingleCustomerTransaction(string mobileNumber, int numberOfMonths)
        { 
            return await repository.GetSingleCustomerTransaction(mobileNumber, numberOfMonths);
        }

        public async Task<ServiceResponseOf<CustomerTransactionWithBonus>> GetSingleCustomerRewardPointsData(string mobileNumber, int numberOfMonths)
        {
          
            return await ResponseHelpers.GetServiceResponseAsync(async () =>
            {
                var data = await GetSingleCustomerTransaction(mobileNumber, numberOfMonths);
                var bonus = new CustomerTransactionWithBonus(data) { RewardPoints = new BonusPointCalculator().GetBonusPoint(data.Amount) };

                return bonus;
            }
            , ""); ;
        }

        public async Task<ServiceResponseOf<IEnumerable<CustomerTransactionWithBonus>>> GetAllCustomersRewardPointsData(int numberOfMonths)
        {
            return await ResponseHelpers.GetServiceResponseAsync(async () =>
            {
                var data = await GetAllCustomersTransactions(numberOfMonths);
                var bonus = data.Select(x => new CustomerTransactionWithBonus(x) { RewardPoints = new BonusPointCalculator().GetBonusPoint(x.Amount) });

                return bonus;
            }
            , "") ; ;
        }
    }
}
