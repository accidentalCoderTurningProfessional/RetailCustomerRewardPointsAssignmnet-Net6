using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RetailCustomerBonusCalculator.BusinessService;
using RetailCustomerBonusCalculator.BusinessService.Models;
using RetailCustomerBonusCalculator.BusinessService.ServiceResponse;

namespace RetailCustomerBonusCalculator.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class CustomerBonusController : ControllerBase
    {
        private readonly ICustomerDataService customerDataService;
        private readonly ILogger<CustomerBonusController> _logger;
        public CustomerBonusController(ICustomerDataService customerDataService, ILogger<CustomerBonusController> logger)
        {
            this.customerDataService = customerDataService;
            this._logger = logger;
        }

        /// <summary>
        /// Gets the transaction amount and corresponding reward points for all the customer
        /// </summary>
        /// <param name="numberOfMonths"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, typeof(List<CustomerTransactionWithBonus>), Description = "Success of method")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, typeof(List<string>), Description = "Bad request")]
        public async Task<IActionResult> GetAllCustomersTransactions(int numberOfMonths)
        {
            try
            {
                var result = await customerDataService.GetAllCustomersRewardPointsData(numberOfMonths);
                if (result?.Status == ServiceResponseWrapper.ResponseStatuses.Error)
                {
                    this._logger.LogError(result.ErrorSummary);
                    this._logger.LogError(result.Errors[0]);
                }
                else
                {
                    this._logger.LogInformation($"Succefully pulled data for All the Customers for last  { numberOfMonths} months");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Gets the transaction amount and corresponding reward points for a single customer
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="numberOfMonths"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpGet("{mobileNumber}")]
        [SwaggerResponse(StatusCodes.Status200OK, typeof(CustomerTransactionWithBonus), Description = "Success of method")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, typeof(List<string>), Description = "Bad request")]
        public async Task<IActionResult> GetACustomerTransaction(string mobileNumber, int numberOfMonths)
        {
            if (mobileNumber == null) { throw new ArgumentNullException(nameof(mobileNumber)); }
            try
            {

                var result = await customerDataService.GetSingleCustomerRewardPointsData(mobileNumber, numberOfMonths);
                if (result?.Status == ServiceResponseWrapper.ResponseStatuses.Error)
                {
                    this._logger.LogError(result.ErrorSummary);
                    this._logger.LogError(result.Errors[0]);
                }
                else
                {
                    this._logger.LogInformation($"Succefully pulled data for the customer {result?.Content?.CustomerName} for last { numberOfMonths} months");
                }
                return Ok(result);

            }
            catch (Exception ex)
            {

                this._logger.LogError(ex.Message);
                return BadRequest();
            }

        }

    }
}
