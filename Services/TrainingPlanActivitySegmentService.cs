using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PaceMe.BlazorApp.Properties;
using PaceMe.BlazorApp.Model;

namespace PaceMe.BlazorApp.Services
{
    public interface ITrainingPlanActivitySegmentService
    {
        Task<List<ActivitySegment>> GetTrainingPlanActivitySegments(string userId, Guid TrainingPlanId, Guid TrainingPlanActivityId);
        Task<ActivitySegment> GetTrainingPlanActivitySegment(string userId, Guid TrainingPlanId, Guid TrainingPlanActivityId, Guid TrainingPlanActivitySegmentId);
        Task<Guid> CreateTrainingPlanActivitySegment(string userId, Guid TrainingPlanId, Guid TrainingPlanActivityId, ActivitySegment create);
        Task UpdateTrainingPlanActivitySegment(string userId, Guid TrainingPlanId, Guid TrainingPlanActivityId, ActivitySegment update);
        Task DeleteTrainingPlanActivitySegment(string userId, Guid TrainingPlanId, Guid TrainingPlanActivityId, Guid TrainingPlanActivitySegmentId);
    }

    public class TrainingPlanActivitySegmentService : BasePacemeApiClient<ActivitySegment>, ITrainingPlanActivitySegmentService
    {
        public TrainingPlanActivitySegmentService(IHttpClientFactory clientFactory) : base(clientFactory){}

        public async Task<List<ActivitySegment>> GetTrainingPlanActivitySegments(string userId, Guid TrainingPlanId, Guid TrainingPlanActivityId)
        {
            return await GetMany($"/api/user/{userId}/TrainingPlan/{TrainingPlanId}/Activity/{TrainingPlanActivityId}/segment");
        }

        public async Task<ActivitySegment> GetTrainingPlanActivitySegment(string userId, Guid TrainingPlanId, Guid TrainingPlanActivityId, Guid TrainingPlanActivitySegmentId)
        {
            return await Get($"/api/user/{userId}/TrainingPlan/{TrainingPlanId}/Activity/{TrainingPlanActivityId}/segment/{TrainingPlanActivitySegmentId}");
        }

        public async Task<Guid> CreateTrainingPlanActivitySegment(string userId, Guid TrainingPlanId, Guid TrainingPlanActivityId, ActivitySegment create)
        {
            return await Create($"/api/user/{userId}/TrainingPlan/{TrainingPlanId}/Activity/{TrainingPlanActivityId}/segment", create);
        }

        public async Task UpdateTrainingPlanActivitySegment(string userId, Guid TrainingPlanId, Guid TrainingPlanActivityId, ActivitySegment update)
        {
            await Update($"/api/user/{userId}/TrainingPlan/{TrainingPlanId}/Activity/{TrainingPlanActivityId}/segment/{update.ActivitySegmentId}", update);
        }

        public async Task DeleteTrainingPlanActivitySegment(string userId, Guid TrainingPlanId, Guid TrainingPlanActivityId, Guid TrainingPlanActivitySegmentId)
        {
            await Delete($"/api/user/{userId}/TrainingPlan/{TrainingPlanId}/Activity/{TrainingPlanActivityId}/segment/{TrainingPlanActivitySegmentId}");
        }
    }
}