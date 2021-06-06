using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PaceMe.BlazorApp.Properties;
using PaceMe.BlazorApp.Model;

namespace PaceMe.BlazorApp.Services
{

    public abstract class BasePacemeApiClient<T>
    {
        private readonly HttpClient _httpClient;

        public BasePacemeApiClient(IHttpClientFactory clientFactory){
            _httpClient = clientFactory.CreateClient(HttpClientNames.FunctionApi);
        }

        protected async Task<List<T>> GetMany(string url)
        {
            try
            {
                var result = await _httpClient.GetAsync(url);
                if(result.StatusCode == HttpStatusCode.BadRequest){
                    throw new InvalidOperationException(await result.Content.ReadAsStringAsync());
                }
                if(!result.IsSuccessStatusCode){
                    throw new HttpRequestException(await result.Content.ReadAsStringAsync());
                }
                return JsonConvert.DeserializeObject<List<T>>(await result.Content.ReadAsStringAsync());
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
                throw;
            }
        }

        protected async Task<T> Get(string url)
        {
            try
            {
                var result = await _httpClient.GetAsync(url);

                if(!result.IsSuccessStatusCode){
                    throw new HttpRequestException(await result.Content.ReadAsStringAsync());
                }
                return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
                throw;
            }
        }

        protected async Task<Guid> Create(string url, T create)
        {
            var stringPayload = JsonConvert.SerializeObject(create);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            try
            {
                var result = await _httpClient.PostAsync(url, httpContent);

                if(result.StatusCode == HttpStatusCode.BadRequest){
                    throw new InvalidOperationException(await result.Content.ReadAsStringAsync());
                }
                if(!result.IsSuccessStatusCode){
                    throw new HttpRequestException(await result.Content.ReadAsStringAsync());
                }
                return JsonConvert.DeserializeObject<Guid>(await result.Content.ReadAsStringAsync());
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
                throw;
            }
        }


        protected async Task Update(string url, T update)
        {
            var stringPayload = JsonConvert.SerializeObject(update);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            try
            {
                var result = await _httpClient.PutAsync(url, httpContent);

                if(!result.IsSuccessStatusCode){
                    throw new HttpRequestException(await result.Content.ReadAsStringAsync());
                };
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
                throw;
            }
        }

        protected async Task Delete(string url)
        {            
            try
            {
                var result = await _httpClient.DeleteAsync(url);

                if(!result.IsSuccessStatusCode){
                    throw new HttpRequestException(await result.Content.ReadAsStringAsync());
                };
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
                throw;
            }
        }
    }
}