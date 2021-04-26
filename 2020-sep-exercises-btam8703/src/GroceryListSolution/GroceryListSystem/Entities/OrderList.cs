namespace GroceryListSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderList")]
    internal partial class OrderList
    {
        private string _CustomerComment;
        private string _PickIssue;

        public int OrderListID { get; set; }

        public int OrderID { get; set; }

        public int ProductID { get; set; }

        [Required(ErrorMessage = "QtyOrdered is required")]
        public double QtyOrdered { get; set; }

        public double QtyPicked { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Discount is required")]
        [Column(TypeName = "money")]
        public decimal Discount { get; set; }

        [StringLength(100, ErrorMessage = "CustomerComment is limited to 100 characters")]
        public string CustomerComment
        {
            get { return _CustomerComment; }
            set { _CustomerComment = string.IsNullOrEmpty(value) ? null : value; }
        }

        [StringLength(100, ErrorMessage = "PickIssue is limited to 100 characters")]
        public string PickIssue
        {
            get { return _PickIssue; }
            set { _PickIssue = string.IsNullOrEmpty(value) ? null : value; }
        }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
