using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using eRaceSystem.DAL;
using eRaceSystem.Entities;
using eRaceSystem.ViewModels.ReceivingModels;
using System.ComponentModel; //need for wizard implementation of ObjectDataSource
#endregion

namespace eRaceSystem.BLL.Receiving
{
    [DataObject]
    public class ReceivingOrderController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ReceivingOrderList> ReceivingOrder_List()
        {
            using (var context = new eRaceContext())
            {
                var results = from x in context.Orders

                              where x.Closed == false && x.OrderNumber != null && x.OrderDate != null
                              select new ReceivingOrderList
                              {

                                  ValueId = x.OrderID,
                                  DisplayText = x.OrderID.ToString() + " " + x.Vendor.Name
                              };
                return results.OrderBy(x => x.DisplayText).ToList();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ReceivingVendorDetail> Orders_FindVendorbyID(int orderid)
        {
            using (var context = new eRaceContext())
            {
                var results = from x in context.Orders
                              where x.OrderID == orderid
                              select new ReceivingVendorDetail
                              {
                                  VendorID = x.VendorID,
                                  VendorName = x.Vendor.Name,
                                  VendorAddress = x.Vendor.Address,
                                  VendorContact = x.Vendor.Contact,
                                  VendorPhone = x.Vendor.Phone

                              };

                return results.ToList();
            }
        }



        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ReceivingOrderDetails> Order_FindOrderbyID(int orderid)
        {
            using (var context = new eRaceContext())
            {
                var results = from x in context.OrderDetails
                              where x.OrderID == orderid
                              select new ReceivingOrderDetails
                              {
                                  Item = x.Product.ItemName,

                                  QuantityOrdered = (x.Quantity) * (x.OrderUnitSize),

                                  //both Bulk Quantity and Unit Size go into "OrderedUnits on Listview"

                                  OrderedUnits = (x.Quantity + " x case of " + x.OrderUnitSize).ToString(), 
                                 

                                  //this is the backorder need to find value for this in the db
                                  QuantityOutstanding = 0


                              };

                return results.ToList();




            }
        }
    }
}
      






