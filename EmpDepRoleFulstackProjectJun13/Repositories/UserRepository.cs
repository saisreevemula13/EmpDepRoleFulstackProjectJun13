using EmpDepRoleFulstackProjectJun13.Data;
using EmpDepRoleFulstackProjectJun13.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmpDepRoleFulstackProjectJun13.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly AppDBContext _context;

        public UserRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            return await _context.Users // Includes the Role navigation property
      .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

    }

}
