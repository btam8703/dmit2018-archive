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
    public class RefundController
    {
        //get invoicedetails made
        public List<ReturnInvoiceDetails> Get_InvoiceDetails(int invoiceid)
        {
            using (var context = new eRaceContext())
            {
                var results = (from x in context.InvoiceDetails
                               where x.InvoiceID == invoiceid
                               select new ReturnInvoiceDetails
                                {
                                    ProductID = x.ProductID,
                                    ProductName = x.Product.ItemName,
                                    Quantity = x.Quantity,
                                    Price = x.Price,
                                    ReStockCharge = x.Product.ReStockCharge,
                                    CategoryID = x.Product.CategoryID
                                }).ToList();
                if (results.Count == 0)
                {
                    throw new Exception("Cannot find Invoice #, Invoice Number does not exist or no longer on file");
                }
                return results;
            }
        }

        //store errors inside the list
        List<string> errors = new List<string>();
        //Prepare to return products
        public int ReturnProducts(int employeeid, int invoicenumber, decimal subtotal, decimal gst, decimal total, List<ReturnedProducts> returnedProducts)
        {
            using (var context = new eRaceContext())
            {
                //query existing invoicenumber
                var exists = (from x in context.Invoices
                                  where x.InvoiceID == invoicenumber
                                  select x).FirstOrDefault();

                exists = new Invoice();
                exists.InvoiceDate = DateTime.Now;
                exists.EmployeeID = employeeid;
                exists.SubTotal = subtotal * -1;
                exists.GST = gst * -1;
                exists.Total = total;

                // if it exists add to Invoices
                context.Invoices.Add(exists);
                foreach (var item in returnedProducts)
                {
                    var product = (from x in context.Products
                                       where x.ProductID == item.ProductID
                                       select x).FirstOrDefault();

                    var existingRefund = (from x in context.StoreRefunds
                                                  where x.ProductID == item.ProductID
                                                  && x.OriginalInvoiceID == item.InvoiceID
                                                  select x).FirstOrDefault();

                    //if existing refund is null, it has been returned already
                    if (existingRefund == null)
                    {
                        
                        var newRefund = new StoreRefund();
                        newRefund.InvoiceID = exists.InvoiceID;
                        newRefund.ProductID = item.ProductID;
                        newRefund.OriginalInvoiceID = invoicenumber;
                        newRefund.Reason = item.Reason;

                        if (newRefund.Reason == "")
                        {
                            errors.Add("Please enter a reason for the return");
                        }
                        if (item.Quantity > 0)
                        {
                            product.QuantityOnHand = product.QuantityOnHand + item.Quantity;
                        }

                        //if there are errors point it back to user, otherwise commit changes
                        if (errors.Count > 0)
                        {
                            throw new BusinessRuleException("Refund errors", errors);
                        }
                        else
                        {
                            context.StoreRefunds.Add(newRefund);
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        errors.Add("Product(s) has already been refunded");
                        if (errors.Count > 0)
                        {
                            throw new BusinessRuleException("Refund errors", errors);
                        }
                    }
                }
                //return new generated InvoiceID back to user to see
                return exists.InvoiceID;
            }
        }
    }
}
