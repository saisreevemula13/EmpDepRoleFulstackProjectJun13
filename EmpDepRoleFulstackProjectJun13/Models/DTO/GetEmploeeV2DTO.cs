﻿namespace EmpDepRoleFulstackProjectJun13.Models.DTO
{
    public class GetEmploeeV2DTO
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoiningDate { get; set; }
        public string RoleName { get; set; }
        public string DepartmentName { get; set; }
        public string? Designation { get; set; }
    }
}
