using System;

namespace Vespoli.Api.Dtos
{
    public class WorkoutForDetailDto
    {
        public int Id { get; set; }
        public DateTime WorkoutDate { get; set; }
        public Double Distance { get; set; }
        public Double WorkoutTime { get; set; }
        public string Note { get; set; }
    }
}