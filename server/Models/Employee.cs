using System;
using System.ComponentModel.DataAnnotations;

namespace CRM
{
    public class Employee
    {
        [Key]
        public int employee_id {get; set;}
        public string name {get; set;}
        public string email {get; set;}
        public string phone {get; set;}
            
        public Employee()
        {

        }
    }
}