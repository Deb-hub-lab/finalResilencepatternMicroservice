using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Npgsql.EntityFrameworkCore.PostgreSQL;


namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<Student> Students { get; set; } = default!;
        public DbSet<Attendance> Attendances { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Students
            modelBuilder.Entity<Student>(b =>
            {
                b.ToTable("students");
                b.HasKey(s => s.Id);
                b.Property(s => s.Name).IsRequired();

                b.HasMany<Attendance>("_attendances")
                 .WithOne()
                 .HasForeignKey("StudentId")
                 .IsRequired();

                b.Ignore(s => s.Attendances);

                // Seed Students
                b.HasData(
                    new Student(Guid.Parse("11111111-1111-1111-1111-111111111111"), "Alice"),
                    new Student(Guid.Parse("22222222-2222-2222-2222-222222222222"), "Bob"),
                    new Student(Guid.Parse("33333333-3333-3333-3333-333333333333"), "Charlie")
                );
            });

            // Attendances
            modelBuilder.Entity<Attendance>(b =>
            {
                b.ToTable("attendances");
                b.HasKey(a => a.Id);
                b.Property(a => a.OccurredAt).HasColumnType("date");
                b.Property(a => a.IsPresent).IsRequired();

                // Seed Attendances
                b.HasData(
                    new Attendance(
                        Guid.Parse("aaaaaaa1-1111-1111-1111-111111111111"),
                        Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        new DateTime(2025, 11, 1),
                        true
                    ),
                    new Attendance(
                        Guid.Parse("aaaaaaa2-1111-1111-1111-111111111111"),
                        Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        new DateTime(2025, 11, 2),
                        false
                    ),
                    new Attendance(
                        Guid.Parse("aaaaaaa3-1111-1111-1111-111111111111"),
                        Guid.Parse("22222222-2222-2222-2222-222222222222"),
                        new DateTime(2025, 11, 1),
                        true
                    ),
                    new Attendance(
                        Guid.Parse("aaaaaaa4-1111-1111-1111-111111111111"),
                        Guid.Parse("33333333-3333-3333-3333-333333333333"),
                        new DateTime(2025, 11, 1),
                        true
                    )
                );
            });
        }
    }
}
