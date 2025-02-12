using Microsoft.EntityFrameworkCore;
using PetShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Data.Context
{
    public class PetShopContext : DbContext
    {
        public PetShopContext(DbContextOptions<PetShopContext> options) : base(options) { }

        public DbSet <Users> Users { get; set; }
        public DbSet <Companies> Companies { get; set; }
        public DbSet <Appointments> Appointments { get; set; }
        public DbSet <Services> Services { get; set; }
        public DbSet <ServiceGroup> ServiceGroup { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<string>()
                .AreUnicode(false)
                .HaveMaxLength(100);

            configurationBuilder
                .Properties<float>()
                .HavePrecision(8, 2);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PetShopContext).Assembly);
              

            modelBuilder.Entity<Pets>()
                .HasOne(im => im.User)
                .WithMany(u => u.Pets)
                .HasForeignKey(im => im.UserId);

            modelBuilder.Entity<Appointments>()
                .HasOne(U => U.Users)
                .WithMany()
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<Appointments>()
                .HasOne(U => U.Pets)
                .WithMany()
                .HasForeignKey(u => u.PetId);

            modelBuilder.Entity<ServiceGroup>()
                .HasKey(sg => new {sg.ServiceId, sg.AppointmentId });

            modelBuilder.Entity<ServiceGroup>()
                .HasOne(sg => sg.Services)
                .WithMany(s => s.ServiceGroups)
                .HasForeignKey(sg => sg.ServiceId);

            modelBuilder.Entity<ServiceGroup>()
                .HasOne(sg => sg.Appointments)
                .WithMany(s => s.ServiceGroups)
                .HasForeignKey(sg => sg.AppointmentId);
        }
    }
}