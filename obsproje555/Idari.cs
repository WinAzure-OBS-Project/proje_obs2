namespace proje_obs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Idari")]
    public partial class Idari
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Unvan { get; set; }

        [StringLength(50)]
        public string Adi { get; set; }

        [StringLength(50)]
        public string Soyadi { get; set; }

        [StringLength(50)]
        public string Mail { get; set; }

        [StringLength(50)]
        public string Tel { get; set; }

        [StringLength(50)]
        public string Fax { get; set; }

        [StringLength(50)]
        public string Adres { get; set; }


        //public int BolumKodu { get; set; }

        //[ForeignKey("BolumKodu")]
        //public virtual Bolum bolum { get; set; }

        [StringLength(50)]
        public string Sifre { get; set; }
    }
}
