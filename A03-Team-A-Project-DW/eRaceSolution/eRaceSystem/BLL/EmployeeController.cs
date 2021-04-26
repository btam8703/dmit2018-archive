using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using eRaceSystem.DAL;
using eRaceSystem.Entities;
using eRaceSystem.ViewModels;
using System.ComponentModel; 
#endregion


namespace eRaceSystem.BLL
{
    public class EmployeeController
    {
        public EmployeeItem Employee_FindByID(int employeeid)
        {
            using (var context = new eRaceContext())
            {
                var results = context.Employees
                                .Where(x => x.EmployeeID == employeeid)
                                .Select(x => x)
                                .FirstOrDefault();
                EmployeeItem item = new EmployeeItem
                {
                     EmployeeID = results.EmployeeID,
                     LastName = results.LastName,
                     FirstName = results.FirstName,
                     Address = results.Address,
                     City = results.City,
                     PostalCode = results.PostalCode,
                     Phone = results.Phone,
                     PositionID = results.PositionID,
                     LoginID = results.LoginId,
                     BirthDate = results.BirthDate,
                     SocialInsuranceNumber = results.SocialInsuranceNumber

                };
                return item;
            }
        }

    }
}
