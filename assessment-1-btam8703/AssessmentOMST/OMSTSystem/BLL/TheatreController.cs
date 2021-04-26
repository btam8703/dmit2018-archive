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
using System.Security.Cryptography.X509Certificates;
#endregion

namespace OMSTSystem.BLL
{
    [DataObject]
    public class TheatreController
    {
        //activate after Activity 2 is complete

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<TheatreItem> Theatres_List()
        {
            using (var context = new OMSTContext())
            {
                var results = from x in context.Theatres
                              select new TheatreItem
                              {
                                  TheatreID = x.TheatreID,
                                  LocationID = x.LocationID,
                                  TheatreNumber = x.TheatreNumber,
                                  SeatingSize = x.SeatingSize
                              };
                return results.ToList();
            }
        }

        //activate after Activity 2 is complete

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public TheatreItem Theatres_Get(int theatreid)
        {
            using (var context = new OMSTContext())
            {
                var results = (from x in context.Theatres
                               where x.TheatreID == theatreid
                               select new TheatreItem
                               {
                                   TheatreID = x.TheatreID,
                                   LocationID = x.LocationID,
                                   TheatreNumber = x.TheatreNumber,
                                   SeatingSize = x.SeatingSize
                               }).FirstOrDefault();
                return results;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void Theatre_Add(TheatreItem item)
        {
            using (var context = new OMSTContext())
            {
                Theatre addItem = new Theatre()
                {
                    LocationID = item.LocationID,
                    TheatreNumber = item.TheatreNumber,
                    SeatingSize = item.SeatingSize
                };
                context.Theatres.Add(addItem);
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Theatre_Update(TheatreItem item)
        {
            using (var context = new OMSTContext())
            {
                Theatre updateItem = new Theatre
                {
                    TheatreID = item.TheatreID,
                    LocationID = item.LocationID,
                    TheatreNumber = item.TheatreNumber,
                    SeatingSize = item.SeatingSize
                };
                context.Entry(updateItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Delete,false)]
        public void Theatre_Delete(TheatreItem item)
        {
            Theatre_Delete(item.TheatreID);
        }
        public void Theatre_Delete(int theatreid)
        {
            using (var context = new OMSTContext())
            {
                var exists = context.Theatres.Find(theatreid);
                context.Theatres.Remove(exists);
                context.SaveChanges();
            }
        }
        //TODO: add the methods to Add, Update and Delete a Theatre record

    }
}
