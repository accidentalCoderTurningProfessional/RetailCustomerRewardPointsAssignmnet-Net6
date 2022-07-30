using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RetailCustomerBonusCalculator.BusinessService;
using RetailCustomerBonusCalculator.Controllers;

namespace RetailCustomerBonusCalculator.UnitTests
{
    public class CustomerBonusControllerTestSetup
    {
        protected CustomerBonusController Controller;
        protected Mock<ILogger<CustomerBonusController>> LogHelperMock;
        protected Mock<ICustomerDataService> CustomerDataServiceMock;

        [SetUp]
        public virtual void Setup()
        {
            CustomerDataServiceMock = new Mock<ICustomerDataService>();
            LogHelperMock = new Mock<ILogger<CustomerBonusController>>();
            Controller = CreateController();
        }

        protected CustomerBonusController CreateController()
        {

            var controller = new CustomerBonusController(CustomerDataServiceMock.Object, LogHelperMock.Object);

            return controller;
        }
    }
}