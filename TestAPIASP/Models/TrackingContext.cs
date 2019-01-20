using System;
using Microsoft.EntityFrameworkCore;

namespace TestAPIASP.Models
{
    public class TrackingContext : DbContext
    {
        public TrackingContext(DbContextOptions<TrackingContext> options)
            : base(options)
        {
        }

        public DbSet<TrackingItem> TrackedItems { get; set; }
    }
}
