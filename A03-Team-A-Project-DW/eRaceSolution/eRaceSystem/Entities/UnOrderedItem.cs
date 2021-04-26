namespace eRaceSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class UnOrderedItem
    {
        [Key]
        public int ItemID { get; set; }

        public int OrderID { get; set; }

        [Required(ErrorMessage = "ItemName is required")]
        [StringLength(50, ErrorMessage = "ItemName is limited to 50 chars")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "VendorProductID is required")]
        [StringLength(25, ErrorMessage = "VendorProductID is limited to 25 chars")]
        public string VendorProductID { get; set; }

        public int Quantity { get; set; }
    }
}
