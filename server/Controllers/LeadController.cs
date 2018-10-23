using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CRM
{
    [Route("api/leads")]
    [ApiController]
    public class LeadController : ControllerBase
    {
        private CRMContext _context;

        public LeadController(CRMContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IQueryable<LeadDTO> Get()
        {
            IQueryable<LeadDTO> leads = from l in _context.Lead
                select new LeadDTO()
                {
                    Id = l.lead_id,
                    LastContact = l.last_contact,
                    Status = l.status,
                    Priority = l.priority,
                    Customer =  new CustomerDTO()
                                {
                                    Id = l.customer.customer_id,
                                    Name = l.customer.name,
                                    Email = l.customer.email,
                                    Phone = l.customer.phone,
                                    Age = l.customer.age,
                                    Details = l.customer.details
                                },
                    Employee = new EmployeeDTO()
                                {
                                    Id = l.employee.employee_id,
                                    Name = l.employee.name,
                                    Email = l.employee.email,
                                    Phone = l.employee.phone
                                }
                };

            return leads;
        }

        [HttpGet("{id}", Name = "GetLead")]
        public async Task<IActionResult> GetById(int? id)
        {  
            
            if (id == null)
            {
                return NotFound();
            }

            LeadDTO lead = await _context.Lead
                                .Include(l => l.status)
                                .Include(l => l.priority)
                                .Include(l => l.customer)
                                .Include(l => l.customer.details) // Details not being transferred with the DTO
                                .Include(l => l.employee)
                                .Select(l =>
                                    new LeadDTO()
                                    {
                                        Id = l.lead_id,
                                        LastContact = l.last_contact,
                                        Status = l.status,
                                        Priority = l.priority,
                                        Customer =  new CustomerDTO()
                                        {
                                            Id = l.customer.customer_id,
                                            Name = l.customer.name,
                                            Email = l.customer.email,
                                            Phone = l.customer.phone,
                                            Age = l.customer.age,
                                            Details = l.customer.details
                                        },
                                        Employee = new EmployeeDTO()
                                        {
                                            Id = l.employee.employee_id,
                                            Name = l.employee.name,
                                            Email = l.employee.email,
                                            Phone = l.employee.phone
                                        }
                                    }
                                )
                                .SingleOrDefaultAsync(l => l.Id == id);

            if (lead == null)
            {
                return NotFound();
            }

            return Ok(lead);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Lead lead)
        {

            if (lead == null)
            {
                return BadRequest();
            }

            _context.Lead.Add(lead);
            await _context.SaveChangesAsync();

            _context.Entry(lead).Reference(l => l.status).Load();
            _context.Entry(lead).Reference(l => l.priority).Load();
            _context.Entry(lead).Reference(l => l.customer).Load();
            // _context.Entry(lead).Reference(l => l.customer.details).Load(); <- figure this out, not working throws a 500
             _context.Entry(lead).Reference(l => l.employee).Load();

            LeadDTO leadDto = new LeadDTO()
            {
                Id = lead.lead_id,
                LastContact = lead.last_contact,
                Status = lead.status,
                Priority = lead.priority,
                Customer =  new CustomerDTO()
                            {
                                Id = lead.customer.customer_id,
                                Name = lead.customer.name,
                                Email = lead.customer.email,
                                Phone = lead.customer.phone,
                                Age = lead.customer.age,
                                Details = lead.customer.details
                            },
                Employee = new EmployeeDTO()
                            {
                                Id = lead.employee.employee_id,
                                Name = lead.employee.name,
                                Email = lead.employee.email,
                                Phone = lead.employee.phone
                            }
            };

            return CreatedAtRoute("GetLead", new {id = lead.lead_id}, leadDto);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Lead lead)
        {
            if (lead == null || lead.lead_id != id)
            {
                return BadRequest();
            }

            _context.Lead.Update(lead);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Lead lead = _context.Lead.Find(id);
            if (lead == null)
            {
                return NotFound();
            }

            _context.Lead.Remove(lead);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
