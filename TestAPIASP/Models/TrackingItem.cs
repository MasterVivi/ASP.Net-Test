using System;

namespace TestAPIASP.Models
{
    public class TrackingItem
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public bool IsComplete { get; set; }
    }
}