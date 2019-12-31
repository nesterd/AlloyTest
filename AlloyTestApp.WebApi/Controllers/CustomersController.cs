using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlloyTestApp.Application.Exceptions;
using AlloyTestApp.Core.Entities;
using AlloyTestApp.Core.Interfaces.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlloyTestApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersRepository _customersRepository;

        public CustomersController(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _customersRepository.GetListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            try
            {
                await _customersRepository.AddAsync(customer);
                return Ok();
            }
            catch(UniqueNameException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return BadRequest(ModelState);
            }
            
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            await _customersRepository.DeleteByNameAsync(name);
            return Ok();
        }
    }
}