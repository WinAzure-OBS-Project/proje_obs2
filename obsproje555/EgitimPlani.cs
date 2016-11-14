namespace proje_obs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EgitimPlani")]
    public partial class EgitimPlani
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string BolumAdi { get; set; }

        [StringLength(50)]
        public string Onay_tarihi { get; set; }

        [StringLength(50)]
        public string Donem { get; set; }
    }
}
