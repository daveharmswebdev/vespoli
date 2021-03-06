﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vespoli.Domain;

namespace Vespoli.Data
{
    public class RowerRepository : IRowerRepository
    {
        private readonly VespoliContext _context;
        private readonly ILogger<RowerRepository> _logger;

        public RowerRepository(VespoliContext context, ILogger<RowerRepository> logger)
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

        public async Task<IEnumerable<Rower>> GeAllRowers()
        {
            var rowers = await _context.Rowers
                .Include(w => w.Workouts)
                .ToListAsync();

            return rowers;
        }

        public async Task<Rower> GetSingleRower(int id)
        {
            var rower = await _context.Rowers
                .Include(w => w.Workouts)
                .FirstOrDefaultAsync(r => r.Id == id);

            return rower;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
