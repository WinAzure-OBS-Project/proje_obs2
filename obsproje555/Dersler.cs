namespace proje_obs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Dersler")]
    public partial class Dersler
    {
        [Key]
        [StringLength(50)]
        public string DersKodu { get; set; }

        [StringLength(50)]
        public string DersAdi { get; set; }

        [StringLength(50)]
        public string Tur { get; set; }

        public int? Kredi { get; set; }

        public int? AKTS { get; set; }

        public int? TeoriDersSaati { get; set; }

        public int? UygulamaDersSaati { get; set; }

        [StringLength(50)]
        public string Donem { get; set; }
    }
}
