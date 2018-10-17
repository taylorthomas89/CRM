using System;
using System.ComponentModel.DataAnnotations;

namespace CRM
{
    public class PriorityType
    {
        [Key]        
        public int priority_id {get; set;}
        public string priority {get; set;}
        public PriorityType()
        {

        }
    }
}