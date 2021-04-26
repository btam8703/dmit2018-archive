using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.ReceivingModels
{
    public class ReceivingOrderDetails
    {
        public string Item { get; set; }
        public int QuantityOrdered { get; set; }
        public int BulkQuantity { get; set; }
        public int UnitSize { get; set; }
        public string OrderedUnits { get; set; }

        
        
        public int QuantityOutstanding { get; set; }

    

    }
}
