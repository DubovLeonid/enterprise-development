using Microsoft.EntityFrameworkCore;
using Salon.Domain.Models;
using System.Diagnostics.Metrics;

namespace Salon.Infrastructure.EntityFrameworkCore.Context;

public class SalonDbContext(DbContextOptions<SalonDbContext> options) : DbContext(options)
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Master> Masters { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Client)
            .WithMany()
            .HasForeignKey(a => a.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Master)
            .WithMany()
            .HasForeignKey(a => a.MasterId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Service)
            .WithMany()
            .HasForeignKey(a => a.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}