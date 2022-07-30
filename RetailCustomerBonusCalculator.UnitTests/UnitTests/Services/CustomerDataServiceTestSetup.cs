using Moq;
using NUnit.Framework;
using RetailCustomerBonusCalculator.BusinessService;

namespace RetailCustomerBonusCalculator.UnitTests.UnitTests.Services
{
    public  class CustomerDataServiceTestSetup
        {
            protected CustomerDataService Service;
            protected Mock<ICustomerDataService> customerDataServiceMock;
            protected Mock<ICustomerTransactionDataRepository> customerTransactionDataRepository;

            [SetUp]
            public virtual void Setup()
            {
                customerTransactionDataRepository = new Mock<ICustomerTransactionDataRepository>();
            customerDataServiceMock = new Mock<ICustomerDataService>();
                Service = CreateService();
            }

            protected CustomerDataService CreateService()
            {
                var service = new CustomerDataService(customerTransactionDataRepository.Object)
                {
                };

                return service;
            }
        }
    }
