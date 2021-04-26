namespace OMSTSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    using OMSTSystem.Entities;

    internal class ScreenType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ScreenType()
        {
            Movies = new HashSet<Movy>();
        }

        public int ScreenTypeID { get; set; }

        [Required]
        [StringLength(30)]
        public string Description { get; set; }

        public bool Premium { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Movy> Movies { get; set; }
    }
}
