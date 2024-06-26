using BusinessObject.Model;
using eStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.impl;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository repository = new CustomerRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomers() => repository.GetCustomers();

        [HttpGet("Search/{keyword}")]
        public ActionResult<IEnumerable<Customer>> Search(string keyword) => repository.Search(keyword);

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomerById(string id) => repository.GetCustomerById(id);

        [HttpGet("Email/{email}")]
        public ActionResult<Customer> GetCustomerByEmail(string email) => repository.GetCustomerByEmail(email);

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(string id)
        {
            Customer c = repository.GetCustomerById(id);
            if (c == null)
            {
                return NotFound();
            }
            repository.DeleteCustomer(c);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult PutCustomer(string id, PutCustomer putCustomer)
        {
            Customer cTmp = repository.GetCustomerById(id);
            if (cTmp == null)
            {
                return NotFound();
            }

            cTmp.FirstName = putCustomer.FirstName;
            cTmp.LastName = putCustomer.LastName;

            if (putCustomer.Password != null && cTmp.PasswordHash != putCustomer.Password)
            {
                cTmp.PasswordHash = putCustomer.Password;
            }

            repository.UpdateCustomer(cTmp);
            return NoContent();
        }
    }
}
