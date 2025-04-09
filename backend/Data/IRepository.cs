using System.Linq.Expressions;

namespace EmployeeManagementSystem.Data
{
    public interface IRepository<T> where T : class //Defini aqui metodos padrões para serem implementados
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetAll(Expression<Func<T, bool >> filter);
        Task<T> FindByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(int id);
        
        Task <int> SaveChangesAsync();
    }
}
