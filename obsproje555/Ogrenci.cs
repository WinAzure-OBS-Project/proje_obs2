namespace proje_obs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ogrenci")]
    public partial class Ogrenci
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OgrenciNo { get; set; }

        [StringLength(50)]
        public string Ad { get; set; }

        [StringLength(50)]
        public string Soyad { get; set; }

        [StringLength(50)]
        public string Cinsiyet { get; set; }

        [StringLength(50)]
        public string DogumTarihi { get; set; }

        [StringLength(50)]
        public string Eposta { get; set; }

        [StringLength(50)]
        public string Telefon { get; set; }

        public double? MezunOlunanOkulPuani { get; set; }

        [StringLength(50)]
        public string AktifKayitDonemi { get; set; }

        public int? DanismanId { get; set; }

        [StringLength(50)]
        public string Sifre { get; set; }
    }
}
