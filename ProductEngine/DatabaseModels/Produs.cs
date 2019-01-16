namespace DatabaseModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Produs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Produs()
        {
            AparitieProdus = new HashSet<AparitieProdus>();
            EvolutiaPretului = new HashSet<EvolutiaPretului>();
            UrmarireProdus = new HashSet<UrmarireProdus>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Url { get; set; }

        [StringLength(250)]
        public string Denumire { get; set; }

        
        public decimal Pret { get; set; }

        [StringLength(50)]
        public string Stock { get; set; }

        [StringLength(250)]
        public string Url_Imagine { get; set; }

        public Guid? Id_Vanzator { get; set; }

        [StringLength(50)]
        public string SKU { get; set; }

        [StringLength(20)]
        public string EAN { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Data_Creat { get; set; }

        [StringLength(100)]
        public string Cod_Denumire_Produs { get; set; }

        public bool? Sters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AparitieProdus> AparitieProdus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EvolutiaPretului> EvolutiaPretului { get; set; }

        public virtual Vanzator Vanzator { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UrmarireProdus> UrmarireProdus { get; set; }
    }
}
