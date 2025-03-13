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
            dbContext.SaveChanges();
        }
    }
}
