using Asp.Versioning;
using EmpDepRoleFulstackProjectJun13.Data;
using EmpDepRoleFulstackProjectJun13.Models.DTO;
using EmpDepRoleFulstackProjectJun13.Repositories;
using EmpDepRoleFulstackProjectJun13.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmpDepRoleFulstackProjectJun13.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EmployeeV2Controller : ControllerBase
    {
        private readonly AppDBContext _dbContext;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeService2 _employeeService;
        public EmployeeV2Controller(AppDBContext dbContext, IEmployeeRepository employeeRepository, IEmployeeService2 employeeService)
        {
            _dbContext = dbContext;
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        [Authorize(Roles = "Admin,Manager,IT,Tester")]
        public async Task<IActionResult> GetEmployees([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, bool? isAscending, [FromQuery] int? PageNumber = 1,
             [FromQuery] int? PageSize = 1000)
        {
            var employees = await _employeeService.GetAllEmployeesAsync(filterOn, filterQuery, sortBy, isAscending ?? true, PageNumber, PageSize);
            return Ok(employees);
        }

        [HttpGet("{id:int}")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> GetEmployeesById(int id)
        {
            var emp = await _employeeService.GetEmployeeByIdAsync(id);
            return Ok(emp);
        }

        [HttpPost]
        [MapToApiVersion("2.0")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployee2DTO empDTO)
        {
            var emp = await _employeeService.CreateEmployeeAsync(empDTO);
            return CreatedAtAction(nameof(GetEmployeesById), new { id = emp.EmployeeId }, emp);
        }

        [HttpPut("{id:int}")]
        [MapToApiVersion("2.0")]
        [Authorize(Roles = "Admin,Manager,IT")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDTO empDTO)
        {

            var UpdateEmp = await _employeeService.UpdateEmployeeAsync(id, empDTO);
            if (UpdateEmp == null)
                return NotFound(new { message = $"Employee {id} not found" });
            return Ok(UpdateEmp);
        }

        [HttpDelete("{id:int}")]
        [MapToApiVersion("2.0")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var isDeleted = await _employeeService.DeleteEmployeeAsync(id);
            return isDeleted ? Ok(new { message = "Employee deleted successfully" }) : NotFound();
        }
        //        {
        //  "username": "karan.mehta@example.com",
        //  "password": "karan@123"
        //}
}
}
