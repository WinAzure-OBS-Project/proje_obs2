namespace db_updater
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DersTarihlers
    {
        public string Id { get; set; }

        public string Yıl { get; set; }

        public DateTime dersEklemeBaslangic { get; set; }

        public DateTime dersAcmaBaslangic { get; set; }

        public DateTime dersEklemeBitis { get; set; }

        public DateTime dersAcmaBitis { get; set; }
    }
}
