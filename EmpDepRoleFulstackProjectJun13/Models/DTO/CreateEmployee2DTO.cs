namespace EmpDepRoleFulstackProjectJun13.Models.DTO
{
    public class CreateEmployee2DTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoiningDate { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        //new field
        public string Designation{ get; set; }
    }
}
