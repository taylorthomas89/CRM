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
                    Customer = l.customer,
                    Employee = l.employee
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
                                .Include(l => l.customer.details)
                                .Include(l => l.employee)
                                .Select(l =>
                                    new LeadDTO()
                                    {
                                        Id = l.lead_id,
                                        LastContact = l.last_contact,
                                        Status = l.status,
                                        Priority = l.priority,
                                        Customer = l.customer,
                                        Employee = l.employee

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
            _context.Entry(lead).Reference(l => l.employee).Load();

            LeadDTO leadDto = new LeadDTO()
            {
                Id = lead.lead_id,
                LastContact = lead.last_contact,
                Status = lead.status,
                Priority = lead.priority,
                Customer = lead.customer,
                Employee = lead.employee
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
