using Hubtel.Wallets.Domain.Models;
using Hubtel.Wallets.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Hubtel.Wallets.Persistence.Context
{
    public class HubtelWalletsDbContext : DbContext
    {
        public virtual DbSet<TblAsAccountScheme> TblAsAccountSchemes { get; set; }
        public virtual DbSet<TblAsAccountSchemeGet> TblAsAccountSchemeGet { get; set; }
        public virtual DbSet<TblTtype> TblTtypes { get; set; }
        public virtual DbSet<TblTtypeGet> TblTtypeGet { get; set; }
        public virtual DbSet<TblUcaUserCreditAccount> TblUcaUserCreditAccounts { get; set; }
        public virtual DbSet<VwTnsTypeAndScheme> VwTnsTypeAndSchemes { get; set; }
        public virtual DbSet<VwUcaUserCreditAccount> VwUcaUserCreditAccounts { get; set; }
        public virtual DbSet<VwCreditAccountsForUser> VwCreditAccountsForUser { get; set; }
        public virtual DbSet<VwUserRolesAndClaim> VwUserRolesAndClaim { get; set; }
        //public virtual DbSet<object> Objects { get; set; }

        public HubtelWalletsDbContext(DbContextOptions<HubtelWalletsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAsAccountScheme>(entity =>
            {
                entity.HasKey(e => e.AsIdpk);

                entity.ToTable("tblAsAccountScheme");

                entity.Property(e => e.AsIdpk).HasColumnName("asIDpk");

                entity.Property(e => e.AsSchemeName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("asSchemeName");

                entity.Property(e => e.AsTypeIdfk).HasColumnName("asTypeIDfk");

                entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblTtype>(entity =>
            {
                entity.HasKey(e => e.TIdpk);

                entity.ToTable("tblTType");

                entity.Property(e => e.TIdpk).HasColumnName("tIDpk");

                entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TTypeName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("tTypeName");
            });

            modelBuilder.Entity<TblUcaUserCreditAccount>(entity =>
            {
                entity.HasKey(e => e.UcaIdpk);

                entity.ToTable("tblUcaUserCreditAccounts");

                entity.Property(e => e.UcaIdpk).HasColumnName("ucaIDpk");

                entity.Property(e => e.UcaAccountNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ucaAccountNumber");

                entity.Property(e => e.UcaCreationDate)
                    .HasColumnName("ucaCreationDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UcaEditedDate).HasColumnName("ucaEditedDate");

                entity.Property(e => e.UcaSchemeIdfk).HasColumnName("ucaSchemeIDfk");

                entity.Property(e => e.UcaTypeIdfk).HasColumnName("ucaTypeIDfk");

                entity.Property(e => e.UcaUserIdfk)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ucaUserIDfk");
            });

            modelBuilder.Entity<TblAsAccountSchemeGet>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("TblAsAccountSchemeGet");
            });
            modelBuilder.Entity<TblTtypeGet>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("TblTtypeGet");
            });

            modelBuilder.Entity<VwTnsTypeAndScheme>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("vwTnsTypeAndSchemes");
            });
            modelBuilder.Entity<VwTnsTypeAndSchemeGet>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwTnsTypeAndSchemeGet");
            });
            modelBuilder.Entity<VwUserRolesAndClaim>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwUserRolesAndClaim");
            });

            modelBuilder.Entity<VwUcaUserCreditAccount>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("vwUcaUserCreditAccounts");
            });
            modelBuilder.Entity<VwCreditAccountsForUser>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwCreditAccountsForUser");
            });

            modelBuilder.ApplyConfiguration(new TblTtypeConfiguration());
            modelBuilder.ApplyConfiguration(new TblAsAccountSchemeConfiguration());
            modelBuilder.ApplyConfiguration(new TblUcaUserCreditAccountConfiguration());

            //modelBuilder.Entity<object>(entity => entity.HasNoKey());
        }
    }
}