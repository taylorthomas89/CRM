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
        public IActionResult Get()
        {
            if (_context.Customer.ToList().Count == 0)
            {
                return Ok("No customers found.");
            }
            else 
            {
                return Ok(_context.Customer.Include(c => c.details).ToList());
            }
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> Get(int? id)
        {   

            if (id == null)
            {
                return NotFound();
            }

            Customer customer = await _context.Customer
                                    .Include(c => c.details)
                                    .SingleOrDefaultAsync(c => c.customer_id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);

        }

        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            _context.Customer.Add(customer);
            _context.SaveChanges();
            // return Ok(_context.Customer.Include(c => c.details).ToList());
            return CreatedAtRoute("GetCustomer", new {id = customer.customer_id }, customer);
        }

        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            if (customer == null || customer.customer_id != id)
            {
                return BadRequest();
            }

            _context.Customer.Update(customer);
            _context.SaveChanges();
            return NoContent();
            // return Ok(customer);
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
            // return Ok(_context.Customer.Include(c => c.details).ToList());
        }
    }
}
