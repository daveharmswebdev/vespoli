using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Vespoli.Domain;

namespace Vespoli.Data
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly VespoliContext _context;
        private readonly ILogger<WorkoutRepository> _logger;

        public WorkoutRepository(VespoliContext context, ILogger<WorkoutRepository> logger)
        {
            _context = context;
            _logger = logger;
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