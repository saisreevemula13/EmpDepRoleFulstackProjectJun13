using System.Data;

namespace EmpDepRoleFulstackProjectJun13.Models.Domain
{
    public class Employee
    {
            public int EmployeeId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public decimal Salary { get; set; }
            public DateTime JoiningDate { get; set; }

            // Foreign Keys
            public int DepartmentId { get; set; }
            public Department Department { get; set; }

            public int RoleId { get; set; }
            public Role Role { get; set; }
       
    }
}
