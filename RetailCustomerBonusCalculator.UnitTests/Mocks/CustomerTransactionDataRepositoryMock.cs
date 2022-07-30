using Moq;
using RetailCustomerBonusCalculator.BusinessService;
using RetailCustomerBonusCalculator.BusinessService.Models;
using System;
using System.Collections.Generic;

namespace RetailCustomerBonusCalculator.UnitTests.Mocks
{
    public static class CustomerTransactionDataRepositoryMock
    {
        /// <summary>
        /// simulates a successful call to GetAllCustomersTransactions
        /// </summary>
        /// <param name="mock"></param> 
        public static MethodSpy<ICustomerTransactionDataRepository> GetAllCustomersTransactions(this Mock<ICustomerTransactionDataRepository> mock)
            {
                var customer = new CustomerTransaction()
                {
                    Amount = 1,
                    CustomerName = "test",
                    CustomerMobileNumber = "1234567890",
                    CustomerId = 123,
                };
                var setup = mock.Setup(m => m.GetAllCustomersTransactions(It.IsAny<int>()))
                    .Returns(async () =>
                    {
                        return new List<CustomerTransaction>() { customer };
                    });

                return mock.CreateSpy(setup);
            }

        /// <summary> 
        /// simulates a exception call to GetAllCustomersTransactionsExceptionSetup
        /// </summary>
        /// <param name="mock"></param>
        public static MethodSpy<ICustomerTransactionDataRepository> GetAllCustomersTransactionsExceptionSetup(this Mock<ICustomerTransactionDataRepository> mock)
            {
                var setup = mock.Setup(m => m.GetAllCustomersTransactions(It.IsAny<int>()))
                    .Throws(new Exception(TestsHelper.ExpectedExceptionText));

                return mock.CreateSpy(setup);
            }

        /// <summary> 
        /// simulates a successful call to GetSingleCustomerTransaction
        /// </summary>
        /// <param name="mock"></param>
        public static MethodSpy<ICustomerTransactionDataRepository> GetSingleCustomerTransaction(this Mock<ICustomerTransactionDataRepository> mock)
            {
                var customer = new CustomerTransaction()
                {
                    Amount = 51,
                    CustomerName = "test",
                    CustomerMobileNumber = "1234567890",
                    CustomerId = 123,
                };

                var setup = mock.Setup(m => m.GetSingleCustomerTransaction(It.IsAny<string>(), It.IsAny<int>()))
                        .Returns(async () =>
                        {
                            return customer;
                        });

                return mock.CreateSpy(setup);
            }

        /// <summary>
        /// simulates a exception call to GetACustomerTransactionAsyncExceptionSetup
        /// </summary>
        /// <param name="mock"></param>
        public static MethodSpy<ICustomerTransactionDataRepository> GetACustomerTransactionAsyncExceptionSetup(this Mock<ICustomerTransactionDataRepository> mock)
            {
                var setup = mock.Setup(m => m.GetSingleCustomerTransaction(It.IsAny<string>(), It.IsAny<int>()))
                    .Throws(new Exception(TestsHelper.ExpectedExceptionText));

                return mock.CreateSpy(setup);
            }



        }
}
