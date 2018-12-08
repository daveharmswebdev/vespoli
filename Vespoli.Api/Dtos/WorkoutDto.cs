using System;
using System.Collections.Generic;
using Vespoli.Domain;

namespace Vespoli.Api.Dtos
{
    public class WorkoutDto
    {
        public int Id { get; set; }
        public DateTime WorkoutDate { get; set; }
        public Double Distance { get; set; }
        public Double WorkoutTime { get; set; }
        public string Note { get; set; }
        public int RowerId { get; set; }
        public RowerForWorkoutDto Rower { get; set; }
    }
}