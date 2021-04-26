namespace OMSTSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    using OMSTSystem.Entities;

    internal class Ticket
    {
        public int TicketID { get; set; }

        public int ShowTimeID { get; set; }

        public int TicketCategoryID { get; set; }

        [Column(TypeName = "money")]
        public decimal TicketPrice { get; set; }

        [Column(TypeName = "money")]
        public decimal TicketPremium { get; set; }

        public virtual ShowTime ShowTime { get; set; }

        public virtual TicketCategory TicketCategory { get; set; }
    }
}
