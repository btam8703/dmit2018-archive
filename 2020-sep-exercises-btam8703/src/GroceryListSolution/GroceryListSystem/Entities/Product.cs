namespace GroceryListSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderLists = new HashSet<OrderList>();
        }

        public int ProductID { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(100, ErrorMessage = "Description is limited to 100 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Discount is required")]
        [Column(TypeName = "money")]
        public decimal Discount { get; set; }

        [Required(ErrorMessage = "UnitSize is required")]
        [StringLength(20, ErrorMessage = "UnitSize is limited to 20 characters")]
        public string UnitSize { get; set; }

        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Taxable is required")]
        public bool Taxable { get; set; }

        public virtual Category Category { get; set; }

        [NotMapped]
        public string DescriptionUnitSize
        {
            get
            {
                return Description + "(" + UnitSize + ")";
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderList> OrderLists { get; set; }
    }
}
