using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM
{
    public class LeadDTO
    {
        [Key]
        public int Id {get; set;}
        public DateTime LastContact {get; set;}
        public StatusType Status {get; set;}
        public PriorityType Priority {get; set;}
        public Customer Customer {get; set;}
        public Employee Employee {get; set;}

        public LeadDTO()
        {

        }
    }
}