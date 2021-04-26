using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRaceSystem.ViewModels.PurchasingModels
{
    public class OrderInfo
    {
        public int OrderId { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public Decimal TaxGST { get; set; }
        public Decimal SubTotal { get; set; }
        public int EmployeeID { get; set; }
        public int VendorID { get; set; }
        public bool Closed { get; set; }
        public string Comment { get; set; }

    }
}
