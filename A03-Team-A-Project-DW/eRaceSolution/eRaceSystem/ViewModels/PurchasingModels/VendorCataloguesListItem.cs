using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.PurchasingModels
{
    public class VendorCataloguesListItem
    {
        public int VendorCatalogID { get; set; }
        public int ProductID { get; set; }
        public string Description { get; set; }
        public int VendorID { get; set; }
        public int OrderUnitSize { get; set; }
        public string Category { get; set; }
        public int QuantityOnOrder { get; set; }
        public int QuantityOnHand { get; set; }
        public int ReOrderLevel { get; set; }
    }
}
