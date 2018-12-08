using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vespoli.Domain;

namespace Vespoli.Data
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly VespoliContext _context;

        public WorkoutRepository(VespoliContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<Workout>> GeAllWorkoutsByRower(int rowerId)
        {
            var workouts = await _context.Workouts
                .Where(w => w.RowerId == rowerId)
                .Include(w => w.Rower)
                .ToListAsync();

            return workouts;
        }

        public async Task<Workout> GetSingleWorkout(int rowerId, int id)
        {
            var workout = await _context.Workouts
                .Where(w => w.RowerId == rowerId)
                .FirstOrDefaultAsync(w => w.Id == id); 

            return workout;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}