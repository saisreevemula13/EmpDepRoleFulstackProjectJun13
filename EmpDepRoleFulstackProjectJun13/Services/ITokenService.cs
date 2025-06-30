using EmpDepRoleFulstackProjectJun13.Models.Domain;

namespace EmpDepRoleFulstackProjectJun13.Services
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
