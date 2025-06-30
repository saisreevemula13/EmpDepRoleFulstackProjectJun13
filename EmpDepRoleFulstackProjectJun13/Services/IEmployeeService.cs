using EmpDepRoleFulstackProjectJun13.Models.DTO;

namespace EmpDepRoleFulstackProjectJun13.Services
{
    public interface IEmployeeService
    {
        Task<GetEmployeeDTO> CreateEmployeeAsync(CreateEmployeeDTO dto);
        Task<GetEmployeeDTO?> GetEmployeeByIdAsync(int id);
        Task<List<GetEmployeeDTO>> GetAllEmployeesAsync();
        Task<GetEmployeeDTO> UpdateEmployeeAsync(int id, UpdateEmployeeDTO dto);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
