using BusinessService;
using BusinessService.Models;
using BusinessService.ServiceResponse;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetailCustomerBonusCalculator.UnitTests
{
    public static class CustomerDataServiceMock
    {
        /// <summary>
        /// simulates a successful call to GetAllCustomersRewardPoints
        /// </summary>
        /// <param name="mock"></param> 
        public static MethodSpy<ICustomerDataService> GetAllCustomersRewardPointsSuccessSetup(this Mock<ICustomerDataService> mock)
            {
                var customer = new CustomerTransaction()
                {
                    Amount = 1,
                    CustomerName = "test",
                    CustomerMobileNumber = "1234567890",
                    CustomerId = 123,
                };
            var customerBonus = new CustomerTransactionWithBonus(customer)
            {
                RewardPoints = 123
            };
                var setup = mock.Setup(m => m.GetAllCustomersRewardPointsData(It.IsAny<int>()))
                    .Returns(ResponseHelpers.GetServiceResponseAsync<IEnumerable<CustomerTransactionWithBonus>>(async () =>
                    {
                        return new List<CustomerTransactionWithBonus>(){ customerBonus};
                    }, ""));

            return mock.CreateSpy(setup);
            }

        /// <summary> 
        /// simulates a exception call to GetAllCustomersRewardPoints
        /// </summary>
        /// <param name="mock"></param>
        public static MethodSpy<ICustomerDataService> GetAllCustomersRewardPointsExceptionSetup(this Mock<ICustomerDataService> mock)
            {
                var setup = mock.Setup(m => m.GetAllCustomersRewardPointsData(It.IsAny<int>()))
                    .Throws(new Exception(TestsHelper.ExpectedExceptionText));

                return mock.CreateSpy(setup);
            }

        /// <summary> 
        /// simulates a successful call to GetSingleCustomerRewardPoint
        /// </summary>
        /// <param name="mock"></param>
        public static MethodSpy<ICustomerDataService> GetSingleCustomerRewardPointSuccessSetup(this Mock<ICustomerDataService> mock)
            {
            var customer = new CustomerTransaction()
            {
                Amount = 1,
                CustomerName = "test",
                CustomerMobileNumber = "1234567890",
                CustomerId = 123,
            };
            var customerBonus = new CustomerTransactionWithBonus(customer)
            {
                RewardPoints = 123
            };

            var setup = mock.Setup(m => m.GetSingleCustomerRewardPointsData(It.IsAny<string>(), It.IsAny<int>()))
                    .Returns(ResponseHelpers.GetServiceResponseAsync(async () =>
                    {
                        return customerBonus;
                    },""));

                return mock.CreateSpy(setup);
            }

        /// <summary>
        /// simulates a exception call to GetSingleCustomerRewardPoint
        /// </summary>
        /// <param name="mock"></param>
        public static MethodSpy<ICustomerDataService> GetSingleCustomerRewardPointsExceptionSetup(this Mock<ICustomerDataService> mock)
            {
                var setup = mock.Setup(m => m.GetSingleCustomerRewardPointsData(It.IsAny<string>(), It.IsAny<int>()))
                    .Throws(new Exception(TestsHelper.ExpectedExceptionText));

                return mock.CreateSpy(setup);
            }

          

}
}
