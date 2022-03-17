using CustomerModel;
using CustomerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Controllers
{
    /// <summary>
    ///  To perform customer operation
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class CustomerController : ControllerBase
    {
        ICommonService _commonService = null;
        ICustomerService _customerService = null;
        private readonly IOptions<APIConfig> _apiConfig;

        /// <summary>
        /// Inject service dependancy through constructor 
        /// </summary>
        /// <param name="commonService"></param>
        /// <param name="customerService"></param>
        /// <param name="apiConfig"></param>
        public CustomerController(ICommonService commonService, ICustomerService customerService, IOptions<APIConfig> apiConfig)
        {
            _commonService = commonService;
            _customerService = customerService;
            _apiConfig = apiConfig;
            _commonService.BaseAddress = apiConfig.Value.Value;
        }

        /// <summary>
        ///  This method to get All customers’ first names (comma separated) who are of age provided.
        /// </summary>
        /// <param name="age">default age is 56 but user can change</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult GetCustomers(int age=56)
        {
            try
            {
                
                _customerService.Customers= _commonService.GetAllCustomers();
                 var data = _customerService.GetCustomerswithAge(age);
                 return StatusCode(StatusCodes.Status200OK, data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// This method to get All customer’s IDs and associated phone numbers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult GetCustomerswithPhoneNumber()
        {
            try
            {
                var customers = _commonService.GetAllCustomers();
                _customerService.Customers = _commonService.GetAllCustomersWithPhoneNumber(customers);
                var data = _customerService.GetCustomerswithPhoneNumber();
                return StatusCode(StatusCodes.Status200OK, data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// This method to get The number of valid phone numbers per state, displayed in ascending alphabetical order
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public IActionResult GetPhoneNumbersPerState()
        {
            try
            {
                var customers = _commonService.GetAllCustomers();
                _customerService.Customers = _commonService.GetAllCustomersWithPhoneNumber(customers);
                var data = _customerService.GetPhoneNumbersPerState();
                return StatusCode(StatusCodes.Status200OK, data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
