using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMIT2018Common.UserControls;

#region Additional Namespaces
using eRaceProject.Security;
using eRaceSystem.BLL;
using eRaceSystem.BLL.Sales;
using eRaceSystem.ViewModels;
#endregion

namespace eRaceProject.Subsystem
{
    public partial class SalesRefund : System.Web.UI.Page
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

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            //clear form fields
            InvoiceLookupTextBox.Text = "";
            InvoiceTextBox.Text = "";
            InvoiceDetailsGridView.DataSource = null;
            InvoiceDetailsGridView.DataBind();
            SubtotalTextBox.Text = "0.00";
            TaxTextBox.Text = "0.00";
            TotalTextBox.Text = "0.00";
        }

        protected void FindInvoiceButton_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(InvoiceLookupTextBox.Text))
            {
                MessageUserControl.ShowInfo("Find Invoice", "Please input an existing Invoice#");
            }
            else if (InvoiceLookupTextBox.Text.Length > 4)
            {
                MessageUserControl.ShowInfo("Find Invoice", "No invoice generated, please put a valid Invoice#");
            }
            else
            {
                int invoicenumber = int.Parse(InvoiceLookupTextBox.Text);
                var info = new RefundController();
                MessageUserControl.TryRun(() =>
                {
                    List<ReturnInvoiceDetails> invoicedetailsList = info.Get_InvoiceDetails(invoicenumber);
                    //datasource databind the invoicedetailList source
                    InvoiceDetailsGridView.DataSource = invoicedetailsList;
                    InvoiceDetailsGridView.DataBind();
                }, "Invoice found", "Now fetching invoice details...");
            }

            SubtotalTextBox.Text = "0.00";
            TaxTextBox.Text = "0.00";
            TotalTextBox.Text = "0.00";
        }

        protected void RefundReasonCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //create subtotal, tax and total
            decimal productSubtotal = 0.00M;
            decimal productTax = 0.00M;
            decimal productTotal = 0.00M;
            decimal GST = 0.05M;
            foreach (GridViewRow row in InvoiceDetailsGridView.Rows)
            {
                //if the refundreason is checked provice a reasoning in textbox
                bool refundreasonChecked = (row.FindControl("RefundReasonCheckBox") as CheckBox).Checked;
                if (refundreasonChecked)
                {
                    var productAmount = row.FindControl("Amount") as Label;
                    var productRestock = row.FindControl("RestockCharge") as Label;
                    productSubtotal += (decimal.Parse(productAmount.Text) - decimal.Parse(productRestock.Text));
                    productTax = productSubtotal * GST;
                    productTotal = (productTax + productSubtotal) * -1;
                }
            }

            SubtotalTextBox.Text = productSubtotal.ToString("0.00");
            TaxTextBox.Text = productTax.ToString("0.00");
            TotalTextBox.Text = (productTotal).ToString("0.00");

        }

        protected void RefundButton_Click(object sender, EventArgs e)
        {
            List<ReturnedProducts> returnedProducts = new List<ReturnedProducts>();

           
                int invoiceNumber = int.Parse(InvoiceLookupTextBox.Text);
                int employeeid = int.Parse(EmployeeID.Text);
                decimal subtotal = decimal.Parse(SubtotalTextBox.Text);
                decimal tax = decimal.Parse(TaxTextBox.Text);
                decimal total = decimal.Parse(TotalTextBox.Text);

                foreach (GridViewRow row in InvoiceDetailsGridView.Rows)
                {
                    bool refundreasonChecked = (row.FindControl("RefundReasonCheckBox") as CheckBox).Checked;
                    if (refundreasonChecked)
                    {
                        var existingProductid = row.FindControl("ProductID") as Label;
                        var existingQuantity = row.FindControl("Quantity") as TextBox;
                        var existingReason = row.FindControl("RefundReasonTextBox") as Label;
                        ReturnedProducts newReturnedItems = new ReturnedProducts();
                        newReturnedItems.InvoiceID = int.Parse(InvoiceLookupTextBox.Text);
                        newReturnedItems.ProductID = int.Parse(existingProductid.Text);
                        newReturnedItems.Quantity = int.Parse(existingQuantity.Text);
                        newReturnedItems.Reason = existingProductid.Text;

                        returnedProducts.Add(newReturnedItems); //add wanted returned items to list
                    }
                }

                MessageUserControl.TryRun(() =>
                {
                    RefundController info = new RefundController();
                    int newInvoicenumber = info.ReturnProducts(employeeid, invoiceNumber, subtotal, tax, total, returnedProducts);
                    InvoiceTextBox.Text = newInvoicenumber.ToString();

                }, "Refund Button", "Product(s) has been refunded");
        }
    }
}