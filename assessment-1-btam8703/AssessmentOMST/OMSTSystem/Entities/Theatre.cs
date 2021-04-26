namespace OMSTSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    using OMSTSystem.Entities;

    internal class Theatre
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Theatre()
        {
            ShowTimes = new HashSet<ShowTime>();
        }

        public int TheatreID { get; set; }

        public int LocationID { get; set; }

        public int TheatreNumber { get; set; }

        public int SeatingSize { get; set; }

        public virtual Location Location { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShowTime> ShowTimes { get; set; }
    }
}
