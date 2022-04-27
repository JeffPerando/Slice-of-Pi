using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace Main.Models
{
    public partial class CrimeDbContext : DbContext
    {
        public CrimeDbContext()
        {
        }

        public CrimeDbContext(DbContextOptions<CrimeDbContext> options)
            : base(options)
        {
        }

        //public virtual DbSet<AgencyInformation> AgencyInformations { get; set; } = null!;
        //public virtual DbSet<Crime> Crimes { get; set; } = null!;
        public virtual DbSet<Home> Homes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<StateCrimeSearchResult> StateCrimeSearchResults { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ApplicationDbContextConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<AgencyInformation>(entity =>
            {
                entity.ToTable("AgencyInformation");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgencyCounty).HasMaxLength(100);

                entity.Property(e => e.AgencyName).HasMaxLength(100);

                entity.Property(e => e.Ori)
                    .HasMaxLength(25)
                    .HasColumnName("ORI");
            });

            modelBuilder.Entity<Crime>(entity =>
            {
                entity.ToTable("Crime");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AgencyId).HasColumnName("AgencyID");

                entity.Property(e => e.OffenseType).HasMaxLength(100);

                entity.HasOne(d => d.Agency)
                    .WithMany(p => p.Crimes)
                    .HasForeignKey(d => d.AgencyId)
                    .HasConstraintName("Home_Fk_Crime");
            });
            */
            modelBuilder.Entity<Home>(entity =>
            {
                entity.ToTable("Home");

                entity.Property(e => e.Id).HasColumnName("ID");

                //entity.Property(e => e.AgencyId).HasColumnName("AgencyID");

                entity.Property(e => e.County).HasMaxLength(100);

                entity.Property(e => e.State).HasMaxLength(2);

                entity.Property(e => e.StreetAddress).HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ZipCode).HasMaxLength(10);
                /*
                entity.HasOne(d => d.Agency)
                    .WithMany(p => p.Homes)
                    .HasForeignKey(d => d.AgencyId)
                    .HasConstraintName("Home_Fk_AgencyInformation");
                */
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Homes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Home_Fk_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<StateCrimeSearchResult>(entity =>
            {
                entity.ToTable("StateCrimeSearchResult");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateSearched).HasColumnType("datetime");

                entity.Property(e => e.State).HasMaxLength(100);

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.StateCrimeSearchResults)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SCSR_Fk_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
