namespace DatabaseModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IstoricCautari")]
    public partial class IstoricCautari
    {
        [StringLength(1000)]
        public string Valoare { get; set; }

        [StringLength(100)]
        public string Cod { get; set; }

        [Key]
        public Guid Id_Cautare { get; set; }
    }
}
