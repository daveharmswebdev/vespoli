using System;

namespace Vespoli.Domain
{
    public class Workout
    {
        public int Id { get; set; }
        public DateTime WorkoutDate { get; set; }
        public Double Distance { get; set; }
        public Double WorkoutTime { get; set; }
        public string Note { get; set; }
        public Rower Rower { get; set; }
        public int RowerId { get; set; }
    }
}
