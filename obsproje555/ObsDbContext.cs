namespace proje_obs
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ObsDbContext : DbContext
    {
        public ObsDbContext()
            : base("name=Model1")
        {
        }

        public virtual DbSet<AcilanDersler> AcilanDersler { get; set; }
        public virtual DbSet<Bolum> Bolum { get; set; }
        public virtual DbSet<Dersler> Dersler { get; set; }
        public virtual DbSet<DersSorumlulari> DersSorumlulari { get; set; }
        public virtual DbSet<Donemler> Donemler { get; set; }
        public virtual DbSet<EgitimPlani> EgitimPlani { get; set; }
        public virtual DbSet<Fakulte> Fakulte { get; set; }
        public virtual DbSet<Idari> Idari { get; set; }
        public virtual DbSet<Kayit> Kayit { get; set; }
        public virtual DbSet<Notlar> Notlar { get; set; }
        public virtual DbSet<Ogrenci> Ogrenci { get; set; }
        public virtual DbSet<DersTarihler> DersTarihler { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcilanDersler>()
                .Property(e => e.DersKodu)
                .IsUnicode(false);

            modelBuilder.Entity<AcilanDersler>()
                .Property(e => e.DersAdi)
                .IsUnicode(false);

            modelBuilder.Entity<Bolum>()
                .Property(e => e.BolumAdi)
                .IsUnicode(false);

            modelBuilder.Entity<Bolum>()
                .Property(e => e.BolumKodu)
                .IsUnicode(false);

            modelBuilder.Entity<Bolum>()
                .Property(e => e.Telefon)
                .IsUnicode(false);

            modelBuilder.Entity<Bolum>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Dersler>()
                .Property(e => e.DersKodu)
                .IsUnicode(false);

            modelBuilder.Entity<Dersler>()
                .Property(e => e.DersAdi)
                .IsUnicode(false);

            modelBuilder.Entity<Dersler>()
                .Property(e => e.Tur)
                .IsUnicode(false);

            modelBuilder.Entity<Dersler>()
                .Property(e => e.Donem)
                .IsUnicode(false);

            modelBuilder.Entity<DersSorumlulari>()
                .Property(e => e.Adi)
                .IsUnicode(false);

            modelBuilder.Entity<DersSorumlulari>()
                .Property(e => e.Soyadi)
                .IsUnicode(false);

            modelBuilder.Entity<DersSorumlulari>()
                .Property(e => e.Unvani)
                .IsUnicode(false);

            modelBuilder.Entity<DersSorumlulari>()
                .Property(e => e.Telefonu)
                .IsUnicode(false);

            modelBuilder.Entity<DersSorumlulari>()
                .Property(e => e.Maili)
                .IsUnicode(false);

            modelBuilder.Entity<DersSorumlulari>()
                .Property(e => e.OdaNo)
                .IsUnicode(false);
            

            modelBuilder.Entity<Donemler>()
                .Property(e => e.Donem)
                .IsUnicode(false);

            modelBuilder.Entity<EgitimPlani>()
                .Property(e => e.BolumAdi)
                .IsUnicode(false);

            modelBuilder.Entity<EgitimPlani>()
                .Property(e => e.Onay_tarihi)
                .IsUnicode(false);

            modelBuilder.Entity<EgitimPlani>()
                .Property(e => e.Donem)
                .IsUnicode(false);

            modelBuilder.Entity<Fakulte>()
                .Property(e => e.FakulteAdi)
                .IsUnicode(false);

            modelBuilder.Entity<Fakulte>()
                .Property(e => e.FakulteKodu)
                .IsUnicode(false);

            modelBuilder.Entity<Fakulte>()
                .Property(e => e.Adres)
                .IsUnicode(false);

            modelBuilder.Entity<Fakulte>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Fakulte>()
                .Property(e => e.Telefon)
                .IsUnicode(false);

            modelBuilder.Entity<Idari>()
                .Property(e => e.Unvan)
                .IsUnicode(false);

            modelBuilder.Entity<Idari>()
                .Property(e => e.Adi)
                .IsUnicode(false);

            modelBuilder.Entity<Idari>()
                .Property(e => e.Soyadi)
                .IsUnicode(false);

            modelBuilder.Entity<Idari>()
                .Property(e => e.Mail)
                .IsUnicode(false);

            modelBuilder.Entity<Idari>()
                .Property(e => e.Tel)
                .IsUnicode(false);

            modelBuilder.Entity<Idari>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Idari>()
                .Property(e => e.Adres)
                .IsUnicode(false);

            modelBuilder.Entity<Notlar>()
                .Property(e => e.HarfNotu)
                .IsUnicode(false);

            modelBuilder.Entity<Notlar>()
                .Property(e => e.OtomatikMi)
                .IsUnicode(false);

            modelBuilder.Entity<Ogrenci>()
                .Property(e => e.Ad)
                .IsUnicode(false);

            modelBuilder.Entity<Ogrenci>()
                .Property(e => e.Soyad)
                .IsUnicode(false);

            modelBuilder.Entity<Ogrenci>()
                .Property(e => e.Cinsiyet)
                .IsUnicode(false);

            modelBuilder.Entity<Ogrenci>()
                .Property(e => e.DogumTarihi)
                .IsUnicode(false);

            modelBuilder.Entity<Ogrenci>()
                .Property(e => e.Eposta)
                .IsUnicode(false);

            modelBuilder.Entity<Ogrenci>()
                .Property(e => e.Telefon)
                .IsUnicode(false);

            modelBuilder.Entity<Ogrenci>()
                .Property(e => e.AktifKayitDonemi)
                .IsUnicode(false);
        }
    }
}
