using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RuilWinkelVaals.Models
{
    public partial class DB_DevOpsContext : DbContext
    {
        public DB_DevOpsContext()
        {
        }

        public DB_DevOpsContext(DbContextOptions<DB_DevOpsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountDatum> AccountData { get; set; }
        public virtual DbSet<AccountTypeLt> AccountTypeLts { get; set; }
        public virtual DbSet<LedenpasLt> LedenpasLts { get; set; }
        public virtual DbSet<ProfileDatum> ProfileData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DamelotSVR.Damelot.com;Database=DB_DevOps;User Id=sa;Password=Acce$$dbg01!;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<AccountDatum>(entity =>
            {
                entity.Property(e => e.DateBlocked).HasColumnType("date");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.AccountData)
                    .HasForeignKey(d => d.ProfileId)
                    .HasConstraintName("FK_AccountData_ProfileData");
            });

            modelBuilder.Entity<AccountTypeLt>(entity =>
            {
                entity.ToTable("AccountType_LT");

                entity.Property(e => e.AccountType).HasMaxLength(50);
            });

            modelBuilder.Entity<LedenpasLt>(entity =>
            {
                entity.ToTable("Ledenpas_LT");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<ProfileDatum>(entity =>
            {
                entity.Property(e => e.Achternaam).HasMaxLength(50);

                entity.Property(e => e.Balans).HasDefaultValueSql("((0))");

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Geboortedatum).HasColumnType("date");

                entity.Property(e => e.Huisnummer).HasMaxLength(10);

                entity.Property(e => e.Postcode).HasMaxLength(10);

                entity.Property(e => e.Straat).HasMaxLength(50);

                entity.Property(e => e.Voornaam).HasMaxLength(50);

                entity.Property(e => e.Woonplaats).HasMaxLength(50);

                entity.HasOne(d => d.AccountTypeNavigation)
                    .WithMany(p => p.ProfileData)
                    .HasForeignKey(d => d.AccountType)
                    .HasConstraintName("FK_ProfileData_AccountType_LT");

                entity.HasOne(d => d.LedenpasNavigation)
                    .WithMany(p => p.ProfileData)
                    .HasForeignKey(d => d.Ledenpas)
                    .HasConstraintName("FK_ProfileData_Ledenpas_LT");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
