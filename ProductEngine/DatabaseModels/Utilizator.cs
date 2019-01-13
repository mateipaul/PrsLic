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
        private string v;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Utilizator()
        {
            UrmarireProdus = new HashSet<UrmarireProdus>();
        }

        public Utilizator(string v)
        {
            new Utilizator { Id = Guid.NewGuid(), Email = "admin@admin.com", Numar_Telefon = "074962682", Nume_Utilizator = "admin", Parola = "admin", Porecla = "admin", Rol = "1" };
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

        [StringLength(200)]
        public string CheieParola { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UrmarireProdus> UrmarireProdus { get; set; }
    }
}
