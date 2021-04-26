using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using eRaceProject.Security;
using eRaceSystem.BLL;
using eRaceSystem.BLL.Sales;
using eRaceSystem.ViewModels;
#endregion

namespace eRaceProject.Subsystem
{
    public partial class Sales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Security
            if (Request.IsAuthenticated)
            {

                if (User.IsInRole("Clerk"))
                {

                    //obtain the CustomerId on the security User record
                    SecurityController ssysmgr = new SecurityController();
                    int? employeeid = ssysmgr.GetCurrentUserClerkID(User.Identity.Name);

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
                            LoggedUser.Text = item.LastName + ", " + item.FirstName;
                            EmployeeID.Text = empid.ToString();
                            //tempEmployeeID = empid;
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
                MessageUserControl.ShowInfo("Success", "Invoice has been added.");
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
                MessageUserControl.ShowInfo("Success", "Invoice has been updated.");
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
                MessageUserControl.ShowInfo("Success", "Invoice has been removed.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }



        #endregion


        //Clears cart might want in in the page load
        protected void ClearCartButton_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {
                CategoryProductDDL.SelectedValue = "0";
                //Clear out values
                QuantityText.Text = "1";
                CartGridView.DataSource = null;
                CartGridView.DataBind();
                SubtotalTextBox.Text = "0.00";
                TaxTextBox.Text = "0.00";
                TotalTextBox.Text = "0.00";
            },"Shopping Cart", "Cart has been cleared, please select a new product");  

        }

        //Add product to cart
        protected void AddToCartButton_Click(object sender, EventArgs e)
        {
            
            //Validation
            if (int.Parse(CategoryProductDDL.SelectedValue) == 0)
            {
                MessageUserControl.ShowInfo("Category", "Select a category to pick");
            }
            else if (QuantityText.Text.Contains("."))
            {
                MessageUserControl.ShowInfo("Quantity", "Product quantity cannot have decimals");
            }
            else if(string.IsNullOrEmpty(QuantityText.Text)) //quantity cannot be empty
            {
                MessageUserControl.ShowInfo("Quantity", "Product quantity must not be empty, defaulting to 1 quantity");
                QuantityText.Text = "1";
            }
            else if (int.Parse(QuantityText.Text) <= 0) //quantity cannot be less than 0
            {
                //throw new Exception("Please enter a quantity of products");
                MessageUserControl.ShowInfo("Quantity", "Product quantity must be greater than 0");
            }
            else
            {

                //create new instance of controller
                SalesController info = new SalesController();
                //create new instance of ShoppingCartList viewmodel
                
                List<ShoppingCartList> shoppingcartlist = new List<ShoppingCartList>();

                int tempProductid = 0; //used to hold productid already in the list
                //Must dump the data currently in the cartgridview into the shoppingcartlist
                foreach (GridViewRow row in CartGridView.Rows)
                {
                    ShoppingCartList existingcartitem = new ShoppingCartList();
                    var productidlabel = row.FindControl("ProductID") as Label;
                    var productname = row.FindControl("ProductName") as Label;
                    var quantity = row.FindControl("Quantity") as TextBox;
                    var price = row.FindControl("Price") as Label;

                    existingcartitem.ProductID = int.Parse(productidlabel.Text);
                    tempProductid = int.Parse(productidlabel.Text);
                    existingcartitem.ProductName = productname.Text;
                    existingcartitem.Quantity = int.Parse(quantity.Text);
                    existingcartitem.Price = decimal.Parse(price.Text);
                    shoppingcartlist.Add(existingcartitem);

                }

                //adding a new item/product into the cart
                ShoppingCartList newCartItem = new ShoppingCartList();
                newCartItem.ProductID = int.Parse(ProductDDL.SelectedValue); //ProductID 
                int productid = int.Parse(ProductDDL.SelectedValue); //to get Price of item
                newCartItem.ProductName = info.Get_ItemName(productid);
                newCartItem.Quantity = int.Parse(QuantityText.Text); //quantity from textbox
                decimal unitprice = info.Get_UnitPrice(productid);
                newCartItem.Price = decimal.Parse(unitprice.ToString("0.00"));

                //if existingcartitem ProductID equals the new cartitem Product ID error dont add,
                //  otherwise add new cartitem to the list
                if (tempProductid == newCartItem.ProductID)
                {
                    MessageUserControl.ShowInfo("Product", "You cannot add a product that already exist. Re-enter quantity in the cart and refresh");
                }
                else
                {
                    MessageUserControl.TryRun(() =>
                    {
                        shoppingcartlist.Add(newCartItem); //add item into the shoppingcart
                    }, "Adding Product", "Product has been added to the cart");
                }


                //datasource, databind shoppingcartlist
                CartGridView.DataSource = shoppingcartlist;
                CartGridView.DataBind();


                decimal subtotal = 0; //declaring a subtotal to save item.Amount
                decimal tax = 0; //declaring a new tax to 
                foreach (ShoppingCartList item in shoppingcartlist)
                {
                    subtotal += item.Amount;
                    tax = subtotal * 0.05M;
                    SubtotalTextBox.Text = subtotal.ToString("0.00");
                    TaxTextBox.Text = tax.ToString("0.00");
                    TotalTextBox.Text = (subtotal + tax).ToString("0.00");
                }

            }
        }

        //Removing product from the cart
        protected void CartGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //string itemname = "";
            MessageUserControl.TryRun(() =>
            {

                int index = Convert.ToInt32(e.CommandArgument); //Finds index in the gridview
                GridViewRow gridViewRow = CartGridView.Rows[index]; //sets index to find which row

                List<ShoppingCartList> shoppingcartlist = new List<ShoppingCartList>();
                foreach (GridViewRow row in CartGridView.Rows)
                {
                    ShoppingCartList existingcartitem = new ShoppingCartList();
                    var productidlabel = row.FindControl("ProductID") as Label;
                    var productname = row.FindControl("ProductName") as Label;
                    //itemname = (row.FindControl("ProductName") as Label).Text;
                    var quantity = row.FindControl("Quantity") as TextBox;
                    var price = row.FindControl("Price") as Label;

                    existingcartitem.ProductID = int.Parse(productidlabel.Text);
                    existingcartitem.ProductName = productname.Text;
                    existingcartitem.Quantity = int.Parse(quantity.Text);
                    existingcartitem.Price = decimal.Parse(price.Text);
                    shoppingcartlist.Add(existingcartitem);
                

                }
                shoppingcartlist.RemoveAt(index); //remove row from the index that it was selected from 
                //datasource databind new shoppingcartlist to get "new" list
                CartGridView.DataSource = shoppingcartlist; 
                CartGridView.DataBind();

                decimal subtotal = 0; //declaring a subtotal to save item.Amount
                decimal tax = 0; //declaring a new tax to 
                foreach (ShoppingCartList item in shoppingcartlist)
                {
                    subtotal += item.Amount;
                    tax = subtotal * 0.05M;
                    SubtotalTextBox.Text = subtotal.ToString("0.00");
                    TaxTextBox.Text = tax.ToString("0.00");
                    TotalTextBox.Text = (subtotal + tax).ToString("0.00");
                }
                if(CartGridView.Rows.Count == 0)
                {
                    SubtotalTextBox.Text = "0.00";
                    TaxTextBox.Text = "0.00";
                    TotalTextBox.Text = "0.00";
                    
                }

                //}, "Removing Product", $"{itemname} has been removed from the cart");
            }, "Removing Product", "Product has been removed from the cart");
        }

