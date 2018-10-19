using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM
{
    public class CustomerDTO
    {
        [Key]
        public int Id {get; set;}
        public string Name {get; set;}
        public string Email {get; set;}
        public string Phone {get; set;}
        public int Age {get; set; }
        public Details Details {get; set;}

        public CustomerDTO()
        {

        }
    }
}