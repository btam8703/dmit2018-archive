using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GroceryListWebApp.ExercisePages
{
    public partial class ProductCRUD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

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
                MessageUserControl.ShowInfo("Success", "Product has been added.");
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
                MessageUserControl.ShowInfo("Success", "Product has been updated.");
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
                MessageUserControl.ShowInfo("Success", "Product has been removed.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }

        protected void ProductListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}