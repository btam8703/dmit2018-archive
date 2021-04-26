using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using eRaceProject.Security;
using eRaceSystem.BLL;
using eRaceSystem.BLL.Purchasing;
using eRaceSystem.ViewModels;
using eRaceSystem.ViewModels.PurchasingModels;
#endregion

namespace eRaceProject.Subsystem
{
    public partial class Purchasing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Security
            if (Request.IsAuthenticated)
            {

                if (User.IsInRole("Director"))
                {

                    //obtain the CustomerId on the security User record
                    SecurityController ssysmgr = new SecurityController();
                    int? employeeid = ssysmgr.GetCurrentUserDirectorID(User.Identity.Name);

                    //need to convert the int? to an int for the call to the CustomerController method
                    //int custid = customerid == null ? default(int) : int.Parse(customerid.ToString());
                    int empid = employeeid ?? default(int);

                    MessageUserControl.TryRun(() =>
                    {
                        EmployeeController csysmgr = new EmployeeController();
                        EmployeeItem item = csysmgr.Employee_FindByID(empid);
                        if (item == null)
                        {
                            LoggedUser.Text = "Unknown";
                            throw new Exception("Logged employee cannot be found on file ");
                        }
                        else
                        {
                            EmployeeIDField.Text = empid.ToString();
                            LoggedUser.Text = item.LastName + ", " + item.FirstName;
                        }
                    });

                }
                else if (User.IsInRole("OfficeManager"))
                {

                    //obtain the CustomerId on the security User record
                    SecurityController ssysmgr = new SecurityController();
                    int? employeeid = ssysmgr.GetCurrentUserOfficeManagerID(User.Identity.Name);

                    //need to convert the int? to an int for the call to the CustomerController method
                    //int custid = customerid == null ? default(int) : int.Parse(customerid.ToString());
                    int empid = employeeid ?? default(int);

                    MessageUserControl.TryRun(() =>
                    {
                        EmployeeController csysmgr = new EmployeeController();
                        EmployeeItem item = csysmgr.Employee_FindByID(empid);
                        if (item == null)
                        {
                            LoggedUser.Text = "Unknown";
                            throw new Exception("Logged employee cannot be found on file ");
                        }
                        else
                        {
                            EmployeeIDField.Text = empid.ToString();
                            LoggedUser.Text = item.LastName + ", " + item.FirstName;
                        }
                    });

                }

                else
                {
                    Response.Redirect("~/SamplePages/AccessDenied.aspx");
                }
            }


            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            #endregion
        }

        #region Error Handling
        protected void SelectCheckForException(object sender,
                                       ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }
        protected void InsertCheckForException(object sender,
                                              ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Success", "Album has been added.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }
        protected void UpdateCheckForException(object sender,
                                               ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Success", "Album has been updated.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }
        protected void DeleteCheckForException(object sender,
                                                ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Success", "Album has been removed.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }


        #endregion

        protected void SelectVendor_Click(object sender, EventArgs e)
        {
            if (VendorSelectionDDL.SelectedIndex > 0)
            {
                MessageUserControl.TryRun(() =>
                {
                    //Renable and disable controls
                    VendorSelectionDDL.Enabled = false;
                    SelectVendor.Enabled = false;
                    SaveOrder.Enabled = true;
                    CancelOrder.Enabled = true;
                    DeleteOrder.Enabled = true;
                    PlaceOrder.Enabled = true;

                    //Change css style
                    SelectVendor.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";

                    SaveOrder.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "hand";
                    CancelOrder.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "hand";
                    DeleteOrder.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "hand";
                    PlaceOrder.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "hand";


                    //Grab vendor info and add it
                    var vendorcontroller = new VendorController();
                    var vendorinfo = vendorcontroller.Vendor_Get(int.Parse(VendorSelectionDDL.SelectedValue));

                    VendorInfoLabel.Text = vendorinfo.Name + " - " + vendorinfo.Contact + " - " + vendorinfo.Phone;

                    //Visibility and rebinding data sources
                    OrderInfoPanel.Visible = true;

                    OrderDetailItemsGV.DataSource = null;
                    OrderDetailItemsGV.DataBind();
                    VendorCatalogGV.DataSource = null;
                    VendorCatalogGV.DataBind();

                    OrderDetailItemsGV.Visible = true;
                    VendorCatalogGV.Visible = true;

                    //Populate order info method
                    PopulateOrderInfo(int.Parse(VendorSelectionDDL.SelectedValue));
                }, "Vendor", "View vendor info");
            }
            else
            {
                MessageUserControl.ShowInfo("Vendor Selection Error", "Select a Vendor from the list");
            }
        }

        private void PopulateOrderInfo(int vendorid)
        {
            var ordercontroller = new OrderController();
            var orderdetailcontroller = new OrderDetailController();
            var catalogcontroller = new VendorCatalogsController();

            var orderinfo = ordercontroller.Orders_CheckOpen(vendorid);
            if (orderinfo != null)
            {
                //Populate Order Fields
                OrderIDLabel.Text = orderinfo.OrderId.ToString();
                CommentTextBox.Text = orderinfo.Comment;
                SubTotalTextBox.Text = string.Format("{0:C}", orderinfo.SubTotal);
                TaxTextBox.Text = string.Format("{0:C}", orderinfo.TaxGST);
                TotalTextBox.Text = string.Format("{0:C}", orderinfo.SubTotal + orderinfo.TaxGST);

                //Items already in the order
                List<ItemSummary> orderdetails = orderdetailcontroller.OrderDetail_GetByOrderId(orderinfo.OrderId);


                //All Catalog Items
                List<ItemSummary> catalogitems = catalogcontroller.VendorCatalogs_GetItems(int.Parse(VendorSelectionDDL.SelectedValue));
                if (orderdetails.Count == 0)
                {
                    VendorCatalogGV.DataSource = catalogitems;
                    VendorCatalogGV.DataBind();
                }
                else
                {
                    //Filtering
                    IEnumerable<ItemSummary> filteredItems = catalogitems.Where(i => !orderdetails.Any(x => x.ProductID == i.ProductID));

                    VendorCatalogGV.DataSource = filteredItems;
                    VendorCatalogGV.DataBind();
                }
                //Items already in order
                OrderDetailItemsGV.DataSource = orderdetails;
                OrderDetailItemsGV.DataBind();


            }
            else
            {
                //new order - visibility stuff

                var catalogcontroller2 = new VendorCatalogsController();
                List<ItemSummary> catalogitems = catalogcontroller2.VendorCatalogs_GetItems(int.Parse(VendorSelectionDDL.SelectedValue));
                VendorCatalogGV.DataSource = catalogitems;
                VendorCatalogGV.DataBind();

                OrderIDLabel.Text = null;
                CommentTextBox.Text = null;
                SubTotalTextBox.Text = "$0.00";
                TaxTextBox.Text = "$0.00";
                TotalTextBox.Text = "$0.00";
            }
        }

        protected void OrderDetailItemsGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label unitsize = ((Label)e.Row.FindControl("UnitSizeLabel"));
                TextBox perunitcost = ((TextBox)e.Row.FindControl("UnitCostTextboxGV"));
                TextBox orderqty = ((TextBox)e.Row.FindControl("OrderQtyTextboxGV"));

                Label peritemcost = ((Label)e.Row.FindControl("PerItemCostLabel"));
                Label extendedcost = ((Label)e.Row.FindControl("ExtendedCostLabel"));

                //Changing text for unit size
                double unitsizebefore = double.Parse(unitsize.Text);
                if (unitsizebefore == 1)
                {
                    unitsize.Text = "each";

                }
                else
                {
                    unitsize.Text += " per case";
                }

                //removing trailing zeroes
                double num = double.Parse(perunitcost.Text);
                var format2 = string.Format("{0:0.00}", num);
                string formattednum = "";
                if (format2.EndsWith("00"))
                {
                    formattednum = ((int)num).ToString();
                    perunitcost.Text = formattednum;

                }
                else
                {
                    perunitcost.Text = format2;
                }

                //recalculating amounts
                if (orderqty.Text != null)
                {
                    double unitcost = double.Parse(perunitcost.Text);
                    double orderedqty = double.Parse(orderqty.Text);

                    double singleunitcost = Math.Round(unitcost / unitsizebefore, 2);
                    peritemcost.Text = "$" + string.Format("{0:#.00}", singleunitcost);
                    extendedcost.Text = "$" + string.Format("{0:#.00}", unitcost * orderedqty);
                }
            }

        }

        protected void Refresh_Command(object sender, CommandEventArgs e)
        {

        }

        protected void OrderDetailItemsGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RemoveItem")
            {
                List<ItemSummary> orderitems = new List<ItemSummary>();
                List<ItemSummary> catalogitems = new List<ItemSummary>();
                MessageUserControl.TryRun(() =>
                {
                    int indextoremove = Convert.ToInt32(e.CommandArgument);


                    foreach (GridViewRow row in OrderDetailItemsGV.Rows)
                    {
                        //Grab all controls
                        ItemSummary orderitem = new ItemSummary();
                        HiddenField productid = row.FindControl("ProductID") as HiddenField;
                        HiddenField baseprice = row.FindControl("BaseItemPrice") as HiddenField;
                        HiddenField reorderlevel = row.FindControl("ReOrderAmount") as HiddenField;
                        HiddenField onorderlevel = row.FindControl("OnOrder") as HiddenField;
                        HiddenField instock = row.FindControl("InStock") as HiddenField;
                        Label description = row.FindControl("Description") as Label;
                        TextBox unitcost = row.FindControl("UnitCostTextboxGV") as TextBox;
                        Label unitsize = row.FindControl("UnitSizeLabel") as Label;
                        TextBox orderqty = row.FindControl("OrderQtyTextboxGV") as TextBox;

                        //Setting values
                        orderitem.ProductID = int.Parse(productid.Value);
                        orderitem.Description = description.Text;
                        orderitem.ReOrderLevel = int.Parse(reorderlevel.Value);
                        orderitem.QuantityOnHand = int.Parse(instock.Value);
                        orderitem.QuantityOnOrder = int.Parse(onorderlevel.Value);
                        orderitem.SellingPrice = decimal.Parse(baseprice.Value);
                        orderitem.UnitCost = decimal.Parse(unitcost.Text);
                        orderitem.Quantity = int.Parse(orderqty.Text);

                        string unittext = unitsize.Text;

                        //getting values from text
                        if (unittext == "each")
                        {
                            orderitem.OrderUnitSize = 1;
                        }
                        else
                        {
                            string unitsizenotext = string.Empty;
                            for (int i = 0; i < unittext.Length; i++)
                            {
                                if (Char.IsDigit(unittext[i]))
                                {
                                    unitsizenotext += unittext[i];
                                }
                            }

                            orderitem.OrderUnitSize = int.Parse(unitsizenotext);
                        }
                        
                        //sorting values
                        if (row.RowIndex != indextoremove)
                        {
                            orderitems.Add(orderitem);
                        }
                        else
                        {
                            catalogitems.Add(orderitem);
                        }
                    }

                    //for catalog now
                    foreach (GridViewRow row in VendorCatalogGV.Rows)
                    {
                        //grab all controls
                        ItemSummary item = new ItemSummary();
                        HiddenField productid = row.FindControl("ProductID") as HiddenField;
                        Label description = row.FindControl("Description") as Label;
                        Label reorderlevel = row.FindControl("ReOrderLevel") as Label;
                        Label instock = row.FindControl("InStock") as Label;
                        Label onorder = row.FindControl("OnOrder") as Label;
                        Label unitsize = row.FindControl("UnitSize") as Label;
                        HiddenField baseitemprice = row.FindControl("BaseItemPrice") as HiddenField;
                        HiddenField unitcost = row.FindControl("UnitCost") as HiddenField;

                        //Setting values
                        item.ProductID = int.Parse(productid.Value);
                        item.Description = description.Text;
                        item.ReOrderLevel = int.Parse(reorderlevel.Text);
                        item.QuantityOnHand = int.Parse(instock.Text);
                        item.QuantityOnOrder = int.Parse(onorder.Text);
                        item.SellingPrice = decimal.Parse(baseitemprice.Value);
                        item.UnitCost = decimal.Parse(unitcost.Value);

                        //Unit Size
                        //Checking for string
                        string unittext = unitsize.Text;

                        if (unittext == "each")
                        {
                            item.OrderUnitSize = 1;
                        }
                        else
                        {
                            string unitsizenotext = string.Empty;
                            for (int i = 0; i < unittext.Length; i++)
                            {
                                if (Char.IsDigit(unittext[i]))
                                {
                                    unitsizenotext += unittext[i];
                                }
                            }

                            item.OrderUnitSize = int.Parse(unitsizenotext);
                        }

                        catalogitems.Add(item);
                    }
                }, "Order Items", "Removing order items");


                //Binding datasources
                OrderDetailItemsGV.DataSource = orderitems;
                OrderDetailItemsGV.DataBind();

                VendorCatalogGV.DataSource = catalogitems;
                VendorCatalogGV.DataBind();

                UpdateTotalAmounts();
            }
            else if (e.CommandName == "RefreshAmounts")
            {
                //MessageUserControl.TryRun(() =>
                //{
                    //Relevant controls 
                    GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    Label itemname = row.FindControl("Description") as Label;
                    TextBox qtyordered = row.FindControl("OrderQtyTextboxGV") as TextBox;
                    Label unitsize = row.FindControl("UnitSizeLabel") as Label;
                    TextBox unitcost = row.FindControl("UnitCostTextboxGV") as TextBox;
                    HiddenField baseitemprice = row.FindControl("BaseItemPrice") as HiddenField;

                    Label overcostindicator = row.FindControl("OverItemCostsLabel") as Label;

                    Label peritemcost = row.FindControl("PerItemCostLabel") as Label;
                    Label extendedcost = row.FindControl("ExtendedCostLabel") as Label;

                    //remove "per case" or "each"
                    string unitsizestring = unitsize.Text;
                    decimal unitsizedec = 0;
                    if (unitsize.Text == "each")
                    {
                        unitsizedec = 1;
                    }
                    else
                    {
                        string unitsizenostring = string.Empty;
                        for (int i = 0; i < unitsizestring.Length; i++)
                        {
                            if (Char.IsDigit(unitsizestring[i]))
                            {
                            unitsizenostring += unitsizestring[i];
                            }
                        }
                        unitsizedec = decimal.Parse(unitsizenostring);
                    }
                    

                    //Qty Ordered empty value
                    string qtyorderedvalidate = qtyordered.Text;
                    double qtyordereddouble;
                    if (string.IsNullOrEmpty(qtyorderedvalidate))
                    {
                        MessageUserControl.ShowInfo(itemname.Text + " refresh:", "Cannot enter an empty quantity");
                    }
                    else
                    {
                        //Qty ordered whole positive number
                        qtyordereddouble = double.Parse(qtyorderedvalidate);
                        bool iswhole = qtyordereddouble == (int)qtyordereddouble;
                        if (iswhole == false || qtyordereddouble < 0)
                        {
                            MessageUserControl.ShowInfo(itemname.Text + " refresh:", "Enter a whole positive number");
                        }
                        else
                        {
                            //validate unit cost
                            string validateunitcost = unitcost.Text;
                            decimal unitcostdecimal;
                            //Unitcost empty value
                            if (string.IsNullOrEmpty(validateunitcost))
                            {
                                MessageUserControl.ShowInfo(itemname.Text + " refresh:", "Cannot enter an empty unit cost");
                            }
                            else
                            {

                                //Unit cost proper money amount and positive
                                //Do not need to check for non numeric values since textmode = number
                                unitcostdecimal = decimal.Parse(validateunitcost);
                                if (Decimal.Round(unitcostdecimal, 2) != unitcostdecimal || unitcostdecimal < 0)
                                {
                                    MessageUserControl.ShowInfo(itemname.Text + " refresh:", "Enter a valid positive dollar amount");
                                }
                                else
                                {
                                    decimal decunitcost = decimal.Parse(unitcost.Text);
                                    decimal singleunitcost = Math.Round(decunitcost / unitsizedec, 2);

                                    if (singleunitcost > decimal.Parse(baseitemprice.Value))
                                    {
                                        //Add ! !
                                        overcostindicator.Text = "! !";
                                        //Style !!
                                        overcostindicator.ForeColor = System.Drawing.Color.Red;
                                        overcostindicator.Font.Bold = true;
                                        double num = double.Parse(baseitemprice.Value);
                                        var format2 = string.Format("{0:0.00}", num);
                                        string formattednum = "";
                                        if (format2.EndsWith("00"))
                                        {
                                            formattednum = ((int)num).ToString();
                                            MessageUserControl.ShowInfo(itemname.Text + " refresh:", "The entered single item cost has a higher value than $" + formattednum); //{0:C}

                                        }
                                        else
                                        {
                                            MessageUserControl.ShowInfo(itemname.Text + " refresh:", "The entered single item cost has a higher value than $" + format2); //{0:C}

                                        }
                                    }
                                    else
                                    {
                                        //Reset ! ! if valid
                                        overcostindicator.Text = null;
                                    }

                                    //Recalculate unit cost and extended cost fields
                                    double calcunitcost = Math.Round((double.Parse(unitcost.Text)), 0);
                                    double orderedqty = double.Parse(qtyordered.Text);

                                    peritemcost.Text = "$" + string.Format("{0:#.00}", singleunitcost);
                                    extendedcost.Text = "$" + string.Format("{0:#.00}", calcunitcost * orderedqty);

                                    //Update Total Textboxes
                                    UpdateTotalAmounts();
                                }

                            }

                        }
                    }
                //}, "Refreshing Amounts", "Quantities have been refreshed");

            }
        }

        private void UpdateTotalAmounts()
        {
            double SubTotal = 0;
            double Tax = 0;

            //Get SubTotal for whole order
            foreach (GridViewRow row in OrderDetailItemsGV.Rows)
            {
                Label extendedbox = row.FindControl("ExtendedCostLabel") as Label;

                //Get number from $0.00
                string subtotalstring = extendedbox.Text;
                string subtotalnostring = string.Empty;
                for (int i = 0; i < subtotalstring.Length; i++)
                {
                    if (Char.IsDigit(subtotalstring[i]))
                    {
                        subtotalnostring += subtotalstring[i];
                    }
                }

                SubTotal += Convert.ToDouble(subtotalnostring);

            }

            //Calculate tax and total
            Tax = Math.Round(SubTotal * 0.05, 2);
            SubTotal = Math.Round(SubTotal, 2);
            double total = Math.Round(Tax + SubTotal, 2);

            //Change textboxes
            TaxTextBox.Text = "$" + string.Format("{0:#.00}", Tax / 100);
            SubTotalTextBox.Text = "$" + string.Format("{0:#.00}", SubTotal / 100);
            TotalTextBox.Text = "$" + string.Format("{0:#.00}", total / 100);
        }

        protected void OrderDetailItemsGV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void VendorCatalogGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int indextoadd = Convert.ToInt32(e.CommandArgument);
            List<ItemSummary> orderitems = new List<ItemSummary>();
            List<ItemSummary> catalogitems = new List<ItemSummary>();


            MessageUserControl.TryRun(() =>
            {
                foreach (GridViewRow row in VendorCatalogGV.Rows)
                {
                    //Grab all controls
                    ItemSummary item = new ItemSummary();
                    HiddenField productid = row.FindControl("ProductID") as HiddenField;
                    Label description = row.FindControl("Description") as Label;
                    Label reorderlevel = row.FindControl("ReOrderLevel") as Label;
                    Label instock = row.FindControl("InStock") as Label;
                    Label onorder = row.FindControl("OnOrder") as Label;
                    Label unitsize = row.FindControl("UnitSize") as Label;
                    HiddenField baseitemprice = row.FindControl("BaseItemPrice") as HiddenField;
                    HiddenField unitcost = row.FindControl("UnitCost") as HiddenField;

                    //Setting values
                    item.ProductID = int.Parse(productid.Value);
                    item.Description = description.Text;
                    item.ReOrderLevel = int.Parse(reorderlevel.Text);
                    item.QuantityOnHand = int.Parse(instock.Text);
                    item.QuantityOnOrder = int.Parse(onorder.Text);
                    item.SellingPrice = decimal.Parse(baseitemprice.Value);
                    item.UnitCost = decimal.Parse(unitcost.Value);

                    string unittext = unitsize.Text;

                    //getting values from text
                    if (unittext == "each")
                    {
                        item.OrderUnitSize = 1;
                    }
                    else
                    {
                        string unitsizenotext = string.Empty;
                        for (int i = 0; i < unittext.Length; i++)
                        {
                            if (Char.IsDigit(unittext[i]))
                            {
                                unitsizenotext += unittext[i];
                            }
                        }

                        item.OrderUnitSize = int.Parse(unitsizenotext);
                    }


                    //sorting items
                    if (row.RowIndex != indextoadd)
                    {
                        catalogitems.Add(item);
                    }
                    else
                    {
                        item.Quantity = 1;
                        orderitems.Add(item);
                    }
                }

                //for order details now
                foreach (GridViewRow row in OrderDetailItemsGV.Rows)
                {
                    //grab all controls
                    ItemSummary orderdetail = new ItemSummary();
                    HiddenField productid = row.FindControl("ProductID") as HiddenField;
                    HiddenField baseitemprice = row.FindControl("BaseItemPrice") as HiddenField;
                    HiddenField reorder = row.FindControl("ReOrderAmount") as HiddenField;
                    HiddenField instock = row.FindControl("InStock") as HiddenField;
                    HiddenField onorder = row.FindControl("OnOrder") as HiddenField;
                    Label description = row.FindControl("Description") as Label;
                    TextBox orderqty = row.FindControl("OrderQtyTextboxGV") as TextBox;
                    Label unitsize = row.FindControl("UnitSizeLabel") as Label;
                    TextBox unitcost = row.FindControl("UnitCostTextboxGV") as TextBox;


                    //Setting Values
                    orderdetail.ProductID = int.Parse(productid.Value);
                    orderdetail.SellingPrice = decimal.Parse(baseitemprice.Value);
                    orderdetail.ReOrderLevel = int.Parse(reorder.Value);
                    orderdetail.QuantityOnHand = int.Parse(instock.Value);
                    orderdetail.QuantityOnOrder = int.Parse(onorder.Value);
                    orderdetail.Description = description.Text;
                    orderdetail.Quantity = int.Parse(orderqty.Text);
                    orderdetail.UnitCost = decimal.Parse(unitcost.Text);

                    //Unit Size
                    //Checking for string
                    string unittext = unitsize.Text;

                    //getting values from text
                    if (unittext == "each")
                    {
                        orderdetail.OrderUnitSize = 1;
                    }
                    else
                    {
                        string unitsizenotext = string.Empty;
                        for (int i = 0; i < unittext.Length; i++)
                        {
                            if (Char.IsDigit(unittext[i]))
                            {
                                unitsizenotext += unittext[i];
                            }
                        }

                        orderdetail.OrderUnitSize = int.Parse(unitsizenotext);
                    }

                    orderitems.Add(orderdetail);
                }
            }, "Order Items", "Adding items from the catalog to the order");


            //Binding datasources
            OrderDetailItemsGV.DataSource = orderitems;
            OrderDetailItemsGV.DataBind();

            UpdateTotalAmounts();

            VendorCatalogGV.DataSource = catalogitems;
            VendorCatalogGV.DataBind();

        }

        protected void VendorCatalogGV_DataBinding(object sender, EventArgs e)
        {

        }

        protected void VendorCatalogGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //setting case text on rowbind
                Label unitsize = ((Label)e.Row.FindControl("UnitSize"));
                if (unitsize.Text == "1")
                {
                    unitsize.Text = "each";
                }
                else
                {
                    string formatted = "case (" + unitsize.Text + ")";
                    unitsize.Text = formatted;
                }
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            //Rebind gvs
            OrderDetailItemsGV.DataSource = null;
            VendorCatalogGV.DataSource = null;
            OrderDetailItemsGV.DataBind();
            VendorCatalogGV.DataBind();

            //Visibility of GVs
            OrderDetailItemsGV.Visible = false;
            VendorCatalogGV.Visible = false;

            //Reset all values
            VendorInfoLabel.Text = null;
            CommentTextBox.Text = null;
            OrderIDLabel.Text = null;
            TaxTextBox.Text = null;
            SubTotalTextBox.Text = null;
            TotalTextBox.Text = null;
            OrderInfoPanel.Visible = false;

            //Reset DDL
            VendorSelectionDDL.SelectedIndex = 0;
            VendorSelectionDDL.Enabled = true;

            //Reset buttons and hover style
            SelectVendor.Enabled = true;
            SelectVendor.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "hand";

            SaveOrder.Enabled = false;
            CancelOrder.Enabled = false;
            DeleteOrder.Enabled = false;
            PlaceOrder.Enabled = false;

            //resetting css styles
            SaveOrder.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";
            CancelOrder.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";
            DeleteOrder.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";
            PlaceOrder.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";


        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            //if orderid exits else clear all
            string orderlabeltext = OrderIDLabel.Text;
            if (!string.IsNullOrEmpty(orderlabeltext))
            {
                MessageUserControl.TryRun(() =>
                {
                    int orderid = int.Parse(OrderIDLabel.Text);
                    OrderController sysmgr = new OrderController();
                    sysmgr.DeleteAll(orderid);
                }, "Order deleted", "Deleted order");
            }
            else
            {
                MessageUserControl.ShowInfo("Order Delete", "Unsaved order has been removed");
            }
            Cancel_Click(sender, e);

        }

        protected void Save_Click(object sender, EventArgs e)
        {

            //button with validation
            bool valid = ValidateOrderItems();
            if (valid == true)
            {
                MessageUserControl.TryRun(() =>
                {
                    SendToBLL(1);
                    Cancel_Click(sender, e);
                },"Save Selected", "Order has been saved");
            }

        }

        private void SendToBLL(int type)
        {
            //Order
            //Order ID
            OrderInfo order = new OrderInfo();
            if (!string.IsNullOrEmpty(OrderIDLabel.Text))
            {
                order.OrderId = int.Parse(OrderIDLabel.Text);
            }


            order.VendorID = int.Parse(VendorSelectionDDL.SelectedValue);

            string comment = CommentTextBox.Text;
            order.Comment = comment;
            order.EmployeeID = int.Parse(EmployeeIDField.Text);

            //Tax amount
            string taxstring = TaxTextBox.Text;
            taxstring = taxstring.Remove(0, 1);
            order.TaxGST = decimal.Parse(taxstring);

            //SubTotal
            string subtotal = SubTotalTextBox.Text;
            subtotal = subtotal.Remove(0, 1);
            order.SubTotal = decimal.Parse(subtotal);

            //OrderItems
            List<ItemSummary> orderitems = new List<ItemSummary>();
            foreach (GridViewRow row in OrderDetailItemsGV.Rows)
            {
                ItemSummary item = new ItemSummary();
                HiddenField productid = row.FindControl("ProductID") as HiddenField;
                TextBox orderqty = row.FindControl("OrderQtyTextboxGV") as TextBox;
                Label unitsize = row.FindControl("UnitSizeLabel") as Label;
                TextBox unitcost = row.FindControl("UnitCostTextboxGV") as TextBox;
                item.ProductID = int.Parse(productid.Value);
                item.Quantity = int.Parse(orderqty.Text);

                //getting values from text
                string unitsizestring = unitsize.Text;
                string unitsizenotext = string.Empty;
                if (unitsizestring == "each")
                {
                    unitsizenotext = "1";
                }
                else
                {
                    for (int i = 0; i < unitsizestring.Length; i++)
                    {
                        if (Char.IsDigit(unitsizestring[i]))
                        {
                            unitsizenotext += unitsizestring[i];
                        }
                    }
                }

                item.OrderUnitSize = int.Parse(unitsizenotext);


                item.UnitCost = decimal.Parse(unitcost.Text);
                orderitems.Add(item);
            }

            //Sending it
            OrderController sysmgr = new OrderController();
            sysmgr.SaveAll(type, order, orderitems);
        }

        private bool ValidateOrderItems()
        {
            int count = 0;
            //Character limit on comment textbox
            if (CommentTextBox.Text.Length > 100)
            {
                MessageUserControl.ShowInfo("Comment Field", "Comment text is limited to 100 characters");
                count += 1;
            }

            List<ItemSummary> orderitems = new List<ItemSummary>();
            foreach (GridViewRow row in OrderDetailItemsGV.Rows)
            {
                Label itemname = row.FindControl("Description") as Label;
                TextBox qtyordered = row.FindControl("OrderQtyTextboxGV") as TextBox;
                Label unitsize = row.FindControl("UnitSizeLabel") as Label;
                TextBox unitcost = row.FindControl("UnitCostTextboxGV") as TextBox;
                HiddenField baseitemprice = row.FindControl("BaseItemPrice") as HiddenField;
                Label overcostindicator = row.FindControl("OverItemCostsLabel") as Label;
                Label peritemcost = row.FindControl("PerItemCostLabel") as Label;
                Label extendedcost = row.FindControl("ExtendedCostLabel") as Label;


                //Validating Qty Ordered
                if (string.IsNullOrEmpty(qtyordered.Text))
                {
                    MessageUserControl.ShowInfo(itemname.Text + " Validation:", "Enter a positive quantity value or remove the product");
                    count += 1;
                }
                else
                {
                    //Positive whole value for qty
                    double qtyordereddouble = double.Parse(qtyordered.Text);
                    bool iswhole = qtyordereddouble == (int)qtyordereddouble;
                    if (iswhole == false || qtyordereddouble < 0)
                    {
                        MessageUserControl.ShowInfo(itemname.Text + " Validation:", "A positive whole number must be entered for the quantity");
                        count += 1;
                    }
                    else
                    {
                        //Validating entered unit cost
                        if (string.IsNullOrEmpty(unitcost.Text))
                        {
                            MessageUserControl.ShowInfo(itemname.Text + " Validation:", "Enter a positive unit cost value or remove the product");
                            count += 1;
                        }
                        else
                        {
                            decimal unitcostdecimal = decimal.Parse(unitcost.Text);
                            //Checks for dollar amount (2 decimals)
                            if (Decimal.Round(unitcostdecimal, 2) != unitcostdecimal || unitcostdecimal < 0)
                            {
                                MessageUserControl.ShowInfo(itemname.Text + " Validation:", "Enter a valid positive dollar amount");
                                count += 1;
                            }
                            else
                            {

                                //Get unit size without string
                                string unitsizestring = unitsize.Text;
                                string unitsizenotext = string.Empty;
                                if (unitsizestring == "each")
                                {
                                    unitsizenotext = "1";
                                }
                                else
                                {
                                    for (int i = 0; i < unitsizestring.Length; i++)
                                    {
                                        if (Char.IsDigit(unitsizestring[i]))
                                        {
                                            unitsizenotext += unitsizestring[i];
                                        }
                                    }
                                }

                                //Check if single item cost is larger than base selling price
                                decimal singleunitcost = Math.Round(decimal.Parse(unitcost.Text) / decimal.Parse(unitsizenotext), 2);
                                peritemcost.Text = "$" + string.Format("{0:#.00}", singleunitcost);
                                extendedcost.Text = "$" + string.Format("{0:#.00}", unitcostdecimal * int.Parse(qtyordered.Text));
                                if (singleunitcost > decimal.Parse(baseitemprice.Value))
                                {
                                    double num = double.Parse(baseitemprice.Value);
                                    var format2 = string.Format("{0:0.00}", num);
                                    string formattednum = "";
                                    if (format2.EndsWith("00"))
                                    {
                                        formattednum = ((int)num).ToString();
                                        MessageUserControl.ShowInfo(itemname.Text + " Validation:", "The calculated single item cost has a higher value than $" + formattednum); //{0:C}
                                        count += 1;
                                    }
                                    else
                                    {
                                        MessageUserControl.ShowInfo(itemname.Text + " Validation:", "The calculated single item cost has a higher value than $" + format2); //{0:C}
                                        count += 1;
                                    }
                                    overcostindicator.Text = "! !";
                                    //Style !!
                                    overcostindicator.ForeColor = System.Drawing.Color.Red;
                                    overcostindicator.Font.Bold = true;
                                }
                                else
                                {
                                    overcostindicator.Text = null;
                                }
                                UpdateTotalAmounts();
                            }
                        }
                    }

                }
            }

            //If more than one error count return false
            if (count > 0)
            {
                return false;
            }
            return true;
        }

        protected void PlaceOrder_Click(object sender, EventArgs e)
        {
            //button with validation
            bool valid = ValidateOrderItems();
            if (valid == true)
            {
                MessageUserControl.TryRun(() =>
                {
                    SendToBLL(2);
                    Cancel_Click(sender, e);
                }, "Place Order Selected", "Order has been placed");
            }
        }
    }
}