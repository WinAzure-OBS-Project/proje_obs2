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

        [ForeignKey(ADId)]
        public int? ADId { get; set; }

        [ForeignKey(OgrenciNo)]
        public int? OgrenciNo { get; set; }

        public bool OnaylandiMi { get; set; }

        public virtual AcilanDersler AcilanDers { get; set; }
        public virtual Ogrenci Ogrenciler { get; set; }
        public virtual ICollection<Notlar> not { get; set; }
    }
}
