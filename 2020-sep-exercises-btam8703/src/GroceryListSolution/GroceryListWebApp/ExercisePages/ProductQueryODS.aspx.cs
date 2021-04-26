using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using GroceryListSystem.BLL;
using GroceryListSystem.ViewModels;

namespace GroceryListWebApp.ExercisePages
{
    public partial class ProductQueryODS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MessageLabel.Text = "";
            if (!Page.IsPostBack)
            {
                ProductGridview.Visible = false;
                BackButton.Visible = false;
            }
            else
            {
                ProductGridview.Visible = true;
                BackButton.Visible = true;

            }
        }

        protected void SearchProducts_Click(object sender, EventArgs e)
        {
            if (CategoryDropDownList.SelectedIndex == 0)
            {
                MessageLabel.Text = "Select a Category for search.";
                ProductGridview.Visible = false;
                BackButton.Visible = false;
            }
            else
            {
                ProductGridview.Visible = true;
                BackButton.Visible = true;
            }

        }

        protected void BackButton_Click(object sender, EventArgs e)
        {
            CategoryDropDownList.SelectedIndex = 0;
            ProductGridview.Visible = false;
            BackButton.Visible = false;
        }

        protected void SelectCheckForException(object sender,
                                       ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }
      
    }
}