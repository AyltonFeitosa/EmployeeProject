using EmployeeManagementSystem.Service;

namespace EmployeeManagementSystem.Data
{
    public class DataSendHelper
    {
        private readonly AppDbContext dbContext;

        public DataSendHelper(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void InsertData()
        {
            if (!dbContext.Employees.Any())
            {
                dbContext.Employees.Add(
                    new Entity.Employee {Name = "Employee 1" });
                dbContext.Employees.Add(
                    new Entity.Employee {Name = "Employee 2" });
            }

            if (!dbContext.Users.Any())
            {
                var passwordHelper = new PasswordHelper();
                dbContext.Users.Add(new Entity.User()
                {
                    Email = "admin@teste.com",
                    Password = passwordHelper.HashPassword("12345"),
                    Role = "Admin"
                });
                dbContext.Users.Add(new Entity.User()
                {
                    Email = "emp1@teste.com",
                    Password = passwordHelper.HashPassword("12345"),
                    Role = "Employee"

                });
            }
            
            dbContext.SaveChanges();


        }
    }
}
