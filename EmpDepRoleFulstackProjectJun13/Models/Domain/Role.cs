namespace EmpDepRoleFulstackProjectJun13.Models.Domain
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
