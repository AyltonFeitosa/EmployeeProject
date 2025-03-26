using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Entity
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string JobTitle { get; set; }
        public int Gender { get; set; }
        [ForeignKey(nameof(Department))]
        public int? DepartmentId { get; set; } // Chave estrangeira para a tabela de departamentos. Pode ser null pois um empregado pode não ter um departamento ou desempregado.
        public Department? Department { get; set; } // Propiedade completa para a tabela de departamentos.
        public DateTime JoiningDate { get; set; }
        public DateTime? LastWorkingDate { get; set; } 
        public DateTime DateOfBirth { get; set; }
    }
}
