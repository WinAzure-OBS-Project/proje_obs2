namespace proje_obs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Fakulte")]
    public partial class Fakulte
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string FakulteAdi { get; set; }

        [StringLength(50)]
        public string FakulteKodu { get; set; }

        [StringLength(200)]
        public string Adres { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        [StringLength(50)]
        public string Telefon { get; set; }
    }
}
