using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Vespoli.Domain;

namespace Vespoli.Data
{
    public class VespoliContext : DbContext
    {
        public VespoliContext(DbContextOptions<VespoliContext> options) : base(options)
        {

        }

        public DbSet<Rower> Rowers { get; set; }
        public DbSet<Workout> Workouts { get; set; }
    }
}
