using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_2_ASG
{
    class Rental
    {
        public int RentalNo { get; set; }
        public Person Guest { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public Resort Resort { get; set; }
        public bool PaymentStatus { get; set; }

        public Rental(int rentalNo, Person guest, DateTime arrival, DateTime departure, Resort resort)
        {
            RentalNo = rentalNo;
            Guest = guest;
            ArrivalDate = arrival;
            DepartureDate = departure;
            Resort = resort;
        }

        public double ComputeRentalCost() 
        {
            int days = DepartureDate.Subtract(ArrivalDate).Days;// member 10% discount 
            double cost = Resort.ComputeResortCost(days); //call compute resort cost 
            if (Guest.Member== true)
            {
                return cost * 0.9;
            }
            else
            {
                return cost;
            }
        }

        public override string ToString()
        {
            return "Rental Number: " + RentalNo+"\tGuest: "+Guest+"\tArrival Date: "+ArrivalDate
                + "\tDeparture Date: " + DepartureDate + "\tResort" + Resort;
        }
    }
}