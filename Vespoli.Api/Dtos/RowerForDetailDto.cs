using System;
using System.Collections.Generic;
using Vespoli.Domain;

namespace Vespoli.Api.Dtos
{
    public class RowerForDetailDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public DateTime Created { get; set; }
        public string Introduction { get; set; }
        public string School { get; set; }
        public ICollection<WorkoutForDetailDto> Workouts { get; set; }
    }
}