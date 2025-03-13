namespace EmployeeManagementSystem.Data
{
    public interface IRepository<T> where T : class //Defini aqui metodos padrões para serem implementados
    {
        Task<List<T>> GetAll();        
        Task<T> FindByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(int id);
        
        Task <int> SaveChangesAsync();
    }
}
