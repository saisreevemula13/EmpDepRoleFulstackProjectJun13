using EmpDepRoleFulstackProjectJun13.Data;
using EmpDepRoleFulstackProjectJun13.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmpDepRoleFulstackProjectJun13.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDBContext _dbContext;
        public DepartmentRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _dbContext.Departments.FirstOrDefaultAsync(x => x.DepartmentId == id);
        }
    }
}
