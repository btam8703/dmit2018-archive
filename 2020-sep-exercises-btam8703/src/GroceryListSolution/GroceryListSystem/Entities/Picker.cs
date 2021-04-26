namespace GroceryListSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class Picker
    {
        public int PickerID { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(35, ErrorMessage = "Last Name is limited to 35 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(25, ErrorMessage = "First Name is limited to 25 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Active is required")]
        public bool Active { get; set; }

        public int StoreID { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return LastName + ", " + FirstName; }
        }

        public virtual Store Store { get; set; }
    }
}
