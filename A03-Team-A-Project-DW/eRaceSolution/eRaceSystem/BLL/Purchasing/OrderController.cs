using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using eRaceSystem.DAL;
using eRaceSystem.ViewModels.PurchasingModels;
using eRaceSystem.Entities;
using DMIT2018Common.UserControls;

namespace eRaceSystem.BLL.Purchasing
{
    [DataObject]
    public class OrderController
    {
        public OrderInfo Orders_CheckOpen(int vendorid)
        {
            using (var context = new eRaceContext())
            {
                var results = from order in context.Orders
                              where order.VendorID == vendorid && order.OrderDate == null && order.OrderNumber == null
                              select new OrderInfo
                              {
                                  OrderId = order.OrderID,
                                  TaxGST = order.TaxGST,
                                  SubTotal = order.SubTotal,
                                  VendorID = order.VendorID,
                                  Comment = order.Comment
                              };
                return results.SingleOrDefault();
            }
        }

        public void DeleteAll (int orderid)
        {
            using (var context = new eRaceContext())
            {
                foreach (OrderDetail orders in context.OrderDetails.Where(x => x.OrderID == orderid))
                {
                    context.OrderDetails.Remove(orders);
                }

                Order order = context.Orders
                            .Where(x => x.OrderID == orderid)
                            .Select(x => x).FirstOrDefault();

                if (order != null)
                {
                    context.Orders.Remove(order);
                }

                context.SaveChanges();
            }
        }

        public void DeleteOrderDetails(int orderid)
        {
            using (var context = new eRaceContext())
            {
                foreach (OrderDetail orders in context.OrderDetails.Where(x => x.OrderID == orderid))
                {
                    context.OrderDetails.Remove(orders);
                }

                context.SaveChanges();
            }
        }
        public void SaveAll(int type, OrderInfo order, List<ItemSummary> items)
        {
            using (var context = new eRaceContext())
            {
                //Type 1 for saving
                if (type == 1)
                {
                    Order orderfinal = new Order();

                    if (order.OrderId != 0)
                    {
                        DeleteOrderDetails(order.OrderId);
                        Order currentorder = context.Orders.Find(order.OrderId);
                        if (currentorder != null)
                        {
                            currentorder.Comment = order.Comment;
                            currentorder.EmployeeID = order.EmployeeID;
                            currentorder.TaxGST = order.TaxGST;
                            currentorder.SubTotal = order.SubTotal;
                            currentorder.VendorID = order.VendorID;
                            currentorder.Comment = order.Comment;
                            context.Entry(currentorder).State = System.Data.Entity.EntityState.Modified;

                            foreach (ItemSummary summary in items)
                            {
                                OrderDetail orderdetail = new OrderDetail();
                                orderdetail.OrderID = order.OrderId;
                                orderdetail.ProductID = summary.ProductID;
                                orderdetail.Quantity = summary.Quantity;
                                orderdetail.OrderUnitSize = summary.OrderUnitSize;
                                orderdetail.Cost = summary.UnitCost;
                                context.OrderDetails.Add(orderdetail);
                            }
                        }
                    }
                    else
                    {
                        orderfinal.Comment = order.Comment;
                        orderfinal.EmployeeID = order.EmployeeID;
                        orderfinal.TaxGST = order.TaxGST;
                        orderfinal.SubTotal = order.SubTotal;
                        orderfinal.VendorID = order.VendorID;
                        orderfinal.Comment = order.Comment;

                        context.Orders.Add(orderfinal);
                        context.SaveChanges();


                        foreach (ItemSummary summary in items)
                        {
                            OrderDetail orderdetail = new OrderDetail();
                            orderdetail.OrderID = orderfinal.OrderID;
                            orderdetail.ProductID = summary.ProductID;
                            orderdetail.Quantity = summary.Quantity;
                            orderdetail.OrderUnitSize = summary.OrderUnitSize;
                            orderdetail.Cost = summary.UnitCost;
                            context.OrderDetails.Add(orderdetail);
                        }
                    }

                    
                    context.SaveChanges();
                }
                //type 2 for placing
                else if (type == 2)
                {
                    Order orderfinal = new Order();

                    if (order.OrderId != 0)
                    {
                        DeleteOrderDetails(order.OrderId);
                        Order currentorder = context.Orders.Find(order.OrderId);
                        if (currentorder != null)
                        {
                            currentorder.Comment = order.Comment;
                            currentorder.EmployeeID = order.EmployeeID;
                            currentorder.TaxGST = order.TaxGST;
                            currentorder.SubTotal = order.SubTotal;
                            currentorder.VendorID = order.VendorID;
                            currentorder.Comment = order.Comment;

                            //Placing order logic
                            var now = DateTime.Now;
                            orderfinal.OrderDate = now.Date;

                            //next number
                            int? nextnumber = context.Orders.Max(x => x.OrderNumber);
                            currentorder.OrderNumber = nextnumber + 1;
                            context.Entry(currentorder).State = System.Data.Entity.EntityState.Modified;

                            foreach (ItemSummary summary in items)
                            {
                                OrderDetail orderdetail = new OrderDetail();
                                orderdetail.OrderID = order.OrderId;
                                orderdetail.ProductID = summary.ProductID;
                                orderdetail.Quantity = summary.Quantity;
                                orderdetail.OrderUnitSize = summary.OrderUnitSize;
                                orderdetail.Cost = summary.UnitCost;
                                context.OrderDetails.Add(orderdetail);

                                Product currentproduct = context.Products.Find(summary.ProductID);
                                currentproduct.QuantityOnOrder += summary.Quantity * summary.OrderUnitSize;
                                context.Entry(currentproduct).State = System.Data.Entity.EntityState.Modified;

                            }
                        }
                    }
                    else
                    {
                        orderfinal.Comment = order.Comment;
                        orderfinal.EmployeeID = order.EmployeeID;
                        orderfinal.TaxGST = order.TaxGST;
                        orderfinal.SubTotal = order.SubTotal;
                        orderfinal.VendorID = order.VendorID;
                        orderfinal.Comment = order.Comment;

                        context.Orders.Add(orderfinal);
                        //Placing order logic
                        var now = DateTime.Now;
                        orderfinal.OrderDate = now.Date;

                        //next number
                        int? nextnumber = context.Orders.Max(x => x.OrderNumber);
                        orderfinal.OrderNumber = nextnumber + 1;

                        context.SaveChanges();


                        foreach (ItemSummary summary in items)
                        {
                            OrderDetail orderdetail = new OrderDetail();
                            orderdetail.OrderID = orderfinal.OrderID;
                            orderdetail.ProductID = summary.ProductID;
                            orderdetail.Quantity = summary.Quantity;
                            orderdetail.OrderUnitSize = summary.OrderUnitSize;
                            orderdetail.Cost = summary.UnitCost;
                            context.OrderDetails.Add(orderdetail);

                            Product currentproduct = context.Products.Find(summary.ProductID);
                            currentproduct.QuantityOnOrder += summary.Quantity * summary.OrderUnitSize;
                            context.Entry(currentproduct).State = System.Data.Entity.EntityState.Modified;
                        }
                    }


                    context.SaveChanges();
                }
            }
        }
    }
}
