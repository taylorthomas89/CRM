using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CRM
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private CRMContext _context;

        public EmployeeController(CRMContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Employee> GetAll()
        {
            return _context.Employee.ToList();
        }

        [HttpGet("{id}", Name = "GetEmployee")]
        public IActionResult GetById(int? id)
        {  
            
            Employee employee = _context.Employee.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }

            _context.Employee.Add(employee);
            _context.SaveChanges();
            
            // return Ok(_context.Employee.ToList());
            return CreatedAtRoute("GetEmployee", new { id = employee.employee_id}, employee);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Employee employee)
        {
            if (employee == null || employee.employee_id != id)
            {
                return BadRequest();
            }
            

            _context.Employee.Update(employee);
            _context.SaveChanges();
            return NoContent();
            // return Ok(employee);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Employee employee = _context.Employee.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            _context.SaveChanges();
            return NoContent();
            // return Ok(_context.Employee.ToList());
        }
    }
}
