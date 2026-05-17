namespace Application.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll(); // IQueryable
        Task<T> GetByIdAsync(Guid id);
        Task InsertAsync(T input);
        Task InsertRangeAsync(List<T> input);
        void Update(T input);
        void Delete(T input);
        Task SaveChangesAsync();
    }
}
