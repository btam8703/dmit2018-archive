namespace eRaceSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class CarClass
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CarClass()
        {
            Cars = new HashSet<Car>();
        }

        public int CarClassID { get; set; }

        [Required(ErrorMessage = "CarClassName is required")]
        [StringLength(30, ErrorMessage = "CarClassName is limited to 30 chars")]
        public string CarClassName { get; set; }

        public decimal MaxEngineSize { get; set; }

        [Required(ErrorMessage = "CertificationLevel is required")]
        [StringLength(1, ErrorMessage = "CertificationLevel is limited to 1 char")]
        public string CertificationLevel { get; set; }

        [Column(TypeName = "money")]
        public decimal RaceRentalFee { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(255, ErrorMessage = "Description is limited to 255 chars")]
        public string Description { get; set; }

        public virtual Certification Certification { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Car> Cars { get; set; }
    }
}
