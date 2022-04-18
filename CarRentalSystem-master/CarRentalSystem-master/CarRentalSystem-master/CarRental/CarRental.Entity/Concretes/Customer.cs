using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entity.Concretes
{
    public class Customer
    {
      
        public int CustomerID { get; set; }


        [Required(ErrorMessage = "You must enter a name.")]
        [StringLength(50, MinimumLength = 3)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "You must enter a surname.")]
        [StringLength(50, MinimumLength = 3)]
        public string CustomerSurname { get; set; }

        [Required(ErrorMessage = "You must enter a phone number.")]
        [StringLength(50, MinimumLength = 3)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "You must enter an email.")]
        [StringLength(50, MinimumLength = 13)]
        public string Email { get; set; }

        public List<RentInfo> RentInfos { get; set; }

        public Customer()
        {
            RentInfos = new List<RentInfo>();
        }
    }
}
