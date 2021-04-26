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
    public class VendorCatalogsController
    {
        public List<ItemSummary> VendorCatalogs_GetItems(int vendorid)
        {
            using (var context = new eRaceContext())
            {
                var results = from vendorcatalogitem in context.VendorCatalogs
                              where vendorcatalogitem.VendorID == vendorid
                             select new ItemSummary
                             {
                                 Description = vendorcatalogitem.Product.ItemName,
                                 ProductID = vendorcatalogitem.ProductID,
                                 OrderUnitSize = vendorcatalogitem.OrderUnitSize,
                                 QuantityOnOrder = vendorcatalogitem.Product.QuantityOnOrder,
                                 QuantityOnHand = vendorcatalogitem.Product.QuantityOnHand,
                                 SellingPrice = vendorcatalogitem.Product.ItemPrice,
                                 UnitCost = vendorcatalogitem.OrderUnitCost,
                                 ReOrderLevel = vendorcatalogitem.Product.ReOrderLevel
                             };
                return results.ToList();
            }
        }
    }
}
