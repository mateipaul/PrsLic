namespace DatabaseModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vanzator")]
    public partial class Vanzator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vanzator()
        {
            Produs = new HashSet<Produs>();
        }

        public Guid Id { get; set; }

        [StringLength(150)]
        public string Nume { get; set; }

        [StringLength(250)]
        public string Site { get; set; }

        [StringLength(10)]
        public string Cod_Tara { get; set; }

        [StringLength(1)]
        public string Delimitator_Zecimala { get; set; }

        public bool? Sters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Produs> Produs { get; set; }
    }
}
