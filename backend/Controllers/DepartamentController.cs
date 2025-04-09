using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentController : ControllerBase
    {
        private readonly IRepository<Department> departamentRepository;

        public DepartamentController(IRepository<Department> departamentRepository)
        {
            this.departamentRepository = departamentRepository;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddDepartment([FromBody]Department department)
        {
            await departamentRepository.AddAsync(department);
            await departamentRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] int id)
        {
            await departamentRepository.DeleteAsync(id);
            await departamentRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] int id,[FromBody]Department model)
        {
            var department = await departamentRepository.FindByIdAsync(id);
            department.Name = model.Name;
            departamentRepository.Update(department);
            await departamentRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllDepartment()
        {
            var departments = await departamentRepository.GetAll();
            return Ok(departments);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetOne(int id)
        {
            var department = await departamentRepository.FindByIdAsync(id);
            return Ok(department);
        }


    }
}
