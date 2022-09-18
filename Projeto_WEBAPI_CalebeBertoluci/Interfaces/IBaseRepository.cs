namespace Projeto_WEBAPI_CalebeBertoluci.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<IQueryable<T>> Get(int page, int maxResults);

        Task<T?> GetByKey(int key);

        Task<T> Insert(T entity);

        Task<T> Update(T entity);

        Task<int> Delete(int key);
    }
}