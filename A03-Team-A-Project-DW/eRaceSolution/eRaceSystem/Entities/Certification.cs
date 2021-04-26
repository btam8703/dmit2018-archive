namespace eRaceSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class Certification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Certification()
        {
            CarClasses = new HashSet<CarClass>();
            Members = new HashSet<Member>();
            Races = new HashSet<Race>();
        }

        [Key]
        [StringLength(1, ErrorMessage = "CertificationLevel is limited to 1 char")]
        public string CertificationLevel { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(30, ErrorMessage = "Description is limited to 30 chars")]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarClass> CarClasses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Member> Members { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Race> Races { get; set; }
    }
}
