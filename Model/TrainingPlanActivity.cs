using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PaceMe.BlazorApp.Model
{
    public class TrainingPlanActivity
    {
        [Required]
        public Guid TrainingPlanId { get; set; }
        public Guid TrainingPlanActivityId { get; set; }
        
        [Required]
        [StringLength(250, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        public bool Completed { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}
