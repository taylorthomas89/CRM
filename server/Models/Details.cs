using System;
using System.ComponentModel.DataAnnotations;

namespace CRM
{
    public class Details 
    {
        [Key]
        public int details_id {get; set;}
        public string preferred_contact {get; set;}
        public Details()
        {

        }
    }
}