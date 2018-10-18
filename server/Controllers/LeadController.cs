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
        public List<Lead> Get()
        {
            return _context.Lead
                    .Include(l => l.status)
                    .Include(l => l.priority)
                    .Include(l => l.customer)
                    .Include(l => l.customer.details)
                    .Include(l => l.employee)
                    .ToList();
        }

        [HttpGet("{id}", Name = "GetLead")]
        public async Task<IActionResult> GetById(int? id)
        {  
            
            if (id == null)
            {
                return NotFound();
            }

            Lead lead = await _context.Lead
                                .Include(l => l.status)
                                .Include(l => l.priority)
                                .Include(l => l.customer)
                                .Include(l => l.customer.details)
                                .Include(l => l.employee)
                                .FirstOrDefaultAsync(l => l.lead_id == id);
            
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
            _context.SaveChanges();

            // Grab the newly created lead such that can return below in "CreatedAtRoute"
            Lead newLead = await _context.Lead
                                .Include(l => l.status)
                                .Include(l => l.priority)
                                .Include(l => l.customer)
                                .Include(l => l.customer.details)
                                .Include(l => l.employee)
                                .FirstOrDefaultAsync(l => l.lead_id == lead.lead_id);

            return CreatedAtRoute("GetLead", new {id = lead.lead_id}, newLead);
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
