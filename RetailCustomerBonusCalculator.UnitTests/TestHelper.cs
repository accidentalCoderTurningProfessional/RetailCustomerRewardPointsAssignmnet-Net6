using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace RetailCustomerBonusCalculator.UnitTests
{
    /// <summary>
    /// Contains helper properties and methods for tests.
    /// </summary>
    public static class TestsHelper
    {
        public const string ExpectedExceptionText = "expected exception";
        /// <summary>
        /// Indicates that integration tests are ignored. Useful when calculating code coverage locally.
        /// </summary>
        public static bool IgnoreIntegrationTests { get; set; }

        /// <summary>
        /// Checks if the given ActionResult is a BadRequest result with the given exception message as value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="message"></param>
        public static void IsBadRequestException<T>(this ActionResult<T> result, string message = "expected exception")
        {
            Assert.IsTrue(result.Result is BadRequestObjectResult { Value: string m } && m == message);
        }

        /// <summary>
        /// Checks if the given ActionResult is an Ok result with value true.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        public static void IsOkTrue<T>(this ActionResult<T> result)
        {
            Assert.IsTrue(result.Result is OkObjectResult { Value: bool v } && v);
        }
    }
}
