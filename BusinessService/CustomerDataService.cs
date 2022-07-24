using BusinessService.Models;
using BusinessService.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService
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
        public async Task<CustomerTransaction> GetACustomerTransaction(string mobileNumber, int numberOfMonths)
        { 
            return await repository.GetACustomerTransaction(mobileNumber, numberOfMonths);
        }

        public async Task<ServiceResponseOf<CustomerTransactionWithBonus>> GetSingleCustomerRewardPointsData(string mobileNumber, int numberOfMonths)
        {
          
            return await ResponseHelpers.GetServiceResponseAsync<CustomerTransactionWithBonus>(async () =>
            {
                var data = await GetACustomerTransaction(mobileNumber, numberOfMonths);
                var bonus = new CustomerTransactionWithBonus(data) { RewardPoints = new BonusPointCalculator().GetBonusPoint(data.Amount) };

                return bonus;
            }
            , ""); ;
        }

        public async Task<ServiceResponseOf<IEnumerable<CustomerTransactionWithBonus>>> GetAllCustomersRewardPointsData(int numberOfMonths)
        {
            // bonus = bonus == null ? new List<CustomerTransactionWithBonus>() : bonus;
            return await ResponseHelpers.GetServiceResponseAsync<IEnumerable<CustomerTransactionWithBonus>>(async () =>
            {
                var data = await GetAllCustomersTransactions(numberOfMonths);
                var bonus = data.Select(x => new CustomerTransactionWithBonus(x) { RewardPoints = new BonusPointCalculator().GetBonusPoint(x.Amount) });

                return bonus;
            }
            , ""); ;
        }
    }
}
