using EmpDepRoleFulstackProjectJun13.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EmpDepRoleFulstackProjectJun13.Services
{
    public interface IEmployeeService
    {
        Task<GetEmployeeV1DTO> CreateEmployeeAsync(CreateEmployeeDTO dto);
        Task<GetEmployeeV1DTO?> GetEmployeeByIdAsync(int id);
        Task<List<GetEmployeeV1DTO>> GetAllEmployeesAsync(string? filterOn,string? fileterQuery,string? sortBy, bool isAscending, int? PageNumber = 1,
            int? PageSize = 1000);
        Task<GetEmployeeV1DTO> UpdateEmployeeAsync(int id, UpdateEmployeeDTO dto);
        Task<bool> DeleteEmployeeAsync(int id);

    }
}
