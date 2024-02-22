using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Trabea.DataAccess.Models
{
    public partial class TrabeaContext : DbContext
    {
        public TrabeaContext()
        {
        }

        public TrabeaContext(DbContextOptions<TrabeaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<PartTimeEmployee> PartTimeEmployees { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<Shift> Shifts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-MN9N8N52;Initial Catalog=Trabea;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK__Accounts__A9D1053508A93468");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.AccountEmails)
                    .UsingEntity<Dictionary<string, object>>(
                        "AccountRole",
                        l => l.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__AccountRo__RoleI__29572725"),
                        r => r.HasOne<Account>().WithMany().HasForeignKey("AccountEmail").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__AccountRo__Accou__286302EC"),
                        j =>
                        {
                            j.HasKey("AccountEmail", "RoleId").HasName("PK__AccountR__44D8A1D3217D4879");

                            j.ToTable("AccountRoles");

                            j.IndexerProperty<string>("AccountEmail").HasMaxLength(100).IsUnicode(false);
                        });
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.OfficeEmail, "UQ__Employee__FCEC3C725A63AF81")
                    .IsUnique();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OfficeEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.OfficeEmailNavigation)
                    .WithOne(p => p.Employee)
                    .HasForeignKey<Employee>(d => d.OfficeEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employees__Offic__30F848ED");
            });

            modelBuilder.Entity<PartTimeEmployee>(entity =>
            {
                entity.HasIndex(e => e.PersonalEmail, "UQ__PartTime__7B5B59A515059494")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.CurrentEducation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastEducation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OfficeEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PersonalEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ResignDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.OfficeEmailNavigation)
                    .WithMany(p => p.PartTimeEmployees)
                    .HasForeignKey(d => d.OfficeEmail)
                    .HasConstraintName("FK__PartTimeE__Offic__2D27B809");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ScheduleDate).HasColumnType("datetime");

                entity.HasOne(d => d.ApprovedByNavigation)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.ApprovedBy)
                    .HasConstraintName("FK__Schedules__Appro__37A5467C");

                entity.HasOne(d => d.Partime)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.PartimeId)
                    .HasConstraintName("FK__Schedules__Parti__35BCFE0A");

                entity.HasOne(d => d.Shift)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.ShiftId)
                    .HasConstraintName("FK__Schedules__Shift__36B12243");
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
