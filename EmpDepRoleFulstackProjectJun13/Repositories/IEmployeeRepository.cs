using EmpDepRoleFulstackProjectJun13.Models.Domain;

namespace EmpDepRoleFulstackProjectJun13.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync(string? filterOn, string? filterQuery,string? sortBy, bool isAscending = true,int? PageNumber=1,int? PageSize=1000);
        Task<Employee> GetByIdAsync(int employeeId);
        Task<Employee> CreateAsync(Employee employee);
        Task<Employee> UpdateAsync(int id, Employee employee);
        Task<Employee> DeleteAsync(int id);
    }
}
