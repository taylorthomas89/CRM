using System;
using System.ComponentModel.DataAnnotations;

namespace CRM
{
    public class EmployeeDTO
    {
        [Key]
        public int Id {get; set;}
        public string Name {get; set;}
        public string Email {get; set;}
        public string Phone {get; set;}
            
        public EmployeeDTO()
        {

        }
    }
}