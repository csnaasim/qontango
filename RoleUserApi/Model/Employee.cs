using System;
using System.Data;
using System.Collections.Generic;
using RoleUserApi.Model.Repositories.Interface;
using RoleUserApi.Model.Repositories.Implementation;

namespace RoleUserApi.Model
{
    public class Employee 
    {
        public int UserID { get; set; }
        public int EmpID { get; set; }
        public int OrgID { get; set; }
        public Organization Org { get; set; }
        public EmpDesignation Designation { get; set; }

        public Role Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DesigID { get; set; }

        public int RoleID { get; set; }
        public string WorkPhone { get; set; }
        public string PhoneExt { get; set; }
        public string HomePhone { get; set; }
        public string ProfessionalEmail { get; set; }
        public string PersonalEmail { get; set; }
        public string FaxNo { get; set; }
        public string CellNo { get; set; }
        public string ImagePath { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public EntryLog EntryLog { get; set; }
        public static Employee Parse(DataRow row, string ColPrefix = "emp")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}ID",
                $"{ColPrefix}OrgID",
                $"Org",
                $"{ColPrefix}FirstName",
                $"{ColPrefix}LastName",
                $"{ColPrefix}DesigID",
                $"{ColPrefix}WorkPhone",
                $"{ColPrefix}PhoneExt",
                $"{ColPrefix}HomePhone",
                $"{ColPrefix}ProfessionalEmail",
                $"{ColPrefix}PersonalEmail",
                $"{ColPrefix}FaxNo",
                $"{ColPrefix}CellNo",
                $"{ColPrefix}ImagePath",
                $"{ColPrefix}Country",
                $"{ColPrefix}City",
                $"{ColPrefix}State",
                $"{ColPrefix}Zip",
                $"{ColPrefix}Address1",
                $"{ColPrefix}Address2",
                $"{ColPrefix}IsActive",
                $"{ColPrefix}IsDeleted",
                $"{ColPrefix}CreatedBy",
                $"{ColPrefix}CreateDate",
                $"{ColPrefix}UpdatedBy",
                $"{ColPrefix}UpdateDate",
        };
            if (!row.HasColumns(requiredColumns))
                return null;
                Employee emp        = new Employee();

