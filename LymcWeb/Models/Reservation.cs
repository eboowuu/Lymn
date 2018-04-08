using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LymcWeb.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public string UserName { get; set; }

        public Boat ReservedBoat { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Reservation()
        {
        }

    }
}
