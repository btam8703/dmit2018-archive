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
    class ReceivingUnOrderedItemsController
    {
        //[DataObjectMethod(DataObjectMethodType.Insert, false)]
        //public void UnOrderedItem_Add(UnOrderedItem item)
        //{
        //    using(var context=new eRaceContext())
        //    {
        //        UnOrderedItem additem = new UnOrderedItem
        //        {
        //            ItemID = item.ItemID,
        //            ItemName = item.ItemName,
        //            VendorProductID = item.VendorProductID,
        //            Quantity = item.Quantity
        //        };
        //        context.UnOrderedItems.Add(additem);
        //        context.SaveChanges();
        //    }   
        //}
        //[DataObjectMethod(DataObjectMethodType.Delete, false)]
        //public void UnOrderedItem_Delete(UnOrderedItem item)
        //{
        //   UnOrderedItem_Delete(item.ItemId);
        //}
       
        //public void Album_Delete(int itemid)
        //{
        //    using (var context = new eRaceContext())
        //    {
        //        var exists = context.UnOrderedItems.Find(itemid);
        //        context.UnOrderedItems.Remove(exists);
        //        context.SaveChanges();
        //    }
        //}

    }
}
