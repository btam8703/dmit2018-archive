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
    public class MovieController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<MovieInfo> Movies_ListByYear()
        {
            using (var context = new OMSTContext())
            {
                var results = from x in context.Movies
                              select new MovieInfo
                              {
                                  Title = x.Title,
                                  Year = x.ReleaseYear,
                                  Rating = x.Rating.Description,
                                  Genre = x.Genre.Description,
                                  ScreenType = x.ScreenType.Description,
                                  Length = x.Length
                              };
                return results.OrderBy(x => x.Year).ToList();
            }
        }
    }
}
