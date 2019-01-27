namespace DatabaseModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UrmarireProdus
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }


        public Guid Id_Produs { get; set; }

        public Guid Id_Utilizator { get; set; }

        public decimal Limita_pret { get; set; }

        public bool UtilizatorNotificat { get; set; }

        public bool Invalid { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? DataNotificarii { get; set; }

        public virtual Produs Produs { get; set; }

        public virtual Utilizator Utilizator { get; set; }


    }
}
