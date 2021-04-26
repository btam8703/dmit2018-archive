namespace GroceryListSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderLists = new HashSet<OrderList>();
        }

        public int OrderID { get; set; }

        [Required(ErrorMessage = "OrderDate is required")]
        public DateTime OrderDate { get; set; }

        public int StoreID { get; set; }

        public int CustomerID { get; set; }

        public int? PickerID { get; set; }

        public DateTime? PickedDate { get; set; }

        [Required(ErrorMessage = "Delivery is required")]
        public bool Delivery { get; set; }

        [Required(ErrorMessage = "SubTotal is required")]
        [Column(TypeName = "money")]
        public decimal SubTotal { get; set; }

        [Required(ErrorMessage = "GST is required")]
        [Column(TypeName = "money")]
        public decimal GST { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderList> OrderLists { get; set; }

        public virtual Store Store { get; set; }
    }
}
