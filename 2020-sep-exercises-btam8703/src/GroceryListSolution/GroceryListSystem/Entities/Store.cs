namespace GroceryListSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class Store
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Store()
        {
            Orders = new HashSet<Order>();
            Pickers = new HashSet<Picker>();
        }

        public int StoreID { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(50, ErrorMessage = "Location is limited to 50 characters")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(30, ErrorMessage = "Address is limited to 30 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(30, ErrorMessage = "City is limited to 30 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "Province is required")]
        [StringLength(2, ErrorMessage = "Province is limited to 2 characters")]
        public string Province { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(12, ErrorMessage = "Phone is limited to 12 characters")]
        public string Phone { get; set; }

        [NotMapped]
        public string CityLocation
        {
            get
            {
                return City + ":" + Location;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Picker> Pickers { get; set; }
    }
}
