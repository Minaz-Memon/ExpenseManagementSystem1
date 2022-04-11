using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExpenseManagementSystem1.Models
{
    public partial class ExpenseMSystemContext : IdentityDbContext<User>
    {
        public ExpenseMSystemContext()
        {
        }

        public ExpenseMSystemContext(DbContextOptions<ExpenseMSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Transcation> Transcations { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-IBS4038;Database=ExpenseMSystem;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Transcation>(entity =>
            {
                entity.ToTable("Transcation");

                entity.Property(e => e.TranscationId).HasColumnName("Transcation_Id");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Payee)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Payer)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.FirstName).HasColumnName("FirstName");

                entity.Property(e => e.LastName).HasColumnName("LastName");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
