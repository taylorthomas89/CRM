using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM
{
    public class Customer
    {
        [Key]
        public int customer_id {get; set;}
        public string name {get; set;}
        public string email {get; set;}
        public string phone {get; set;}
        public int age {get; set; }

        [ForeignKey("detailsForeignKey")]
        public int details_id {get; set;}
        public Details details {get; set;}

        public Customer()
        {

        }
    }
}