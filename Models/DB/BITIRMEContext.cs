using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PET_GIDA.Models.DB
{
    public partial class BITIRMEContext : DbContext
    {
        public BITIRMEContext()
        {
        }

        public BITIRMEContext(DbContextOptions<BITIRMEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Kart> Karts { get; set; }
        public virtual DbSet<Kategori> Kategoris { get; set; }
        public virtual DbSet<Kullanici> Kullanicis { get; set; }
        public virtual DbSet<SiparisListesi> SiparisListesis { get; set; }
        public virtual DbSet<Urunler> Urunlers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-7GA78PI\\SQLEXPRESS;Database=BITIRME;User Id=sa;Password=Password1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.Ad).HasMaxLength(50);

                entity.Property(e => e.KullaniciAdi)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Sifre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Soyad).HasMaxLength(50);
            });

            modelBuilder.Entity<Kart>(entity =>
            {
                entity.ToTable("Kart");

                entity.Property(e => e.KartSahibi).HasMaxLength(50);
            });

            modelBuilder.Entity<Kategori>(entity =>
            {
                entity.ToTable("Kategori");

                entity.Property(e => e.KategoriAdi).HasMaxLength(50);
            });

            modelBuilder.Entity<Kullanici>(entity =>
            {
                entity.ToTable("Kullanici");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(157)
                    .HasColumnName("email");

                entity.Property(e => e.KullaniciAdi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("kullaniciAdi");

                entity.Property(e => e.Sifre)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("sifre");
            });

            modelBuilder.Entity<SiparisListesi>(entity =>
            {
                entity.ToTable("SiparisListesi");

                entity.Property(e => e.Mail).HasMaxLength(100);

                entity.Property(e => e.SiparisDurumu).HasMaxLength(50);

                entity.Property(e => e.SiparisTarihi).HasMaxLength(50);

                entity.HasOne(d => d.Urun)
                    .WithMany(p => p.SiparisListesis)
                    .HasForeignKey(d => d.UrunId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Siparis_Urunler");
            });

            modelBuilder.Entity<Urunler>(entity =>
            {
                entity.HasKey(e => e.UrunId);

                entity.ToTable("Urunler");

                entity.Property(e => e.UrunFiyat).HasColumnType("money");

                entity.Property(e => e.UrunResim1).HasColumnType("image");

                entity.Property(e => e.UrunResim2).HasColumnType("image");

                entity.Property(e => e.UrunResim3).HasColumnType("image");

                entity.Property(e => e.UrunResim4).HasColumnType("image");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
