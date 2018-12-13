using System;

namespace Vespoli.Api.Dtos
{
    public class WorkoutForUpdateDto
    {
        public Double Distance { get; set; }
        public Double WorkoutTime { get; set; }
        public string Note { get; set; }
    }
}