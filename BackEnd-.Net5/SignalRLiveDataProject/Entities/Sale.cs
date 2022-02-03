using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SignalRLiveDataProject.Entities
{
	public class Sale
	{
        public int Id { get; set; }

        //[JsonIgnore]
        //public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public int Price { get; set; }
    }
}
 
