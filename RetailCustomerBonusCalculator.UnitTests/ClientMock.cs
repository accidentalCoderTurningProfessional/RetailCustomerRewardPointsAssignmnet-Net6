using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailCustomerBonusCalculator.UnitTests
{
    public static class ClientMock
    {
        /// <summary>
        /// Creates a MethodSpy that can be returned from a client or service mock.
        /// If there are any inputs these are prepared so <see cref="WasCalledWith{T}"/> can be used.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mock"></param>
        /// <param name="setup"></param>
        /// <param name="inputs"></param>
        public static MethodSpy<T> CreateSpy<T>(this Mock<T> mock, IFluentInterface setup,
            params (string name, string value)[] inputs) where T : class
        {
            var setupString = setup.ToString();
            if (inputs != null && inputs.Any())
            {
                setupString = StringifySetup(setup, inputs);
            }

            return new MethodSpy<T>(mock, setupString);
        }
        /// <summary>
        /// Prepares a Setup so that it can be used in e.g. <see cref="WasCalled{T}"/>.
        /// </summary>
        /// <param name="setup"></param>
        /// <param name="inputs"></param>
        private static string StringifySetup(object setup, params (string name, string value)[] inputs)
        {
            var setupString = setup.ToString() ?? string.Empty;
            foreach (var (name, value) in inputs)
            {
                setupString = setupString.Replace(name, value);
            }

            return setupString;
        }
        /// <summary>
        /// Checks if the given setup was called. Currently ony works for parameter-less Setups.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spy"></param>
        public static void WasCalled<T>(this MethodSpy<T> spy) where T : class
        {
            var (wasCalled, invocationsList) = SetupCalled<T>((spy.Mock, spy.Call));
            Assert.IsTrue(wasCalled,
                $"expected\n\n{spy.Call}\n\nto have been called.\n Invocations where:\n\n{string.Join(",\n\n", invocationsList)}\n\n");
        }
        /// <summary>
        /// Verifies that the given setup was called.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        private static (bool wasCalled, List<string> invocations) SetupCalled<T>((Mock mock, string call) param)
            where T : class
        {
            var invocationsList = param.mock.Invocations.Select(i =>
                    i.MatchingSetup?.ToString()?.Replace(typeof(T).Name, string.Empty).Trim()
                        .Replace("\"", string.Empty))
                .ToList();

            return (invocationsList.Any(i => i == param.call), invocationsList);
        }
    }
}
