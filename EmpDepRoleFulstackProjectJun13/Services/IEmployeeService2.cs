using EmpDepRoleFulstackProjectJun13.Models.DTO;

namespace EmpDepRoleFulstackProjectJun13.Services
{
    public interface IEmployeeService2
    {
        Task<List<GetEmploeeV2DTO>> GetAllEmployeesAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending, int? PageNumber, int? PageSize);
        Task<GetEmploeeV2DTO> GetEmployeeByIdAsync(int id);
        Task<GetEmploeeV2DTO> CreateEmployeeAsync(CreateEmployee2DTO empDTO);
        Task<GetEmploeeV2DTO> UpdateEmployeeAsync(int id, UpdateEmployeeDTO empDTO);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
