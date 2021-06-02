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
    public interface ITrainingPlanService
    {
        Task<List<TrainingPlan>> GetTrainingPlans(string userId);
        Task<TrainingPlan> GetTrainingPlan(string userId, Guid trainingPlanId);
        Task<Guid> CreateTrainingPlan(string userId, TrainingPlan create);
        Task UpdateTrainingPlan(string userId, TrainingPlan update);
        Task DeleteTrainingPlan(string userId, Guid trainingPlanId);
    }

    public class TrainingPlanService : BasePacemeApiClient<TrainingPlan>, ITrainingPlanService
    {

        public TrainingPlanService(IHttpClientFactory clientFactory) : base(clientFactory){}

        public async Task<List<TrainingPlan>> GetTrainingPlans(string userId)
        {
            return await GetMany( "/api/user/"+userId+"/TrainingPlan");
        }

        public async Task<TrainingPlan> GetTrainingPlan(string userId, Guid trainingPlanId)
        {
            return await Get("/api/user/"+userId+"/TrainingPlan/"+trainingPlanId.ToString());
        }

        public async Task<Guid> CreateTrainingPlan(string userId, TrainingPlan create)
        {
            return await Create("/api/user/"+userId+"/TrainingPlan", create);
        }


        public async Task UpdateTrainingPlan(string userId, TrainingPlan update)
        {
            await Update("/api/user/"+userId+"/TrainingPlan/"+update.TrainingPlanId.ToString(), update);
        }

        public async Task DeleteTrainingPlan(string userId, Guid trainingPlanId)
        {
            await Delete("/api/user/"+userId+"/TrainingPlan/"+trainingPlanId.ToString());
        }
    }
}