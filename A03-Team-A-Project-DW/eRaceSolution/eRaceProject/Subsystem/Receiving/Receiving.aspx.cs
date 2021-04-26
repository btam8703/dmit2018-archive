using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using eRaceProject.Security;
using eRaceSystem.BLL;
using eRaceSystem.BLL.Receiving;
using eRaceSystem.ViewModels;
using eRaceSystem.ViewModels.ReceivingModels; 
#endregion

namespace eRaceProject.Subsystem
{
    public partial class Receiving : System.Web.UI.Page
    {
        public int TempList { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ReceivingBindOrderList();

            }

            #region Security
            if (Request.IsAuthenticated)
            {
     
                if (User.IsInRole("FoodService"))
                {
                    
                    //obtain the CustomerId on the security User record
                    SecurityController ssysmgr = new SecurityController();
                    int? employeeid = ssysmgr.GetCurrentUserFoodServiceID(User.Identity.Name);

                    //need to convert the int? to an int for the call to the CustomerController method
                    //int custid = customerid == null ? default(int) : int.Parse(customerid.ToString());
                    int empid = employeeid ?? default(int);

                    MessageUserControl.TryRun(() => {
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
                        }
                    });

                }
                else if (User.IsInRole("Clerk"))
                {

                    //obtain the CustomerId on the security User record
                    SecurityController ssysmgr = new SecurityController();
                    int? employeeid = ssysmgr.GetCurrentUserClerkID(User.Identity.Name);

                    //need to convert the int? to an int for the call to the CustomerController method
                    //int custid = customerid == null ? default(int) : int.Parse(customerid.ToString());
                    int empid = employeeid ?? default(int);

                    MessageUserControl.TryRun(() => {
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
                MessageUserControl.ShowInfo("Success", " add message here");
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
                MessageUserControl.ShowInfo("Success", "add message here");
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
                MessageUserControl.ShowInfo("Success", "Add message here.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }
        #endregion
        //drop down list
        #region DropDownList
        protected void ReceivingBindOrderList()
        {
            ReceivingOrderController sysmgr = new ReceivingOrderController();
            List<ReceivingOrderList> info = sysmgr.ReceivingOrder_List();

            ReceivingOrderListDropDown.DataSource = info;
            ReceivingOrderListDropDown.DataTextField = nameof(ReceivingOrderList.DisplayText);
            ReceivingOrderListDropDown.DataValueField = nameof(ReceivingOrderList.ValueId);
            ReceivingOrderListDropDown.DataBind();

            ReceivingOrderListDropDown.Items.Insert(0, new ListItem("select an order", "0"));
            //DisplayVendorName.Text = "None";
        }
        #endregion
        //'open' button code 
        #region Open Button - Display Vendor
        protected void ReceivingSearchOrder_Click(object sender, EventArgs e)
        {
            if (ReceivingOrderListDropDown.SelectedIndex==0)
            {
                //Display Vendor
                MessageLabel.Text = "No Order Selected";
                DisplayVendor.DataSource = null;
                DisplayVendor.DataBind();

                //Display Order Details
                Display_Order.DataSource = null;
                Display_Order.DataBind();
                
            }
            else
            {
                MessageLabel.Text = "";
                
                //Display Vendor
                ReceivingOrderController sysmgr = new ReceivingOrderController();
                List<ReceivingVendorDetail> info = sysmgr.Orders_FindVendorbyID(int.Parse(ReceivingOrderListDropDown.SelectedValue));
                DisplayVendor.DataSource = info;
                DisplayVendor.DataBind();

                //Display Order Details
               
                List<ReceivingOrderDetails> info2 = sysmgr.Order_FindOrderbyID(int.Parse(ReceivingOrderListDropDown.SelectedValue));
                Display_Order.DataSource = info2;
                Display_Order.DataBind();

            }

        }
       protected void DisplayVendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow agvrow = DisplayVendor.Rows[DisplayVendor.SelectedIndex];
           MessageLabel.Text = (agvrow.FindControl("OrderId") as Label).Text;
            

        }

        protected void Display_Order_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GridViewRow selectRow = Display_Order.SelectedRow;
            //String receivedUnits = selectRow.Cells[2].Text;
           


        }
        #endregion



        #region Open Button - Display Order

        #endregion
    }




}