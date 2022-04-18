using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entity.Concretes
{
    public class Employee
    {
        

        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "You must enter an employee name.")]
        [StringLength(50, MinimumLength = 3)]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "You must enter an employee surname.")]
        [StringLength(50, MinimumLength = 3)]
        public string EmployeeSurname { get; set; }

        [Required(ErrorMessage = "You must enter an email.")]
        [StringLength(50, MinimumLength = 13)]
        public string Email { get; set; }


        public int CompanyID { get; set; }
        public Company Company { get; }
    }
}
