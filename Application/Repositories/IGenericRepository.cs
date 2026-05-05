namespace Application.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        public IQueryable<T> GetAll();
        public Task<T> GetByIdAsync(Guid id);
        public Task InsertAsync(T input);
        public Task InsertRangeAsync(List<T> input);
        public void Update(T input);
        public void Delete(T input);
        public Task SaveChangesAsync();
    }
}
