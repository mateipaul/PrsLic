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

        [Key]
        [Column(Order = 1)]
        public Guid Id_Produs { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid Id_Utilizator { get; set; }

        [Key]
        [Column(Order = 3)]
        public decimal Limita_pret { get; set; }

        public virtual Produs Produs { get; set; }

        public virtual Utilizator Utilizator { get; set; }
    }
}
