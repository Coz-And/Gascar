using System;

namespace Gascar.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal ParkingCost { get; set; }
        public decimal ChargingCost { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
