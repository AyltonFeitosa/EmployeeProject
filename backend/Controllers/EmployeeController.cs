using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers // Controlador de requisicoes HTTP   
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepository<Employee> employeeRepository;

        public EmployeeController(IRepository<Employee> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeList()
        {
            return Ok(await employeeRepository.GetAll());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] int id)
        {
            return Ok(await employeeRepository.FindByIdAsync(id));
        }
        [HttpPost]
        public async Task <IActionResult> AddEmployee([FromBody] Employee model)
        {
            await employeeRepository.AddAsync(model);
            await employeeRepository.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task <IActionResult> UpdateEmployee([FromRoute] int id, [FromBody] Employee model)
        {
            var employee = await employeeRepository.FindByIdAsync(id);
            employee.Name = model.Name;
            employee.Email = model.Email;
            employee.Phone = model.Phone;
            employee.LastWorkingDate = model.LastWorkingDate;
            employee.JobTitle = model.JobTitle;
            employee.DepartmentId = model.DepartmentId;
            await employeeRepository.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteEmployee([FromRoute] int id)
        {
            await employeeRepository.DeleteAsync(id);
            await employeeRepository.SaveChangesAsync();
            return Ok();
        }

    }
}
