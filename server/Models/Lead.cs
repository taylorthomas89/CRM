using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM
{
    public class Lead
    {
        [Key]
        public int lead_id {get; set;}
        public DateTime last_contact {get; set;}

        [ForeignKey("StatusForeignKey")]
        public int status_id {get; set;}
        public StatusType status {get; set;}

        [ForeignKey("PriorityForeignKey")]
        public int priority_id {get; set;}
        public PriorityType priority {get; set;}

        [ForeignKey("CustomerForeignKey")]
        public int customer_id {get; set;}
        public Customer customer {get; set;}

        [ForeignKey("EmployeeForeignKey")]
        public int employee_id {get; set;}
        public Employee employee {get; set;}

        public Lead()
        {

        }
    }
}