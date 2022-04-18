using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entity.Concretes
{
    public class Company
    {
        public int CompanyID { get; set; }

        [Required(ErrorMessage = "You must enter a company name.")]
        [StringLength(50, MinimumLength = 3)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "You must enter a city name.")]
        [StringLength(50, MinimumLength = 3)]
        public string City { get; set; }

        [Required(ErrorMessage = "You must enter an adress.")]
        [StringLength(50, MinimumLength = 5)]
        public string Address { get; set; }
        
        public int TotalCars { get; set; }

        public List<Car> Cars { get; set; }
        public List<Employee> Employees { get; set; }
    }
}

