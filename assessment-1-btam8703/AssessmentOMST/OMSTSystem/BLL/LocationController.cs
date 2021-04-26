using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using OMSTSystem.DAL;       //activate after Activity 2 is complete
using OMSTSystem.Entities;  //activate after Activity 2 is complete
using OMSTSystem.ViewModels;
using System.ComponentModel;
#endregion

namespace OMSTSystem.BLL
{
    [DataObject]
    public class LocationController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<LocationInfo> Location_Get()
        {
            using (var context = new OMSTContext())
            {
                var results = from x in context.Locations
                              select new LocationInfo
                              {
                                  ValueId = x.LocationID,
                                  DisplayText = x.Description
                              };
                return results.OrderBy(x => x.DisplayText).ToList();
            }
        }

    }
}
