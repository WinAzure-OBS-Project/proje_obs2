namespace proje_obs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Donemler")]
    public partial class Donemler
    {
        [Key]
        [StringLength(50)]
        public string Donem { get; set; }

        public int? Kredi_sayisi { get; set; }

        public int? Secmeli_kredi { get; set; }

        public int? Zorunlu_kredi { get; set; }

        public int? Toplam_ders { get; set; }

        public int? Toplam_secmeli_sayisi { get; set; }
    }
}
