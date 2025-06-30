using EmpDepRoleFulstackProjectJun13.Models.Domain;

namespace EmpDepRoleFulstackProjectJun13.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(int id);
    }
}
