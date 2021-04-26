namespace GroceryListSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerID { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [StringLength(35, ErrorMessage = "LastName is limited to 35 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        [StringLength(25, ErrorMessage = "FirstName is limited to 25 characters")]
        public string FirstName { get; set; }

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
        public string FullName
        {
            get { return LastName + ", " + FirstName; }
        }

        [NotMapped]
        public string CityCustomer
        {
            get { return City + ": " + LastName + ", " + FirstName; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
