using EmpDepRoleFulstackProjectJun13.Exceptions;
using EmpDepRoleFulstackProjectJun13.Models.Domain;
using EmpDepRoleFulstackProjectJun13.Models.DTO;
using EmpDepRoleFulstackProjectJun13.Repositories;

namespace EmpDepRoleFulstackProjectJun13.Services
{
    public class EmployeeService:IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IRoleRepository _roleRepository;
        public EmployeeService(IEmployeeRepository repository, IRoleRepository roleRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = repository;
            _roleRepository= roleRepository;
            _departmentRepository= departmentRepository;
        }
        public async Task<GetEmployeeDTO> CreateEmployeeAsync(CreateEmployeeDTO dto)
        {
            // 1️⃣ Business Rule
            if (dto.JoiningDate > DateTime.UtcNow)
                throw new Exception("Joining Date cannot be in future");

            var role = await _roleRepository.GetByIdAsync(dto.RoleId);
            if (role == null)
                throw new Exception("Invalid Role");

            if (role.RoleName == "Intern" && dto.Salary > 50000)
                throw new BusinessRuleException("Intern salary cannot exceed ₹50,000");

            var dept = await _departmentRepository.GetByIdAsync(dto.DepartmentId);
            if (dept == null)
                throw new Exception("Invalid Department");

            // 2️⃣ Map DTO → Domain
            var emp = new Employee
            {
                Name = dto.Name,
                Email = dto.Email,
                Salary = dto.Salary,
                JoiningDate = dto.JoiningDate,
                DepartmentId = dto.DepartmentId,
                RoleId = dto.RoleId
            };

            // 3️⃣ Call Repository to Add
            await _employeeRepository.CreateAsync(emp);

            // 4️⃣ Reload
            var savedEmp = await _employeeRepository.GetByIdAsync(emp.EmployeeId);

            // 5️⃣ Map Domain → DTO
            return new GetEmployeeDTO
            {
                EmployeeId = savedEmp.EmployeeId,
                Name = savedEmp.Name,
                Email = savedEmp.Email,
                Salary = savedEmp.Salary,
                JoiningDate = savedEmp.JoiningDate,
                DepartmentName = savedEmp.Department?.Name,
                RoleName = savedEmp.Role?.RoleName
            };
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var emp = await _employeeRepository.DeleteAsync(id);

            // emp will be null if not found, so return false to let controller decide
            return emp != null;
        }

        public async Task<List<GetEmployeeDTO>> GetAllEmployeesAsync()
        {
            var employees=await _employeeRepository.GetAllAsync();
            //Map domaian to DTO;
            var employeeList=new List<GetEmployeeDTO>();
            foreach (var employee in employees)
            {
                employeeList.Add(
                new GetEmployeeDTO
                {
                    EmployeeId = employee.EmployeeId,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DepartmentName = employee.Department?.Name,
                    RoleName=employee.Role?.RoleName
                });
            }
            return employeeList;
        }

        public async Task<GetEmployeeDTO?> GetEmployeeByIdAsync(int id)
        {
            var emp=await _employeeRepository.GetByIdAsync(id);
            if (emp == null)
            {
                return null;
            }
            //domain to DTO conversion
            var dto = new GetEmployeeDTO
            {
                EmployeeId =emp.EmployeeId,
                Name = emp.Name,
                Email = emp.Email,
                JoiningDate = emp.JoiningDate,
                DepartmentName = emp.Department?.Name,
                RoleName = emp.Role?.RoleName
            };
            return dto;
        }

        public async Task<GetEmployeeDTO> UpdateEmployeeAsync(int id, UpdateEmployeeDTO empDTO)
        {
            var emp = new Employee
            {
                Email = empDTO.Email,
                DepartmentId = empDTO.DepartmentId,
                Salary = empDTO.Salary,
                JoiningDate = empDTO.JoiningDate,
                RoleId = empDTO.RoleId,
                Name = empDTO.Name
            };
            emp=await _employeeRepository.UpdateAsync(id,emp);
            if (emp == null)
                return null;
            //domain to DTO conversion
            var dto = new GetEmployeeDTO
            {
                EmployeeId = emp.EmployeeId,
                Name = emp.Name,
                Email = emp.Email,
                Salary=emp.Salary,
                JoiningDate = emp.JoiningDate,
                DepartmentName = emp.Department?.Name,
                RoleName = emp.Role?.RoleName
            };
            return dto;
        }
    }
}