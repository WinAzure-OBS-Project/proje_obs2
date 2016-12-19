namespace proje_obs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kayit")]
    public partial class Kayit
    {
        public int KayitId { get; set; }

        public int ADId { get; set; }

        [ForeignKey("ADId")]
        public AcilanDersler AcilanDers { get; set; }

        [ForeignKey("Ogrenci")]
        public int? OgrenciNo { get; set; }

        
        public virtual Ogrenci Ogrenci { get; set; }

        public bool OnaylandiMi { get; set; }

        public int NotId { get; set; }

        [ForeignKey("NotId")]
        public Notlar not { get; set; }
    }
}
