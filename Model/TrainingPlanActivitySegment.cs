using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaceMe.BlazorApp.Model
{
    public class TrainingPlanActivitySegment
    {
        [Required]
        public Guid TrainingPlanActivityId { get; set; }
        public Guid TrainingPlanActivitySegmentId { get; set; }        
        public int Order { get; set; }
        public int DurationSeconds { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Notes are too long.")]
        public string Notes { get; set; }
    }
}
