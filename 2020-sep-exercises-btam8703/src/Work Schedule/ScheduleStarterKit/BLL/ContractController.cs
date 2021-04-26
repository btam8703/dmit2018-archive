using DMIT2018Common.UserControls;
using ScheduleStarterKit.DAL;
using ScheduleStarterKit.Entities;
using ScheduleStarterKit.ViewModels;
using ScheduleStarterKit.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleStarterKit.BLL
{
    [DataObject]
    public class ContractController
    {
        private List<string> errors = new List<string>();

        #region Query Methods
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<KeyValueOption<int>> ListClients()
        {
            using (var context = new WorkScheduleContext())
            {
                var result = context.Locations.Select(x => new KeyValueOption<int> { Key = x.LocationID, DisplayText = x.Name });
                return result.ToList();
            }
        }

        public ClientLocation GetClientContracts(int locationId)
        {
            using (var context = new WorkScheduleContext())
            {
                var result = from place in context.Locations
                             where place.LocationID == locationId
                             select new ClientLocation
                             {
                                 Name = place.Name,
                                 Address = place.Street + " " + place.City + ", " + place.Province,
                                 Phone = place.Phone,
                                 Contact = place.Contact,
                                 Contracts = from contract in place.PlacementContracts
                                             select new ContractInfo
                                             {
                                                 ContractId = contract.PlacementContractID,
                                                 Title = contract.Title,
                                                 Requirements = contract.Requirements,
                                                 Duration = new Duration
                                                 {
                                                     From = contract.StartDate,
                                                     To = contract.EndDate
                                                 }
                                             }
                             };
                return result.SingleOrDefault();
            }
        }

        public ContractInfo GetContractInfo(int contractId)
        {
            #region TODO: Student Code Here
            using (var context = new WorkScheduleContext())
            {
                var result = (from contract in context.PlacementContracts
                             where contract.PlacementContractID == contractId
                             select new ContractInfo
                             {
                                 ContractId = contract.PlacementContractID,
                                 Title = contract.Title,
                                 Duration = new Duration
                                 {
                                     From = contract.StartDate,
                                     To = contract.EndDate
                                 },
                                 Requirements = contract.Requirements,
                             }).FirstOrDefault();
                return result;
            }
            #endregion
        }

        public List<SkillSummary> ListContractSkills(int contractId)
        {
            #region TODO: Student Code Here
            using (var context = new WorkScheduleContext())
            {
                var result = from skill in context.ContractSkills
                             where skill.ContractID == contractId
                             select new SkillSummary
                             {
                                 SkillId = skill.SkillID,
                                 Description = skill.Skill.Description,
                                 RequiresTicket = skill.Skill.RequiresTicket,
                                 CandidateCount = skill.Skill.EmployeeSkills.Count(),
                                 Required = skill.NumberOfEmployees
                             };
                return result.ToList();
            }
            #endregion
        }

        public List<SkillSummary> ListSkills()
        {
            #region TODO: Student Code Here
            using (var context = new WorkScheduleContext())
            {
                var result = from skill in context.Skills
                             select new SkillSummary
                             {
                                 SkillId = skill.SkillID,
                                 Description = skill.Description,
                                 RequiresTicket = skill.RequiresTicket,
                                 CandidateCount = skill.EmployeeSkills.Count()
                             };
                return result.ToList();
            }
            #endregion
        }
        #endregion

        #region Command Methods
        public int Save(Contract contract)
        {
            #region TODO: Student Code Here
            PlacementContract contractPlacement = null;
            ContractSkill contractSkill = null;
            Location contractLocation = null;
            using (var context = new WorkScheduleContext())
            {
                if(string.IsNullOrEmpty(contract.Title))
                {
                    errors.Add("Contract Title is required to be entered");
                }
                else
                {
                    if (contract.Title.Length < 5)
                    {
                        errors.Add("Contract Title needs 5 or more characters");
                    }
                }

                if(contract.ContractPeriod.From > contract.ContractPeriod.To)
                {
                    errors.Add("From Date of the contract must be earlier or at least equal to the To Date");
                }
                else
                {
                    if (contract.ContractPeriod.To < DateTime.Today)
                    {

                        errors.Add("Contract is in the past, unable to alter past contracts or add contracts in the past");
                    }
                }



                Skill exists = null;
                foreach (SkilledWorkerCount item in contract.WorkerSkills)
                {
                    exists = context.Skills.Find(item.SkillId);
                    if (exists == null)
                    {
                        errors.Add($"Skill identifier: {item.SkillId} is not found");
                    }

                    if (item.WorkerCount <= 0 || item.WorkerCount > 4)
                    {
                        errors.Add($"Skill {exists.Description} requires workers be between 1 and 4");
                    }
                }

                if (contract.CurrentContractId == 0)
                {
                    if (contract.ContractPeriod.From < DateTime.Today)
                    {
                        errors.Add($"Contract cannot start in the past: {contract.ContractPeriod.From}");
                    }

                    contractLocation = context.Locations.Find(contract.ClientLocationId);
                    if (contractLocation == null)
                    {
                        errors.Add($"Contract location identifier {contract.ClientLocationId} is not on file");
                    }

                    if (errors.Count == 0)
                    {
                        contractPlacement = new PlacementContract();
                        contractPlacement.LocationID = contract.ClientLocationId;
                        contractPlacement.Title = contract.Title;
                        contractPlacement.StartDate = contract.ContractPeriod.From;
                        contractPlacement.EndDate = contract.ContractPeriod.To;
                        contractPlacement.Requirements = contract.Requirements;
                        contractPlacement.Cancellation = null;
                        contractPlacement.Reason = null;
                        context.PlacementContracts.Add(contractPlacement);
                        
                        foreach(SkilledWorkerCount item in contract.WorkerSkills)
                        {
                            contractSkill = new ContractSkill();
                            contractSkill.SkillID = item.SkillId;
                            contractSkill.NumberOfEmployees = item.WorkerCount;
                            contractPlacement.ContractSkills.Add(contractSkill);
                        }

                    }
                }
                else
                {
                    contractLocation = context.Locations.Find(contract.ClientLocationId);
                    if (contractLocation ==null)
                    {
                        errors.Add($"Contract location id {contract.ClientLocationId} is not on file");
                    }
                    contractPlacement = context.PlacementContracts.Find(contract.CurrentContractId);
                    if (contractPlacement == null)
                    {
                        errors.Add($"Contract is no longer on file - refresh the client list");
                    }

                    if (errors.Count == 0)
                    {
                        contractPlacement.LocationID = contract.ClientLocationId;
                        contractPlacement.Title = contract.Title;
                        contractPlacement.StartDate = contract.ContractPeriod.From;
                        contractPlacement.EndDate = contract.ContractPeriod.To;
                        contractPlacement.Requirements = contract.Requirements;
                        context.Entry(contractPlacement).State = System.Data.Entity.EntityState.Modified;

                        var currentContractSkills = context.ContractSkills
                                                    .Where(x => x.ContractID == contractPlacement.PlacementContractID)
                                                    .Select(x => x)
                                                    .ToList();
                        foreach (ContractSkill item in currentContractSkills)
                        {
                            context.ContractSkills.Remove(item);
                        }

                        foreach (SkilledWorkerCount item in contract.WorkerSkills)
                        {
                            contractSkill = new ContractSkill();
                            contractSkill.ContractID = contractPlacement.PlacementContractID;
                            contractSkill.SkillID = item.SkillId;
                            contractSkill.NumberOfEmployees = item.WorkerCount;
                            contractPlacement.ContractSkills.Add(contractSkill);
                        }
                    }
                }

                if (errors.Count > 0)
                {
                    throw new BusinessRuleException("The following errors have been raised", errors);
                }

                context.SaveChanges();
                return contractPlacement.PlacementContractID;
            }

            #endregion
        }

        #endregion
    }
}
