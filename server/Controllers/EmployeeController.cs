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
        public IQueryable<EmployeeDTO> GetAll()
        {
            IQueryable<EmployeeDTO> employees = from e in _context.Employee
                select new EmployeeDTO()
                {
                    Id = e.employee_id,
                    Name = e.name,
                    Email = e.email,
                    Phone = e.phone
                };

                return employees;
        }

        [HttpGet("{id}", Name = "GetEmployee")]
        public IActionResult GetById(int? id)
        {  
            if (id == null)
            {
                return NotFound();
            }

            EmployeeDTO employee =  _context.Employee
                                        .Select(e =>
                                            new EmployeeDTO()
                                            {
                                                Id = e.employee_id,
                                                Name = e.name,
                                                Email = e.email,
                                                Phone = e.phone
                                            }    
                                        )
                                        .SingleOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }

            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            EmployeeDTO employeeDto = new EmployeeDTO()
                                {
                                    Id = employee.employee_id,
                                    Name = employee.name,
                                    Email = employee.email,
                                    Phone = employee.phone
                                };
            
            return CreatedAtRoute("GetEmployee", new { id = employee.employee_id}, employeeDto);
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
        }
    }
}
