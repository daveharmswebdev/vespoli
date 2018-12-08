using System;
using System.ComponentModel.DataAnnotations;
using Vespoli.Api.Helpers;

namespace Vespoli.Api.Dtos
{
    public class WorkoutForAddDto
    {
        [Required]
        [CustomDateRange]
        public DateTime WorkoutDate { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]
        public Double Distance { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]
        public Double WorkoutTime { get; set; }

        public string Note { get; set; }

        [Required]
        public int RowerId { get; set; }
    }
}