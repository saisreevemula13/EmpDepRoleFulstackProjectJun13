using EmpDepRoleFulstackProjectJun13.Models.Domain;

namespace EmpDepRoleFulstackProjectJun13.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department?> GetByIdAsync(int id);
    }
}
