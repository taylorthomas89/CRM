using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private CRMContext _context;

        public CustomerController(CRMContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable<CustomerDTO> Get()
        {
            IQueryable<CustomerDTO> customers = from c in _context.Customer
                    select new CustomerDTO()
                    {
                        Id = c.customer_id,
                        Name = c.name,
                        Email = c.email,
                        Phone = c.phone,
                        Age = c.age,
                        Details = c.details
                    };
            
            return customers;
             
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> Get(int? id)
        {   
            if (id == null)
            {
                return NotFound();
            }

            CustomerDTO customer = await _context.Customer
                                        .Include(c => c.details)
                                        .Select(c =>
                                            new CustomerDTO()
                                            {
                                                Id = c.customer_id,
                                                Name = c.name,
                                                Email = c.email,
                                                Phone = c.phone,
                                                Age = c.age,
                                                Details = c.details
                                            }
                                        )
                                        .SingleOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            _context.Entry(customer).Reference(c => c.details).Load();
            CustomerDTO dto = new CustomerDTO()
                                {
                                    Id = customer.customer_id,
                                    Name = customer.name,
                                    Email = customer.email,
                                    Phone = customer.phone,
                                    Age = customer.age,
                                    Details = customer.details
                                };

            return CreatedAtRoute("GetCustomer", new { id = customer.customer_id}, dto);
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            if (customer == null || customer.customer_id != id)
            {
                return BadRequest();
            }

            _context.Customer.Update(customer);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Customer customer = _context.Customer.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
