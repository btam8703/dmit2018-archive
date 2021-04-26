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
    [DataObject]
    public class VendorController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<DDL> List_Vendors()
        {
            using (var context = new eRaceContext())
            {
                var results = from vendor in context.Vendors
                              select new DDL
                              {
                                  ValueID = vendor.VendorID,
                                  Displaytext = vendor.Name
                              };
                return results.ToList();
            }
        }

        public VendorDetails Vendor_Get(int vendorid)
        {
            using (var context = new eRaceContext())
            {
                var result = from vendor in context.Vendors
                             where vendor.VendorID == vendorid
                             select new VendorDetails
                             {
                                Name = vendor.Name,
                                Contact = vendor.Contact,
                                Phone = vendor.Phone
                             };
                return result.FirstOrDefault();
            }
        }
        
    }
}
