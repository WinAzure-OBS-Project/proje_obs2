namespace proje_obs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AcilanDersler")]
    public partial class AcilanDersler
    {
        [Key]
        public int ADId { get; set; }

        [StringLength(50)]
        public string DersKodu { get; set; }

        [StringLength(50)]
        public string DersAdi { get; set; }
        
        public int AkademisyenId { get; set; }

        [ForeignKey("AkademisyenId")]
        public virtual DersSorumlulari DersSorumlusu { get; set; }

        public virtual ICollection<Kayit> Kayitlar { get; set; }

        public int? YariYil { get; set; }

        public int? YilDers { get; set; }

        public bool OnaylandiMi { get; set; }
    }
}