        //Refresh row and update the quantity of the price and amount
        protected void Refresh_Click(object sender, EventArgs e)
        {
            //Updates the quantity in the row
            //string itemname = "";
            MessageUserControl.TryRun(() =>
            {

                List<ShoppingCartList> updateQuantity = new List<ShoppingCartList>(); //list to 
                foreach (GridViewRow row in CartGridView.Rows)
                {
                    //ShoppingCartList existingcartitem = new ShoppingCartList();
                    var productidlabel = row.FindControl("ProductID") as Label;
                    var productname = row.FindControl("ProductName") as Label;
                    //itemname = (row.FindControl("ProductName") as Label).Text;
                    var quantity = row.FindControl("Quantity") as TextBox;
                    var price = row.FindControl("Price") as Label;
                    
                    if (productidlabel != null) //if there is a product in the gridview add it to the list
                    {
                        var updateItem = new ShoppingCartList();
                        updateItem.ProductID = int.Parse(productidlabel.Text);
                        updateItem.ProductName = productname.Text;
                        updateItem.Quantity = int.Parse(quantity.Text);
                        updateItem.Price = decimal.Parse(price.Text);
                        updateQuantity.Add(updateItem);
                    }
                }

                CartGridView.DataSource = updateQuantity;
                CartGridView.DataBind();

                decimal subtotal = 0; //declaring a subtotal to save item.Amount
                decimal tax = 0; //declaring a new tax to 
                foreach (ShoppingCartList item in updateQuantity)
                {
                    subtotal += item.Amount;
                    tax = subtotal * 0.05M;
                    SubtotalTextBox.Text = subtotal.ToString("0.00");
                    TaxTextBox.Text = tax.ToString("0.00");
                    TotalTextBox.Text = (subtotal + tax).ToString("0.00");
                }
                if (CartGridView.Rows.Count == 0)
                {
                    SubtotalTextBox.Text = "0.00";
                    TaxTextBox.Text = "0.00";
                    TotalTextBox.Text = "0.00";

                }

                //}, "Product", $"{itemname} has now been updated");
            }, "Product", "Product has now been updated");

        }

        //Payment
        protected void PaymentButton_Click(object sender, EventArgs e)
        {
            //collect subtotal, tax, total, and items and gridview to send to BLL for processing
            MessageUserControl.TryRun(() =>
            {
                int employeeid = int.Parse(EmployeeID.Text);
                int NewInvoice = 0; //will be used to store the new Invoice
                SalesController info = new SalesController();

                List<ShoppingCartList> cartList = new List<ShoppingCartList>();
                //collecting the rows in the gridview
                foreach (GridViewRow row in CartGridView.Rows)
                {
                    var productidlabel = row.FindControl("ProductID") as Label;
                    var productname = row.FindControl("ProductName") as Label;
                    var quantity = row.FindControl("Quantity") as TextBox;
                    var price = row.FindControl("Price") as Label;

                    if (productidlabel != null) //if there is a product in the gridview add it to the list
                    {
                        var existingitem = new ShoppingCartList();
                        existingitem.ProductID = int.Parse(productidlabel.Text);
                        existingitem.ProductName = productname.Text;
                        existingitem.Quantity = int.Parse(quantity.Text);
                        existingitem.Price = decimal.Parse(price.Text);
                        cartList.Add(existingitem);
                    }
                }

                //Create a new Invoice by calling BLL method //error breaking in NewInvoice
                NewInvoice = info.CreateInvoice(employeeid, 
                                                decimal.Parse(SubtotalTextBox.Text),
                                                decimal.Parse(TaxTextBox.Text),
                                                decimal.Parse(TotalTextBox.Text),
                                                cartList);

                //Display the new Invoice that was made by the user
                InvoiceTextBox.Text = NewInvoice.ToString();

            }, "Payment", $"Invoice {InvoiceTextBox.Text} has now been created");
        }

        protected void LinkToRefund_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Subsystem/Sales/SalesRefund.aspx");
        }
    }
}