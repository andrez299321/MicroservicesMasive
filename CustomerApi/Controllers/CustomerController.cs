using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Roulette.Models;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        AdminCustomer Admin;
        public CustomerController()
        {
            Admin = new AdminCustomer();
        }
        [HttpPost]
        [Route("CreateCustomer")]
        public IActionResult CreateCustomer([FromBody] User customer)
        {
            try
            {
                int result = Admin.CreateCustomer(customer);

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// List to customer
        /// </summary>
        /// <returns>List to customer</returns>
        [HttpGet]
        [Route("List")]
        public IActionResult List()
        {
            try
            {
                return Ok(Admin.ListCustomer());
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
