using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GameOfLifeClean.Models
{
    public partial class A2Context : DbContext
    {
        public A2Context()
        {
        }

        public A2Context(DbContextOptions<A2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Block> Block { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=aa1t9a5g52hpr22.cmkglcti19rm.us-west-2.rds.amazonaws.com;database=A2;user=root;pwd=root1234;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Block>(entity =>
            {
                entity.ToTable("block");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IsAlive)
                    .HasColumnName("is_alive")
                    .HasColumnType("tinyint(1)");

                entity.Property(e => e.X)
                    .HasColumnName("x")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.Y)
                    .HasColumnName("y")
                    .HasColumnType("smallint(6)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Color)
                    .HasColumnName("color")
                    .HasMaxLength(6);

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(255);
            });
        }
    }
}
