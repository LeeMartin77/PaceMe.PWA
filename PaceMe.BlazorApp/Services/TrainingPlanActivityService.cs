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
    public interface ITrainingPlanActivityService
    {
        Task<List<TrainingPlanActivity>> GetTrainingPlanActivities(string userId, Guid TrainingPlanId);
        Task<TrainingPlanActivity> GetTrainingPlanActivity(string userId, Guid TrainingPlanId, Guid TrainingPlanActivityId);
        Task<Guid> CreateTrainingPlanActivity(string userId, Guid TrainingPlanId, TrainingPlanActivity create);
        Task UpdateTrainingPlanActivity(string userId, Guid TrainingPlanId, TrainingPlanActivity update);
        Task DeleteTrainingPlanActivity(string userId, Guid TrainingPlanId, Guid TrainingPlanActivityId);
    }

    public class TrainingPlanActivityService : BasePacemeApiClient<TrainingPlanActivity>, ITrainingPlanActivityService
    {

        public TrainingPlanActivityService(IHttpClientFactory clientFactory) : base(clientFactory){}

        public async Task<List<TrainingPlanActivity>> GetTrainingPlanActivities(string userId, Guid TrainingPlanId)
        {
            return await GetMany("/api/user/"+userId+"/TrainingPlan/"+TrainingPlanId.ToString()+"/Activity");
        }

        public async Task<TrainingPlanActivity> GetTrainingPlanActivity(string userId, Guid TrainingPlanId, Guid TrainingPlanActivityId)
        {
            return await Get("/api/user/"+userId+"/TrainingPlan/"+TrainingPlanId.ToString()+"/Activity/"+TrainingPlanActivityId.ToString());
        }

        public async Task<Guid> CreateTrainingPlanActivity(string userId, Guid TrainingPlanId, TrainingPlanActivity create)
        {
            return await Create("/api/user/"+userId+"/TrainingPlan/"+TrainingPlanId.ToString()+"/Activity", create);
        }


        public async Task UpdateTrainingPlanActivity(string userId, Guid TrainingPlanId, TrainingPlanActivity update)
        {
            await Update("/api/user/"+userId+"/TrainingPlan/"+TrainingPlanId.ToString()+"/Activity/"+update.TrainingPlanActivityId.ToString(), update);
        }

        public async Task DeleteTrainingPlanActivity(string userId, Guid TrainingPlanId, Guid TrainingPlanActivityId)
        {
            await Delete("/api/user/"+userId+"/TrainingPlan/"+TrainingPlanId.ToString()+"/Activity/"+TrainingPlanActivityId.ToString());
        }
    }
}