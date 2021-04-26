namespace OMSTSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using OMSTSystem.Entities;

    internal class TicketCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TicketCategory()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int TicketCategoryID { get; set; }

        [Required]
        [StringLength(30)]
        public string Description { get; set; }

        [Column(TypeName = "money")]
        public decimal TicketPrice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
