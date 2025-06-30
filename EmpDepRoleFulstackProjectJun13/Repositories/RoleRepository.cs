using EmpDepRoleFulstackProjectJun13.Data;
using EmpDepRoleFulstackProjectJun13.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmpDepRoleFulstackProjectJun13.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDBContext _dbContext;
        public RoleRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(x=>x.RoleId == id);

        }
    }
}
