using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.PurchasingModels
{
    public class ItemSummary
    {
        public int OrderDetailID { get; set; }
        public int ProductID { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int OrderUnitSize { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal UnitCost { get; set; }
        public int QuantityOnOrder { get; set; }
        public int QuantityOnHand { get; set; }
        public int ReOrderLevel { get; set; }

        
    }
}
