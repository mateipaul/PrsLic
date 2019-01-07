namespace DatabaseModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Utilizator")]
    public partial class Utilizator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Utilizator()
        {
            UrmarireProdus = new HashSet<UrmarireProdus>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nume_Utilizator { get; set; }

        [Required]
        [StringLength(250)]
        public string Parola { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(14)]
        public string Numar_Telefon { get; set; }

        [StringLength(100)]
        public string Porecla { get; set; }

        [StringLength(10)]
        public string Rol { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UrmarireProdus> UrmarireProdus { get; set; }
    }
}
