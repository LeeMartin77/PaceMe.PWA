using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PaceMe.BlazorApp.Model
{
    public class ActivitySegment : ITypedClone<ActivitySegment>
    {
        [Required]
        public Guid ActivityId { get; set; }
        public Guid ActivitySegmentId { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        public int Order { get; set; }
        public int Repetitions { get; set; }
        public List<SegmentInterval> Intervals { get; set; } = new List<SegmentInterval>();


        public ActivitySegment Clone()
        {
            return new ActivitySegment
            {
                ActivityId = ActivityId,
                ActivitySegmentId = ActivitySegmentId,
                Order = Order,
                Name = Name,
                Repetitions = Repetitions,
                Intervals = Intervals.Select(i => i.Clone()).ToList()
            };
        }
    }

    public class SegmentInterval : ITypedClone<SegmentInterval> 
    {
        [Required]
        public Guid SegmentId { get; set; }
        public Guid SegmentIntervalId { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Notes are too long.")]
        public string Note { get; set; }
        public int Order { get; set; }

        //TODO: Introduce interval types
        public int IntervalType { get; } = 0;
        public int DurationSeconds { get; set; }

        //public int? DistanceMeters { get; set; }
        public SegmentInterval Clone()
        {
            return new SegmentInterval 
            {
                SegmentId = SegmentId,
                SegmentIntervalId = SegmentIntervalId,
                Note = Note,
                Order = Order,
                DurationSeconds = DurationSeconds
            };
        }
    }
}
