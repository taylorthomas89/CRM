using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM
{
    public class Interaction
    {
        [Key]
        public int interaction_id {get; set;}
        public string comment {get; set;}
        public DateTime datetime {get; set;}
        public int duration {get; set;}

        [ForeignKey("LeadForeignKey")]
        public int lead_id {get; set;}
        public Lead lead {get; set;}

        [ForeignKey("EmployeeForeinKey")]
        public int employee_id {get; set;}
        public Employee employee {get; set;}

        public Interaction()
        {

        }
    }
}