using Microsoft.EntityFrameworkCore;
using MiniBron.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBron.EntityFramework.Repository
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<AdditionalService> AdditionalServices { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<ServicesForBooking> ServicesForBookings { get; set; }
        public DbSet<ServicesForSession> ServicesForSessions { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public ApplicationContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-0USVL0V\SQLEXPRESS;
                                        Initial Catalog=minibron_db;Integrated Security=True;
                                        Connect Timeout=30;Encrypt=False;
                                        TrustServerCertificate=False;ApplicationIntent=ReadWrite;
                                        MultiSubnetFailover=False");
        }
    }
}
