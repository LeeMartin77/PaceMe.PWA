﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PaceMe.BlazorApp.Model
{
    public class TrainingPlan : ITypedClone<TrainingPlan>
    {
        public Guid UserId { get; set; }
        public Guid TrainingPlanId { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        public bool Active { get; set; }

        public TrainingPlan Clone()
        {
            return new TrainingPlan 
            {
                UserId = UserId,
                TrainingPlanId = TrainingPlanId,
                Name = Name,
                Active = Active
            };
        }
    }
}
