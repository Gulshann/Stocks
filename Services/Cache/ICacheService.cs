using System.Threading.Tasks;

namespace StocksApp.Services.Cache
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key);
        T Get<T>(string key);

        Task InsertAsync(string key, object item, int expirationMinutes);
        void Insert(string key, object item, int expirationMinutes);


        Task RemoveAsync(string key);

    }
}