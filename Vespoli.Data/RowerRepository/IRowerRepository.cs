using System.Collections.Generic;
using System.Threading.Tasks;
using Vespoli.Domain;

namespace Vespoli.Data
{
    public interface IRowerRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<Rower>> GeAllRowers();
        Task<Rower> GetSingleRower(int id);
    }
}
