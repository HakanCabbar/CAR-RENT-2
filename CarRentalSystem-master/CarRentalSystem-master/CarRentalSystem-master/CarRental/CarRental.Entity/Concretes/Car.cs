using System.ComponentModel.DataAnnotations;

namespace CarRental.Entity.Concretes
{
    
    public class Car
    {
        public int CarID { get; set; }

        [Required(ErrorMessage = "You must enter a car name.")]
        public string CarName { get; set; }

        [Required(ErrorMessage = "You must enter a car model.")]
        public int CarModel { get; set; }

        [Required(ErrorMessage = "You must enter a rent fee.")]
        public int RentFeeDaily { get; set; }

        [Required(ErrorMessage = "You must enter a total car km.")]
        public int CarTotalKM{ get; set; }

        [Required(ErrorMessage = "You must enter seats.")]
        public int CarTotalSeats { get; set; }
       
        public bool isRent { get; set; }

        public int CompanyID { get; set; }
        public Company Company { get; set; }
    }
}
