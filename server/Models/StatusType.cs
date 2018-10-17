using System;
using System.ComponentModel.DataAnnotations;

namespace CRM
{
    public class StatusType
    {
        [Key]
        public int status_id {get; set;}
        public string status {get; set;}
        public StatusType()
        {

        }
    }
}