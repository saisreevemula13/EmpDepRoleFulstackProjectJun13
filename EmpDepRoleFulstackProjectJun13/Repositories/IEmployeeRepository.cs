using EmpDepRoleFulstackProjectJun13.Models.Domain;

namespace EmpDepRoleFulstackProjectJun13.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int employeeId);
        Task<Employee> CreateAsync(Employee employee);
        Task<Employee> UpdateAsync(int id, Employee employee);
        Task<Employee> DeleteAsync(int id);
    }
}
