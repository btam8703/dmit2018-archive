using ScheduleStarterKit.BLL;
using ScheduleStarterKit.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp.Exercises
{
    public partial class ContractEditor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        #region Student Code - Free to modify
        protected void ClientDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ClientDropDown.SelectedIndex > 0)
            {
                MessageUserControl.TryRun(() =>
                {
                    var controller = new ContractController();
                    var info = controller.GetClientContracts(int.Parse(ClientDropDown.SelectedValue));
                    string contact = $"{info.Contact} ({info.Phone})";
                    string url = $"~/Images/{info.Contact.Replace(" ", string.Empty)}.jpg";
                    SetClient(info.Name, info.Address, contact, url, info.Contracts);


                    //binds all contracts to the gridview
                    ClientContracts.DataSource = info.Contracts;
                    ClientContracts.DataBind();
                    NewContract.Visible = true;
                    ClientContracts.Visible = true;

                    
                }, "Client", "View client info and contracts");
            }
            else
            {
                //if index is zero do some visibility stuff
                NewContract.Visible = false;
                SetClient(string.Empty, string.Empty, string.Empty, string.Empty, null);
                ClientContracts.Visible = false;
            }

            AddEditPanel.Visible = false;
            SaveContract.Text = "";
            ContractTitle.Text = "";
            Requirements.Text = "";
            FromDate.Text = "";
            ToDate.Text = "";
            SaveContract.Text = "Save (New)";


            //reset data on gridviews
            AvailableSkills.DataSource = null;
            AvailableSkills.DataBind();

            RequiredSkills.DataSource = null;
            RequiredSkills.DataBind();
        }

        private void SetClient(string name, string address, string contact, string imageUrl, IEnumerable<ContractInfo> clientContractData )
        {
            ClientName.Text = name;
            ClientAddress.Text = address;
            ClientContact.Text = contact;
            ClientImage.ImageUrl = imageUrl;
        }
        #endregion

        protected void NewContract_Click(object sender, EventArgs e)
        {
            //more visibility stuff
            MessageUserControl.TryRun(() =>
            {
                SaveContract.Text = "";
                ContractTitle.Text = "";
                Requirements.Text = "";
                FromDate.Text = "";
                ToDate.Text = "";
                SaveContract.Text = "Save";
                AddEditPanel.Visible = true;

                ContractController sysmgr = new ContractController();
                List<SkillSummary> skillsAvailable = sysmgr.ListSkills();
                AvailableSkills.DataSource = skillsAvailable;
                AvailableSkills.DataBind();

                RequiredSkills.DataSource = null;
                RequiredSkills.DataBind();
            }, "Contract", "Add new contract");
        }

        protected void SaveContract_Click(object sender, EventArgs e)
        {
            
   
            MessageUserControl.TryRun(() =>
            {
                Contract ContractNewPageInfo = new Contract();
                if (SaveContract.Text.Contains("#"))
                {
                    int index = SaveContract.Text.IndexOf("#");
                    string contractid = SaveContract.Text.Substring(index + 1);
                    ContractNewPageInfo.CurrentContractId = int.Parse(contractid);
                }
                ContractNewPageInfo.ClientLocationId = ClientDropDown.SelectedIndex;
                ContractNewPageInfo.Title = ContractTitle.Text;
                ContractNewPageInfo.Requirements = Requirements.Text;

                Duration contractduration = new Duration();
                contractduration.To = DateTime.Parse(ToDate.Text);
                contractduration.From = DateTime.Parse(FromDate.Text);
                ContractNewPageInfo.ContractPeriod = contractduration;

                List<SkilledWorkerCount> skillsReq = new List<SkilledWorkerCount>();
                foreach (GridViewRow row in RequiredSkills.Rows)
                {
                    SkilledWorkerCount skill = new SkilledWorkerCount();
                    var skillIdreq = row.FindControl("SkillId") as Label;
                    var required = row.FindControl("Required") as TextBox;

                    skill.SkillId = int.Parse(skillIdreq.Text);
                    skill.WorkerCount = byte.Parse(required.Text);
                    skillsReq.Add(skill);
                }

                ContractNewPageInfo.WorkerSkills = skillsReq;
                ContractController sysmgr = new ContractController();
                sysmgr.Save(ContractNewPageInfo);
            },"Contract", "Save contract");

        }

        private void RetrieveContractDetails()
        {

        }

        protected void ClientContracts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           

            MessageUserControl.TryRun(() =>
            {
                //grab index of table row using the label of ContractID
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gridViewRow = ClientContracts.Rows[index];
                int contractid = int.Parse((gridViewRow.FindControl("ContractID") as Label).Text);

                //get contract info
                ContractController sysmgr = new ContractController();
                var info = sysmgr.GetContractInfo(contractid);

                if (info !=null)
                {
                    //fill in text boxes
                    AddEditPanel.Visible = true;
                    SaveContract.Text = "Save #" + contractid.ToString() + "";
                    ContractTitle.Text = info.Title;
                    Requirements.Text = info.Requirements;
                    FromDate.Text = info.Duration.From.ToString("yyyy-MM-dd");
                    ToDate.Text = info.Duration.To.ToString("yyyy-MM-dd");


                    //loadingrequiredskills
                    List<SkillSummary> skillsRequired = sysmgr.ListContractSkills(contractid);
                    if (skillsRequired.Count > 0)
                    {
                        RequiredSkills.DataSource = skillsRequired;
                        RequiredSkills.DataBind();
                    }

                    //loading available skills
                    List<SkillSummary> skillsAvailable = sysmgr.ListSkills();
                    if (skillsAvailable.Count > 0)
                    {
                        //if there are no required skills 
                        if (skillsRequired.Count == 0)
                        {
                            AvailableSkills.DataSource = skillsAvailable;
                            AvailableSkills.DataBind();
                        }
                        else
                        {
                            //if some skills are required already in the database
                            //need to filter out both
                            IEnumerable<SkillSummary> filteredAvailableSkills = skillsAvailable.Except(skillsRequired);
                            AvailableSkills.DataSource = filteredAvailableSkills;
                            AvailableSkills.DataBind();
                        }
                    }
                }
                else if (info == null)
                {
                    AddEditPanel.Visible = false;
                    ClientDropDown_SelectedIndexChanged(sender, new EventArgs());
                    throw new Exception("Contract not found");
                }
               
            }, "Contract", "View contract info");
            
        }

        protected void ClientContracts_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void AvailableSkills_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {
                int indexavailable = Convert.ToInt32(e.CommandArgument);
                ContractController sysmgr = new ContractController();
                List<SkillSummary> skillsAvailable = new List<SkillSummary>();
                List<SkillSummary> skillsReq = new List<SkillSummary>();
                foreach (GridViewRow row in AvailableSkills.Rows)
                {

                    SkillSummary skill = new SkillSummary();
                    var skillId = row.FindControl("SkillId") as Label;
                    var description = row.FindControl("Description") as Label;
                    var candidatecount = row.FindControl("CandidateCount") as Label;
                    var reqticket = row.FindControl("Ticket") as CheckBox;

                    skill.SkillId = int.Parse(skillId.Text);
                    skill.Description = description.Text;
                    skill.CandidateCount = int.Parse(candidatecount.Text);
                    skill.RequiresTicket = reqticket.Checked;

                    if (row.RowIndex != indexavailable)
                    {
                        skillsAvailable.Add(skill);
                    }
                    else
                    {
                        skill.Required = 0;
                        skillsReq.Add(skill);
                    }
                }

                foreach (GridViewRow row in RequiredSkills.Rows)
                {
                    SkillSummary skill = new SkillSummary();
                    var skillIdreq = row.FindControl("SkillId") as Label;
                    var description = row.FindControl("Description") as Label;
                    var candidatecount = row.FindControl("CandidateCount") as Label;
                    var reqticket = row.FindControl("Ticket") as CheckBox;
                    var required = row.FindControl("Required") as TextBox;

                    skill.SkillId = int.Parse(skillIdreq.Text);
                    skill.Description = description.Text;
                    skill.CandidateCount = int.Parse(candidatecount.Text);
                    skill.RequiresTicket = reqticket.Checked;
                    skill.Required = byte.Parse(required.Text);
                    skillsReq.Add(skill);
                }

                AvailableSkills.DataSource = skillsAvailable;
                AvailableSkills.DataBind();

                RequiredSkills.DataSource = skillsReq;
                RequiredSkills.DataBind();

            }, "Available Skills", "Add skills");
            
        }

        protected void AvailableSkills_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void RequiredSkills_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            MessageUserControl.TryRun(() =>
            {
                int requiredindex = Convert.ToInt32(e.CommandArgument);
                ContractController sysmgr = new ContractController();
                List<SkillSummary> skillsAvailable = new List<SkillSummary>();
                List<SkillSummary> skillsReq = new List<SkillSummary>();

                foreach (GridViewRow row in RequiredSkills.Rows)
                {
                    SkillSummary skill = new SkillSummary();
                    var skillId = row.FindControl("SkillId") as Label;
                    var description = row.FindControl("Description") as Label;
                    var candidatecount = row.FindControl("CandidateCount") as Label;
                    var reqticket = row.FindControl("Ticket") as CheckBox;
                    var required = row.FindControl("Required") as TextBox;


                    skill.SkillId = int.Parse(skillId.Text);
                    skill.Description = description.Text;
                    skill.CandidateCount = int.Parse(candidatecount.Text);
                    skill.RequiresTicket = reqticket.Checked;
                    skill.Required = byte.Parse(required.Text);

                    if (row.RowIndex != requiredindex)
                    {
                        skill.Required = byte.Parse(required.Text);
                        skillsReq.Add(skill);
                    }
                    else
                    {
                        skillsAvailable.Add(skill);
                    }
                }

                foreach (GridViewRow row in AvailableSkills.Rows)
                {
                    SkillSummary skill = new SkillSummary();
                    var skillIdava = row.FindControl("SkillId") as Label;
                    var description = row.FindControl("Description") as Label;
                    var candidatecount = row.FindControl("CandidateCount") as Label;
                    var reqticket = row.FindControl("Ticket") as CheckBox;

                    skill.SkillId = int.Parse(skillIdava.Text);
                    skill.Description = description.Text;
                    skill.CandidateCount = int.Parse(candidatecount.Text);
                    skill.RequiresTicket = reqticket.Checked;
                    skillsAvailable.Add(skill);
                }

                AvailableSkills.DataSource = skillsAvailable;
                AvailableSkills.DataBind();

                RequiredSkills.DataSource = skillsReq;
                RequiredSkills.DataBind();

            }, "Required Skills", "Remove skills");
        }
    }
}