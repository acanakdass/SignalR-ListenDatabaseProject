using System;
using System.Collections.Generic;

namespace SignalRLiveDataProject.Entities
{
	public class Employee
	{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Sale> Sales{ get; set; }
    }
}

