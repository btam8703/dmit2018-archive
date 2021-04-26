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
    public class ProductController
    {

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ProductCategory> Product_Get(int categoryid)
        {
            using (var context = new GroceryListContext())
            {
                var results = from x in context.Products
                              where x.CategoryID == categoryid
                              select new ProductCategory
                              {
                                  ProductID = x.ProductID,
                                  Description = x.Description,
                                  Price = x.Price,
                                  Discount = x.Discount,
                                  UnitSize = x.UnitSize,
                                  Taxable = x.Taxable
                              };
                return results.ToList();
            }

        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ProductList> Product_ListDropdown()
        {
            using (var context = new GroceryListContext())
            {
                var results = from x in context.Products
                              select new ProductList
                              {
                                  ProductID = x.ProductID,
                                  Description = x.Description,
                                  Price = x.Price,
                                  Discount = x.Discount,
                                  UnitSize = x.UnitSize,
                                  Taxable = x.Taxable
                              };
                return results.OrderBy(x => x.Description).ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ProductList> Product_List()
        {
            using (var context = new GroceryListContext())
            {
                var results = from x in context.Products
                              select new ProductList
                              {
                                  ProductID = x.ProductID,
                                  Description = x.Description,
                                  Price = x.Price,
                                  Discount = x.Discount,
                                  UnitSize = x.UnitSize,
                                  CategoryID = x.CategoryID,
                                  Taxable = x.Taxable
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert,false)]
        public void Product_Add(ProductList item)
        {
            using (var context = new GroceryListContext())
            {
                Product addItem = new Product()
                {
              
                    Description = item.Description,
                    Price = item.Price,
                    Discount = item.Discount,
                    UnitSize = item.UnitSize,
                    CategoryID = item.CategoryID,
                    Taxable = item.Taxable
                };
                context.Products.Add(addItem);
                context.SaveChanges(); 
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update,false)]
        public void Product_Update(ProductList item)
        {
            using (var context = new GroceryListContext())
            {
                Product updateItem = new Product 
                {
                    ProductID = item.ProductID,
                    Description = item.Description,
                    Price = item.Price,
                    Discount = item.Discount,
                    UnitSize = item.UnitSize,
                    CategoryID = item.CategoryID,
                    Taxable = item.Taxable
                };
                context.Entry(updateItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        
        [DataObjectMethod(DataObjectMethodType.Delete,false)]
        public void Product_Delete(ProductList item)
        {
            Product_Delete(item.ProductID);
        }
        public void Product_Delete(int productid)
        {
            using (var context = new GroceryListContext())
            {
                var exists = context.Products.Find(productid);
                context.Products.Remove(exists);
                context.SaveChanges();
            }
        }
    }
}
