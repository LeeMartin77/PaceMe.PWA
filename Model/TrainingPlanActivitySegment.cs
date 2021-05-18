using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaceMe.BlazorApp.Model
{
    public class TrainingPlanActivitySegment : ITypedClone<TrainingPlanActivitySegment>
    {
        [Required]
        public Guid TrainingPlanActivityId { get; set; }
        public Guid TrainingPlanActivitySegmentId { get; set; }        
        public int Order { get; set; }
        public int DurationSeconds { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Notes are too long.")]
        public string Notes { get; set; }

        public TrainingPlanActivitySegment Clone()
        {
            return new TrainingPlanActivitySegment
            {
                TrainingPlanActivityId = TrainingPlanActivityId,
                TrainingPlanActivitySegmentId = TrainingPlanActivitySegmentId,
                Order = Order,
                DurationSeconds = DurationSeconds,
                Notes = Notes
            };
        }
    }
}
