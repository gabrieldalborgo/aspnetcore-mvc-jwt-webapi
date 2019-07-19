using AspNetCore.Mvc.Jwt.WebApi.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetCore.Mvc.Jwt.WebApi.Services
{
    public interface IPolicyService
    {
        Task<Policy> GetByPolicyNumber(string policyNumber);

        Task<IEnumerable<Policy>> GetByUserId(string userId);
    }

    public class PolicyService : IPolicyService
    {
        private readonly HttpClient httpClient;
        private readonly string url;

        public PolicyService(HttpClient httpClient, IOptions<AppConfig> options)
        {
            this.httpClient = httpClient;
            this.url = options.Value.UrlPolicies;
        }

        public async Task<IEnumerable<Policy>> GetAll()
        {
            using(var response = await httpClient.GetAsync(url))
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PoliciesDTO>(json).Policies;
            }
        }

        public async Task<IEnumerable<Policy>> GetByUserId(string userId)
        {
            var policies = await this.GetAll();
            return policies.Where(x => x.ClientId == userId);
        }

        public async Task<Policy> GetByPolicyNumber(string policyNumber)
        {
            var policies = await this.GetAll();
            return policies.SingleOrDefault(x => x.Id == policyNumber);
        }
    }
}
