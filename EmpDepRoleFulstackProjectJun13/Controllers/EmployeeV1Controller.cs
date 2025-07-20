using Asp.Versioning;
using EmpDepRoleFulstackProjectJun13.Data;
using EmpDepRoleFulstackProjectJun13.Models.Domain;
using EmpDepRoleFulstackProjectJun13.Models.DTO;
using EmpDepRoleFulstackProjectJun13.Repositories;
using EmpDepRoleFulstackProjectJun13.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpDepRoleFulstackProjectJun13.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EmployeeV1Controller : ControllerBase
    {
        private readonly AppDBContext _dbContext;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeService _employeeService;
        public EmployeeV1Controller(AppDBContext dbContext, IEmployeeRepository employeeRepository, IEmployeeService employeeService)
        {
            _dbContext = dbContext;
            _employeeRepository= employeeRepository;
            _employeeService = employeeService;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Admin,Manager,IT,Tester")]
        public async Task<IActionResult> GetEmployees([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, bool? isAscending, [FromQuery] int? PageNumber = 1,
             [FromQuery] int? PageSize = 1000)
        {
            var employees = await _employeeService.GetAllEmployeesAsync(filterOn,filterQuery,sortBy, isAscending ?? true, PageNumber,PageSize);
            return Ok(employees);
        }

        [HttpGet("{id:int}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetEmployeesById(int id)
        {
            var emp = await _employeeService.GetEmployeeByIdAsync(id);
            return Ok(emp);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDTO empDTO)
        {
             var emp= await _employeeService.CreateEmployeeAsync(empDTO);
            return CreatedAtAction(nameof(GetEmployeesById), new { id = emp.EmployeeId }, emp);
        }

        [HttpPut("{id:int}")]
        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Admin,Manager,IT")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDTO empDTO)
        {

            var UpdateEmp=await _employeeService.UpdateEmployeeAsync(id, empDTO);
            if (UpdateEmp == null)
                return NotFound(new {message = $"Employee {id} not found" });
            return Ok(UpdateEmp);
        }

        [HttpDelete("{id:int}")]
        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var isDeleted = await _employeeService.DeleteEmployeeAsync(id);
            return isDeleted ? Ok(new { message = "Employee deleted successfully" }): NotFound();
        }


//        {
//  "username": "karan.mehta@example.com",
//  "password": "karan@123"
//}
}
}
