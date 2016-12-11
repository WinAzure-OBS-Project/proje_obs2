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

        public int? ADId { get; set; }

        public int? OgrenciNo { get; set; }

        public bool OnaylandiMi { get; set; }

        [ForeignKey("ADId")]
        public virtual AcilanDersler AcilanDers { get; set; }
        [ForeignKey("OgrenciNo")]
        public virtual Ogrenci Ogrenci { get; set; }

        public virtual ICollection<Notlar> notlar { get; set; }
    }
}
