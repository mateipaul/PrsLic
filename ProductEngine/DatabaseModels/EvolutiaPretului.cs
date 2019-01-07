namespace DatabaseModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EvolutiaPretului")]
    public partial class EvolutiaPretului
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Pret { get; set; }

        public Guid? Id_Produs { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Data_Actualizare { get; set; }

        public virtual Produs Produs { get; set; }
    }
}
