using NUnit.Framework;
using RetailCustomerBonusCalculator.BusinessService.Models;
using RetailCustomerBonusCalculator.UnitTests.Mocks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailCustomerBonusCalculator.UnitTests.UnitTests.Services
{
    [TestFixture]
    public class CustomerDataServiceTest: CustomerDataServiceTestSetup
    {
        /// <summary>
        /// Should successfully return a list of customers
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetAllCustomersTransactionsAsync()
        {
            var setup = customerTransactionDataRepository.GetAllCustomersTransactions();
            var result = await Service.GetAllCustomersTransactions(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("test", ((List<CustomerTransaction>)result)[0].CustomerName);
            setup.WasCalled();
        }
        /// <summary>
        /// should succefully calculate reward points for all the customers
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetAllCustomersRewardPointsDataAsync()
        {
            var setup = customerTransactionDataRepository.GetAllCustomersTransactions();
            var result = await Service.GetAllCustomersRewardPointsData(1);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Content.FirstOrDefault()?.RewardPoints);
            setup.WasCalled();
        }

        /// <summary>
        /// should successfully return a single customer
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetACustomerTransactionAsync()
        {
            var setup = customerTransactionDataRepository.GetSingleCustomerTransaction();
            var result = await Service.GetSingleCustomerTransaction("", 1);
            Assert.IsNotNull(result);
            Assert.AreEqual("test", result.CustomerName);
            setup.WasCalled();
        }

        /// <summary>
        /// should succefully calculate reward points for the given customer
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetSingleCustomerRewardPointsDataAsync()
        {
            var setup = customerTransactionDataRepository.GetSingleCustomerTransaction();
            var result = await Service.GetSingleCustomerRewardPointsData("", 1);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.RewardPoints);
            setup.WasCalled();
        }
    }
}
