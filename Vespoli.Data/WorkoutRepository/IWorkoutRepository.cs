using System.Collections.Generic;
using System.Threading.Tasks;
using Vespoli.Domain;

namespace Vespoli.Data
{
    public interface IWorkoutRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<Workout>> GeAllWorkoutsByRower(int rowerId);
        Task<Workout> GetSingleWorkout(int rowerId, int id);
    }
}