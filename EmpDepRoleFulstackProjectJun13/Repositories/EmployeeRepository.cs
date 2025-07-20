using EmpDepRoleFulstackProjectJun13.Data;
using EmpDepRoleFulstackProjectJun13.Models.Domain;
using EmpDepRoleFulstackProjectJun13.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public async Task<List<Employee>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true,
             int? PageNumber = 1,
             int? PageSize = 1000)
        {
            var emps = _dbContext.Employees
                .Include(e => e.Department)
                .Include(e => e.Role)
                .AsQueryable();
            //filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    emps = emps.Where(x => x.Name.Contains(filterQuery));
                }
            }
            //sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    emps = isAscending ? emps.OrderBy(x => x.Name) : emps.OrderByDescending(x => x.Name);
                }
                if (sortBy.Equals("Salary", StringComparison.OrdinalIgnoreCase))
                {
                    emps = isAscending ? emps.OrderBy(x => x.Salary) : emps.OrderByDescending(x => x.Salary);
                }
            }
            //Pagination
            int pageNumber = PageNumber ?? 1;     // unwrap or use default
            int pageSize = PageSize ?? 1000;  // unwrap or use default

            int skipResults = (pageNumber - 1) * pageSize;

             return await emps
                .Skip(skipResults)
                .Take(pageSize)
                .ToListAsync();
            //var items=await emps
            //     .Skip(skipResults)
            //     .Take(pageSize)
            //     .ToListAsync();
            //Dynamic pagination:
            //int totalRecords = await emps.CountAsync();
            //int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            //return new PaginatedResult<Employee>
            //{
            //    Data = items,
            //    PageNumber = pageNumber,
            //    PageSize = pageSize,
            //    TotalRecords = totalRecords,
            //    TotalPages = totalPages
            //};

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
