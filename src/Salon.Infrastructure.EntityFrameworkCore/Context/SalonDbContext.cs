using Microsoft.EntityFrameworkCore;
using Salon.Domain.Models;

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

        
        modelBuilder.Entity<Client>(entity =>
        {
            entity.Property(c => c.FullName).HasMaxLength(150);
            entity.Property(c => c.Phone).HasMaxLength(20);
        });

        
        modelBuilder.Entity<Master>(entity =>
        {
            entity.Property(m => m.FullName).HasMaxLength(150);
            entity.Property(m => m.PassportNumber).HasMaxLength(10);
            entity.Property(m => m.Specialization).HasMaxLength(100);
            entity.HasIndex(m => m.PassportNumber).IsUnique(); 
        });

        
        modelBuilder.Entity<Service>(entity =>
        {
            entity.Property(s => s.Name).HasMaxLength(100);
            entity.Property(s => s.Price).HasPrecision(10, 2); 
        });

        
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