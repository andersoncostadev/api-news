using NewsAPI.Entities;

namespace NewsAPI.Infra.Interfaces
{
    public interface IMongoRepository<T>
    {
        Result<T> Get(int page, int pageSize);
        T Get(string id);
        T GetBySlug(string slug);
        T Create(T news);
        void Update(string id, T news);
        void Remove(string id);
    }
}