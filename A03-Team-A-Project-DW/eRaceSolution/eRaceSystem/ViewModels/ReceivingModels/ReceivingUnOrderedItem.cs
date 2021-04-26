using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.ReceivingModels
{
    public class ReceivingUnOrderedItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string VendorProductID { get; set; }
        public int Quantity { get; set; }

    }
}
