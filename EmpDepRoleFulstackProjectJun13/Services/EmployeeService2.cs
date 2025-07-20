using AutoMapper;
using EmpDepRoleFulstackProjectJun13.Exceptions;
using EmpDepRoleFulstackProjectJun13.Models.Domain;
using EmpDepRoleFulstackProjectJun13.Models.DTO;
using EmpDepRoleFulstackProjectJun13.Repositories;

namespace EmpDepRoleFulstackProjectJun13.Services
{
    public class EmployeeService2: IEmployeeService2
    {
            private readonly IMapper _mapper;
            private readonly IEmployeeRepository _employeeRepository;
            private readonly IDepartmentRepository _departmentRepository;
            private readonly IRoleRepository _roleRepository;
            public EmployeeService2(IEmployeeRepository repository, IRoleRepository roleRepository, IDepartmentRepository departmentRepository,
                IMapper mapper)
            {
                _mapper = mapper;
                _employeeRepository = repository;
                _roleRepository = roleRepository;
                _departmentRepository = departmentRepository;
            }
            public async Task<GetEmploeeV2DTO> CreateEmployeeAsync(CreateEmployee2DTO dto)
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
                //var emp = new Employee
                //{
                //    Name = dto.Name,
                //    Email = dto.Email,
                //    Salary = dto.Salary,
                //    JoiningDate = dto.JoiningDate,
                //    DepartmentId = dto.DepartmentId,
                //    RoleId = dto.RoleId
                //};
                var emp = _mapper.Map<Employee>(dto);

                // 3️⃣ Call Repository to Add
                await _employeeRepository.CreateAsync(emp);

                // 4️⃣ Reload
                var savedEmp = await _employeeRepository.GetByIdAsync(emp.EmployeeId);
                // 5️⃣ Map Domain → DTO
                //return new GetEmployeeDTO
                //{
                //    EmployeeId = savedEmp.EmployeeId,
                //    Name = savedEmp.Name,
                //    Email = savedEmp.Email,
                //    Salary = savedEmp.Salary,
                //    JoiningDate = savedEmp.JoiningDate,
                //    DepartmentName = savedEmp.Department?.Name,
                //    RoleName = savedEmp.Role?.RoleName
                //};
                return _mapper.Map<GetEmploeeV2DTO>(savedEmp);
            }

            public async Task<bool> DeleteEmployeeAsync(int id)
            {
                var emp = await _employeeRepository.DeleteAsync(id);

                // emp will be null if not found, so return false to let controller decide
                return emp != null;
            }

            public async Task<List<GetEmploeeV2DTO>> GetAllEmployeesAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int? PageNumber = 1,
                  int? PageSize = 1000)
            {
                var employees = await _employeeRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending, PageNumber, PageSize);
                //Map domaian to DTO;
                var employeeList = new List<GetEmploeeV2DTO>();
                //foreach (var employee in employees)
                //{
                //    employeeList.Add(
                //    new GetEmployeeDTO
                //    {
                //        EmployeeId = employee.EmployeeId,
                //        Name = employee.Name,
                //        Email = employee.Email,
                //        Salary = employee.Salary,
                //        DepartmentName = employee.Department?.Name,
                //        RoleName=employee.Role?.RoleName
                //    });
                //}
                return _mapper.Map<List<GetEmploeeV2DTO>>(employees);
            }

            public async Task<GetEmploeeV2DTO?> GetEmployeeByIdAsync(int id)
            {
                var emp = await _employeeRepository.GetByIdAsync(id);
                if (emp == null)
                {
                    return null;
                }
                //domain to DTO conversion
                //var dto = new GetEmployeeDTO
                //{
                //    EmployeeId =emp.EmployeeId,
                //    Name = emp.Name,
                //    Email = emp.Email,
                //    JoiningDate = emp.JoiningDate,
                //    DepartmentName = emp.Department?.Name,
                //    RoleName = emp.Role?.RoleName
                //};
                return _mapper.Map<GetEmploeeV2DTO>(emp);
            }

            public async Task<GetEmploeeV2DTO> UpdateEmployeeAsync(int id, UpdateEmployeeDTO empDTO)
            {
                //dto to domain
                var emp = _mapper.Map<Employee>(empDTO);
                //var emp = new Employee
                //{
                //    Email = empDTO.Email,
                //    DepartmentId = empDTO.DepartmentId,
                //    Salary = empDTO.Salary,
                //    JoiningDate = empDTO.JoiningDate,
                //    RoleId = empDTO.RoleId,
                //    Name = empDTO.Name
                //};
                emp = await _employeeRepository.UpdateAsync(id, emp);
                if (emp == null)
                    return null;
                //domain to DTO conversion
                //var dto = new GetEmployeeDTO
                //{
                //    EmployeeId = emp.EmployeeId,
                //    Name = emp.Name,
                //    Email = emp.Email,
                //    Salary=emp.Salary,
                //    JoiningDate = emp.JoiningDate,
                //    DepartmentName = emp.Department?.Name,
                //    RoleName = emp.Role?.RoleName
                //};
                return _mapper.Map<GetEmploeeV2DTO>(emp);
            }
        }
    }
