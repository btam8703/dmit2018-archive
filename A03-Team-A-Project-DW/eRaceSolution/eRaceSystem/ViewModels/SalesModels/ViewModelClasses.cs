using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace eRaceSystem.ViewModels
{
    public class ProductList
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
    }

    public class ShoppingCartList
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [NotMapped]
        public decimal Amount
        {
            get
            {
                return Price * Quantity;
            }
        }
    }

    public class ReturnInvoiceDetails
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal ReStockCharge { get; set; }
        public int CategoryID { get; set; }

        [NotMapped]
        public decimal Amount
        {
            get
            {
                return (decimal)(Price * Quantity);
            }
        }
    }

    public class ReturnedProducts
    {
        public int InvoiceID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string Reason { get; set; }
    }

}
