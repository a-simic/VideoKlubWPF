namespace WpfVk2019.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Iznajmljivanje")]
    public partial class Iznajmljivanje
    {
        public int IznajmljivanjeId { get; set; }

        public int FilmId { get; set; }

        public int ClanId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DatumIznajmljivanja { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DatumVracanja { get; set; }

        public decimal? CenaPoDanu { get; set; }

        public virtual Clan Clan { get; set; }

        public virtual Film Film { get; set; }
    }
}
