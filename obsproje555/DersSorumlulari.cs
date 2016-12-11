namespace proje_obs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DersSorumlulari")]
    public partial class DersSorumlulari
    {
        [Key]
        public int AkademisyenID { get; set; }

        [StringLength(50)]
        public string Adi { get; set; }

        [StringLength(50)]
        public string Soyadi { get; set; }

        [StringLength(50)]
        public string Unvani { get; set; }

        [StringLength(50)]
        public string Telefonu { get; set; }

        [StringLength(50)]
        public string Maili { get; set; }

        [StringLength(50)]
        public string OdaNo { get; set; }

        [StringLength(50)]
        public string BolumKodu { get; set; }

        [StringLength(50)]
        public string Sifre { get; set; }

        public virtual ICollection<AcilanDersler> AcilanDers { get; set; }
    }
}
