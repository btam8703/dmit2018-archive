namespace GroceryListSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class Delivery
    {
        public int DeliveryID { get; set; }

        public int OrderID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name is limited to 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(12, ErrorMessage = "Phone is limited to 12 characters")]
        public string Phone { get; set; }

        public DateTime ShippedDate { get; set; }
    }
}
