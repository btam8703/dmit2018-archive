using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel;
using eRaceSystem.ViewModels;
using eRaceSystem.ViewModels.SalesModels;
using eRaceSystem.DAL;
using eRaceSystem.Entities;
using DMIT2018Common.UserControls;
#endregion

namespace eRaceSystem.BLL.Sales
{
    [DataObject]
    public class SalesController
    {

        //List categories for DDL
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<SelectionList> List_Categories()
        {
            using (var context = new eRaceContext())
            {
                var results = from x in context.Categories
                              select new SelectionList
                              {
                                  ValueID = x.CategoryID,
                                  Displaytext = x.Description
                              };
                return results.ToList();
            }
        }

        //List products by category selected
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ProductList> List_ProductsByCategory(int categoryid)
        {
            using (var context = new eRaceContext())
            {
                var results = from x in context.Products
                              where x.CategoryID == categoryid
                              select new ProductList
                              {
                                  ProductID = x.ProductID,
                                  CategoryID = x.CategoryID,
                                  ItemName = x.ItemName,
                                  ItemPrice = x.ItemPrice
                              };
                return results.ToList();
            }
        }

        //Get Price of product
        public decimal Get_UnitPrice(int productid)
        {
            using (var context = new eRaceContext())
            {
                decimal unitprice = (from x in context.Products
                                 where x.ProductID == productid
                                 select x.ItemPrice).FirstOrDefault();
                return unitprice;
            }
        }

        //Get name of product
        public string Get_ItemName(int productid)
        {
            using (var context = new eRaceContext())
            {
                string itemname = (from x in context.Products
                                 where x.ProductID == productid
                                 select x.ItemName).FirstOrDefault();
                return itemname;
            }
        }

        //store errors inside the list
        List<string> errors = new List<string>(); 
        //Create a new voice
        public int CreateInvoice(int employeeid, decimal subtotal, decimal gst, decimal ordertotal, List<ShoppingCartList> cartList)
        {
            using (var context = new eRaceContext())
            {
                var tempEmployeeid = employeeid;
                var invoiceDate = DateTime.Now;
                var invoiceSubtotal = subtotal;
                var invoiceGST = gst;
                var invoiceTotal = ordertotal;

                Invoice exists = (from x in context.Invoices
                                  where x.EmployeeID == employeeid
                                  && x.InvoiceDate.Equals(DateTime.Now)
                                  select x).FirstOrDefault();

                if (exists == null) //if there is no record of that existing Invoice create an Invoice
                {
                    exists = new Invoice();
                    exists.InvoiceDate = invoiceDate;
                    exists.EmployeeID = employeeid;
                    exists.SubTotal = invoiceSubtotal;
                    exists.GST = invoiceGST;
                    exists.Total = invoiceTotal;

                    //Add the recorded data to the invoice to the Invoice table
                    context.Invoices.Add(exists); 

                    //for each item in the cartList that was passed create a new InvoiceDetail
                    foreach (ShoppingCartList item in cartList) 
                    {
                        var product = (from x in context.Products
                                       where x.ProductID == item.ProductID
                                       select x).FirstOrDefault();

                        InvoiceDetail newProduct = new InvoiceDetail(); //create a new product
                        newProduct.InvoiceID = exists.InvoiceID;
                        newProduct.Price = item.Price;
                        newProduct.ProductID = item.ProductID;
                        newProduct.Quantity = item.Quantity;

                        //add the recorded data from the cartlist to the InvoiceDetail table
                        context.InvoiceDetails.Add(newProduct);

                        //if more than one quantity of products were selected (QuantityOnHand - Quantity)
                        if (newProduct.Quantity > 0) 
                        {
                            //QuantityOnHand - Quantiity
                            product.QuantityOnHand = product.QuantityOnHand - newProduct.Quantity; 
                        }
                        context.SaveChanges();
                    }
                }
                else
                {
                    errors.Add("You cannot add an already existing Invoice");
                }

                if (exists.InvoiceID == 0)
                {
                    errors.Add("You cannot create an empty Invoice");
                }

                if (errors.Count > 0)
                {
                    throw new BusinessRuleException("Errors: Look beloew to see", errors);
                }

                return exists.InvoiceID;
            }
        }
    }
}
