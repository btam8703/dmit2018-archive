namespace OMSTSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    using OMSTSystem.Entities;
    internal class ShowTime
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShowTime()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int ShowTimeID { get; set; }

        public int MovieID { get; set; }

        public DateTime StartDate { get; set; }

        public int TheatreID { get; set; }

        public virtual Movy Movy { get; set; }

        public virtual Theatre Theatre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
