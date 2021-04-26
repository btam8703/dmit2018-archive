using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GroceryListSystem.DAL;
using GroceryListSystem.Entities;
using GroceryListSystem.ViewModels;
using System.ComponentModel;


namespace GroceryListSystem.BLL
{
    [DataObject]
    public class CategoryController
    {
        //dropdown selects all categories
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SelectionList> Category_List()
        {
            using (var context = new GroceryListContext())
            {
                var results = from x in context.Categories
                              select new SelectionList
                              {
                                  ValueId = x.CategoryID,
                                  DisplayText = x.Description
                              };
                return results.OrderBy(x => x.DisplayText).ToList();
            }
        }

        
    }
}