                emp.EmpID           = row.GetValue<int>($"{ColPrefix}EmpID");
                emp.OrgID           = row.GetValue<int>($"OrgID");
                emp.Org             = Organization.GetOrganization(emp.OrgID);
                emp.FirstName       = row.GetValue<string>($"{ColPrefix}FirstName");
                emp.LastName        = row.GetValue<string>($"{ColPrefix}LastName");
                emp.DesigID         = row.GetValue<int>($"dsgDesigID");
                emp.RoleID             = row.GetValue<int>($"rolRoleID");
                emp.Designation     = EmpDesignation.Parse(row);
                emp.Role            =           Role.Parse(row);
                emp.WorkPhone       = row.GetValue<string>($"{ColPrefix}WorkPhone");
                emp.PhoneExt        = row.GetValue<string>($"{ColPrefix}PhoneExt");
                emp.HomePhone       = row.GetValue<string>($"{ColPrefix}HomePhone");
                emp.ProfessionalEmail = row.GetValue<string>($"{ColPrefix}ProfessionalEmail");
                emp.PersonalEmail   = row.GetValue<string>($"{ColPrefix}PersonalEmail");
                emp.FaxNo           = row.GetValue<string>($"{ColPrefix}FaxNo");
                emp.CellNo          = row.GetValue<string>($"{ColPrefix}CellNo");
                emp.ImagePath       = row.GetValue<string>($"{ColPrefix}ImagePath");
                emp.Country         = row.GetValue<string>($"{ColPrefix}Country");
                emp.City            = row.GetValue<string>($"{ColPrefix}City");
                emp.State           = row.GetValue<string>($"{ColPrefix}State");
                emp.Zip             = row.GetValue<string>($"{ColPrefix}Zip");
                emp.Address1        = row.GetValue<string>($"{ColPrefix}Address1");
                emp.Address2        = row.GetValue<string>($"{ColPrefix}Address2");
                emp.IsActive        = row.GetValue<bool>($"{ColPrefix}IsActive");
                emp.IsDeleted       = row.GetValue<bool>($"{ColPrefix}IsDeleted");
                emp.CreatedBy       = row.GetValue<Int64>($"{ColPrefix}CreatedBy");
                emp.CreateDate      = row.GetValueNullable<DateTime>($"{ColPrefix}CreateDate");
                emp.UpdatedBy       = row.GetValue<Int64>($"{ColPrefix}UpdatedBy");
                emp.UpdateDate      = row.GetValueNullable<DateTime>($"{ColPrefix}UpdateDate");
                return emp;
        }

        public static Employee Parsenew(DataRow row, string ColPrefix = "emp")
        {
            string[] requiredColumns = new[]
            {
                $"{ColPrefix}ID",
                $"{ColPrefix}OrgID",
                $"Org",
                $"{ColPrefix}FirstName",
                $"{ColPrefix}LastName",
                $"{ColPrefix}DesigID",
                $"{ColPrefix}WorkPhone",
                $"{ColPrefix}PhoneExt",
                $"{ColPrefix}HomePhone",
                $"{ColPrefix}ProfessionalEmail",
                $"{ColPrefix}PersonalEmail",
                $"{ColPrefix}FaxNo",
                $"{ColPrefix}CellNo",
                $"{ColPrefix}ImagePath",
                $"{ColPrefix}Country",
                $"{ColPrefix}City",
                $"{ColPrefix}State",
                $"{ColPrefix}Zip",
                $"{ColPrefix}Address1",
                $"{ColPrefix}Address2",
                $"{ColPrefix}IsActive",
                $"{ColPrefix}IsDeleted",
                $"{ColPrefix}CreatedBy",
                $"{ColPrefix}CreateDate",
                $"{ColPrefix}UpdatedBy",
                $"{ColPrefix}UpdateDate",
        };
            if (!row.HasColumns(requiredColumns))
                return null;
            Employee emp = new Employee();

            emp.EmpID = row.GetValue<int>($"{ColPrefix}ID");
            emp.OrgID = row.GetValue<int>($"{ColPrefix}OrgID");
            emp.Org = Organization.GetOrganization(emp.OrgID);
            emp.FirstName = row.GetValue<string>($"{ColPrefix}FirstName");
            emp.LastName = row.GetValue<string>($"{ColPrefix}LastName");
            emp.DesigID = row.GetValue<int>($"{ColPrefix}DesigID");
          //   emp.RoleID = row.GetValue<int>($"rolRoleID");
          //      emp.Designation = EmpDesignation.Parse(row);
          //  emp.Role = Role.Parse(row);
            emp.WorkPhone = row.GetValue<string>($"{ColPrefix}WorkPhone");
            emp.PhoneExt = row.GetValue<string>($"{ColPrefix}PhoneExt");
            emp.HomePhone = row.GetValue<string>($"{ColPrefix}HomePhone");
            emp.ProfessionalEmail = row.GetValue<string>($"{ColPrefix}ProfessionalEmail");
            emp.PersonalEmail = row.GetValue<string>($"{ColPrefix}PersonalEmail");
            emp.FaxNo = row.GetValue<string>($"{ColPrefix}FaxNo");
            emp.CellNo = row.GetValue<string>($"{ColPrefix}CellNo");
            emp.ImagePath = row.GetValue<string>($"{ColPrefix}ImagePath");
            emp.Country = row.GetValue<string>($"{ColPrefix}Country");
            emp.City = row.GetValue<string>($"{ColPrefix}City");
            emp.State = row.GetValue<string>($"{ColPrefix}State");
            emp.Zip = row.GetValue<string>($"{ColPrefix}Zip");
            emp.Address1 = row.GetValue<string>($"{ColPrefix}Address1");
            emp.Address2 = row.GetValue<string>($"{ColPrefix}Address2");
            emp.IsActive = row.GetValue<bool>($"{ColPrefix}IsActive");
            emp.IsDeleted = row.GetValue<bool>($"{ColPrefix}IsDeleted");
            emp.CreatedBy = row.GetValue<Int64>($"{ColPrefix}CreatedBy");
            emp.CreateDate = row.GetValueNullable<DateTime>($"{ColPrefix}CreateDate");
            emp.UpdatedBy = row.GetValue<Int64>($"{ColPrefix}UpdatedBy");
            emp.UpdateDate = row.GetValueNullable<DateTime>($"{ColPrefix}UpdateDate");
            return emp;
        }
        public static List<Employee> GetAllEmployees(int OrgID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0,
               OrgID
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetAllEmployees", obj);
            List<Employee> Employees = new List<Employee>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Employees.Add(Parsenew(item));
            }
            return Employees;
        }
        public static List<Employee> GetAllEmployeesAndDesignationAndRol(int OrgID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0,
               OrgID
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetAllEmployeesAndDesignationAndRol", obj);
            List<Employee> Employees = new List<Employee>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Employees.Add(Parse(item));
            }
            return Employees;
        }
        public static Employee GetEmployee(Int64 EmpID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            object[] obj = {
               0,
               EmpID
            };
            DataSet ds = networkRepo.PostDataTable("sp_FruGetEmployeeByID", obj);
            Employee Employee = new Employee();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Employee = Parse(ds.Tables[0].Rows[0]);
                }
            }
            return Employee;
        }

        public string Insert()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
            {
                0,
				this.OrgID,
				this.FirstName,
				this.LastName,
				this.DesigID,
				this.WorkPhone,
				this.PhoneExt,
				this.HomePhone,
				this.ProfessionalEmail,
				this.PersonalEmail,
				this.FaxNo,
				this.CellNo,
				this.ImagePath,
				this.Country,
				this.City,
				this.State,
				this.Zip,
				this.Address1,
				this.Address2,
				this.IsActive,
				this.IsDeleted,
				this.CreatedBy
            };
            string res = networkRepo.Post("sp_FruInsertEmployee", obj);
            return res;
        }

        public string Update()
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
            {
                0,
				this.EmpID,
				this.OrgID,
				this.FirstName,
				this.LastName,
				this.DesigID,
				this.WorkPhone,
				this.PhoneExt,
				this.HomePhone,
				this.ProfessionalEmail,
				this.PersonalEmail,
				this.FaxNo,
				this.CellNo,
				this.ImagePath,
				this.Country,
				this.City,
				this.State,
				this.Zip,
				this.Address1,
				this.Address2,
				this.IsActive,
				this.IsDeleted,
				this.UpdatedBy
            };
            string res = networkRepo.Post("sp_FruUpdateEmployee", obj);
            return res;
        }

        public string Delete(int EmployeeID)
        {
            INetworkRepo networkRepo = new NetworkRepo();
            Object[] obj =
             {
                0,
                EmployeeID
            };
            string res = networkRepo.Post("sp_FruDeleteEmployee", obj);
            return res;
        }
    }
}
