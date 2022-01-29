using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi2.Models
{
    public class Employee
    {
        public string Department { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string DateOfJoining { get; set; }
        public string Photo { get; set; }

    }
}
