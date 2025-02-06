namespace PetShop.Core.Base.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
        public bool Detached(T entity);

    }
}
