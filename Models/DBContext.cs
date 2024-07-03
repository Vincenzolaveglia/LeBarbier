using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace LeBarbier.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<OfferedService> OfferedServices { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ReservationService> ReservationServices { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OfferedService>()
                .Property(e => e.ServiceName)
                .IsUnicode(false);

            modelBuilder.Entity<OfferedService>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<OfferedService>()
                .HasMany(e => e.ReservationServices)
                .WithOptional(e => e.OfferedServices)
                .HasForeignKey(e => e.Id_OfferedServices);

            modelBuilder.Entity<Reservation>()
                .HasMany(e => e.ReservationServices)
                .WithOptional(e => e.Reservations)
                .HasForeignKey(e => e.Id_Reservation);

            modelBuilder.Entity<Role>()
                .Property(e => e.TypeRole)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Roles)
                .HasForeignKey(e => e.Id_Role);

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PhoneNo)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Reservations)
                .WithOptional(e => e.Users)
                .HasForeignKey(e => e.Id_User);
        }
    }
}
