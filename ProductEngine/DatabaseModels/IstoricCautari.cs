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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IstoricCautari()
        {
            AparitieProdus = new HashSet<AparitieProdus>();
        }

        [Key]
        public Guid Id_Cautare { get; set; }

        [StringLength(1000)]
        public string Valoare { get; set; }

        [StringLength(100)]
        public string Cod { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AparitieProdus> AparitieProdus { get; set; }
    }
}
