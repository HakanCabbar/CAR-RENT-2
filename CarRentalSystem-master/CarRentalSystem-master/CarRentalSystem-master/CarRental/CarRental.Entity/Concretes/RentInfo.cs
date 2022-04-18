using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entity.Concretes
{
    public class RentInfo
    {
        public int CarID { get; set; }

        public string CarName { get; set; }

        public string CarModel { get; set; }

        public int CarTotalKM { get; set; }

        public int CarTotalSeat { get; set; }

        public int RentFeeDaily { get; set; }

        public int CompanyID { get; set; }

    }
}
