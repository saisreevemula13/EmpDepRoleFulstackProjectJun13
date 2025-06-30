using EmpDepRoleFulstackProjectJun13.Data;
using EmpDepRoleFulstackProjectJun13.Models.Domain;
using EmpDepRoleFulstackProjectJun13.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace EmpDepRoleFulstackProjectJun13.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDBContext _dbContext;
        public EmployeeRepository(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task<Employee> CreateAsync(Employee employee)
        {
            _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }
      
        public async Task<Employee> DeleteAsync(int id)
        {
            var emp=await _dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (emp == null)
            {
                return null;
            }

            _dbContext.Employees.Remove(emp);
            await _dbContext.SaveChangesAsync();

            return emp;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _dbContext.Employees
                .Include(e => e.Department)
                .Include(e => e.Role)
                .ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await GetEmployeesWithIncludes()
                 .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task<Employee?> UpdateAsync(int id, Employee employee)
        {
           var existingEmp = await _dbContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (existingEmp == null)
                return null;

            existingEmp.Name = employee.Name;
            existingEmp.JoiningDate = employee.JoiningDate;
            existingEmp.Email = employee.Email;
            existingEmp.Salary = employee.Salary;
            existingEmp.RoleId = employee.RoleId;
            existingEmp.DepartmentId = employee.DepartmentId;

            await _dbContext.SaveChangesAsync();

            return await GetEmployeesWithIncludes()
                 .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }
        private IQueryable<Employee> GetEmployeesWithIncludes()
        {
            return _dbContext.Employees
                .Include(e => e.Department)
                .Include(e => e.Role);
        }

    }
}
