using Moq;

namespace RetailCustomerBonusCalculator.UnitTests
{
    /// <summary>
    /// Represents an object that can be used to check if a method (the given setup) was called.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MethodSpy<T> where T : class
    {
        public Mock<T> Mock { get; set; }
        public string Call { get; set; }

        public MethodSpy(Mock<T> mock, IFluentInterface setup)
        {
            Mock = mock;
            Call = setup.ToString();
        }

        public MethodSpy(Mock<T> mock, string setup)
        {
            Mock = mock;
            Call = setup;
        }
    }
}
