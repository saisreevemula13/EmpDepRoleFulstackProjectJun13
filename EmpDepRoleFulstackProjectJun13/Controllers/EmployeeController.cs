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
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDBContext _dbContext;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeService _employeeService;
        public EmployeeController(AppDBContext dbContext, IEmployeeRepository employeeRepository, IEmployeeService employeeService)
        {
            _dbContext = dbContext;
            _employeeRepository= employeeRepository;
            _employeeService = employeeService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager,IT")]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployeesById(int id)
        {
            var emp = await _employeeService.GetEmployeeByIdAsync(id);
            return Ok(emp);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDTO empDTO)
        {
             var emp= await _employeeService.CreateEmployeeAsync(empDTO);
            return CreatedAtAction(nameof(GetEmployeesById), new { id = emp.EmployeeId }, emp);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,IT")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDTO empDTO)
        {

            var UpdateEmp=await _employeeService.UpdateEmployeeAsync(id, empDTO);
            if (UpdateEmp == null)
                return NotFound(new {message = $"Employee {id} not found" });
            return Ok(UpdateEmp);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var isDeleted = await _employeeService.DeleteEmployeeAsync(id);
            return isDeleted ? Ok(new { message = "Employee deleted successfully" }): NotFound();
        }
    }
}