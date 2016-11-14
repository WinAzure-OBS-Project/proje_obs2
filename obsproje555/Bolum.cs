namespace proje_obs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bolum")]
    public partial class Bolum
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string BolumAdi { get; set; }

        [StringLength(50)]
        public string BolumKodu { get; set; }

        [StringLength(50)]
        public string Telefon { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        public int? Fakulte_id { get; set; }
    }
}
