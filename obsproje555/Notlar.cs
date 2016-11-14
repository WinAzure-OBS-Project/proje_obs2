namespace proje_obs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Notlar")]
    public partial class Notlar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NotId { get; set; }

        public int? KayitId { get; set; }

        public int? Vize { get; set; }

        public int? Final { get; set; }

        public int? But { get; set; }

        public double? YilNot { get; set; }

        [StringLength(50)]
        public string HarfNotu { get; set; }

        [StringLength(50)]
        public string OtomatikMi { get; set; }
    }
}
