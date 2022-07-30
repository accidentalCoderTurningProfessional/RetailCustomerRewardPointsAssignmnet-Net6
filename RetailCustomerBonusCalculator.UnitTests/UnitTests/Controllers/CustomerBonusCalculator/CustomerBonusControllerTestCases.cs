using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using RetailCustomerBonusCalculator.BusinessService.Models;
using RetailCustomerBonusCalculator.BusinessService.ServiceResponse;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailCustomerBonusCalculator.UnitTests
{

    [TestFixture]
    internal class CustomerBonusControllerTestCases : CustomerBonusControllerTestSetup
    {

        /// <summary>
        /// 1.  should return an OKResult with GetAllCustomersTransactions List.
        /// </summary>
        [Test]
        public async Task GetAllCustomersTransactions()
        {
            var setup = CustomerDataServiceMock.GetAllCustomersRewardPointsSuccessSetup();

            var result = await Controller.GetAllCustomersTransactions(2);

            Assert.AreEqual(((ServiceResponseOf<IEnumerable<CustomerTransactionWithBonus>>)((OkObjectResult)result).Value).Content.FirstOrDefault().CustomerMobileNumber, "1234567890");
            Assert.AreEqual(200,(result as OkObjectResult).StatusCode);
            setup.WasCalled();
        }

        /// <summary>
        /// 1.  should return an OKResult with GetACustomerTransaction .
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetACustomerTransaction()
        {
            var setup = CustomerDataServiceMock.GetSingleCustomerRewardPointSuccessSetup();

            var result = await Controller.GetACustomerTransaction("123", 2);
            Assert.AreEqual(((ServiceResponseOf<CustomerTransactionWithBonus>)((OkObjectResult)result).Value).Content.CustomerMobileNumber, "1234567890");
            Assert.AreEqual(200, (result as OkObjectResult).StatusCode);
            setup.WasCalled();
        }

        /// <summary>
        /// 1.  should return an BadRequestResult with GetAllCustomersTransactions List.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetAllCustomersTransactionsException()
        {
            var setup = CustomerDataServiceMock.GetAllCustomersRewardPointsExceptionSetup();


            var result = await Controller.GetAllCustomersTransactions(2);

            Assert.IsTrue(result is BadRequestResult);
            setup.WasCalled();
        }

        /// <summary>
        ///  1.  should return an BadRequestResult with GetACustomerTransaction.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetACustomerTransactionException()
        {
            var setup = CustomerDataServiceMock.GetSingleCustomerRewardPointsExceptionSetup();


            var result = await Controller.GetACustomerTransaction("", 2);

            Assert.IsTrue(result is BadRequestResult);
            setup.WasCalled();
        }
    }
}
