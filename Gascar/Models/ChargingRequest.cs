using System;

namespace Gascar.Models
{
    public class ChargingRequest
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int BatteryCapacityKw { get; set; }
        public int TargetPercentage { get; set; }

        public DateTime RequestTime { get; set; }
        public bool Completed { get; set; }
    }
}

