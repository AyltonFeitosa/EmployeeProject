using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Entity;
using EmployeeManagementSystem.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers // Controlador de requisicoes HTTP   
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepository<Employee> employeeRepository;
        private readonly IRepository<User> userRepo;

        public EmployeeController(IRepository<Employee> employeeRepository, IRepository<User> userRepo)
        {
            this.employeeRepository = employeeRepository;
            this.userRepo = userRepo;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEmployeeList()
        {
            return Ok(await employeeRepository.GetAll());
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetOne([FromRoute] int id)
        {
            return Ok(await employeeRepository.FindByIdAsync(id));
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task <IActionResult> AddEmployee([FromBody] Employee model)
        {
            var user = new User()
            {
                Email = model.Email,
                Role = "Employee",
               Password = (new PasswordHelper()).HashPassword("12345")
            };
            await userRepo.AddAsync(user);
            model.User = user;
            await employeeRepository.AddAsync(model);
            await employeeRepository.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task <IActionResult> DeleteEmployee([FromRoute] int id)
        {
            await employeeRepository.DeleteAsync(id);
            await employeeRepository.SaveChangesAsync();
            return Ok();
        }

    }
}
