namespace OMSTSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using OMSTSystem.Entities;


    internal class Movy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Movy()
        {
            ShowTimes = new HashSet<ShowTime>();
        }

        [Key]
        public int MovieID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public int ReleaseYear { get; set; }

        public int RatingID { get; set; }

        public int GenreID { get; set; }

        public int ScreenTypeID { get; set; }

        public int Length { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual Rating Rating { get; set; }

        public virtual ScreenType ScreenType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShowTime> ShowTimes { get; set; }
    }
}
