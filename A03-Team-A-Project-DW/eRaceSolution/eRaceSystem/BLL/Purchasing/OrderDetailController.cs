using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using eRaceSystem.DAL;
using eRaceSystem.ViewModels.PurchasingModels;

namespace eRaceSystem.BLL.Purchasing
{
    public class OrderDetailController
    {
        public List<ItemSummary> OrderDetail_GetByOrderId(int orderid)
        {
            using (var context = new eRaceContext())
            {
                var results = from orderdetail in context.OrderDetails
                             where orderdetail.OrderID == orderid 
                             select new ItemSummary
                             {
                                 OrderDetailID = orderdetail.OrderDetailID,
                                 ProductID = orderdetail.ProductID,
                                 Description = orderdetail.Product.ItemName,
                                 Quantity = orderdetail.Quantity,
                                 SellingPrice = orderdetail.Product.ItemPrice,
                                 OrderUnitSize = orderdetail.OrderUnitSize,
                                 UnitCost = orderdetail.Cost,
                                 QuantityOnOrder = orderdetail.Product.QuantityOnOrder,
                                 QuantityOnHand = orderdetail.Product.QuantityOnHand,
                                 ReOrderLevel = orderdetail.Product.ReOrderLevel
                             };
                return results.ToList();
            }
        }
    }
}
