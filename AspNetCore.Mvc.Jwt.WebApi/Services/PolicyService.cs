﻿using AspNetCore.Mvc.Jwt.WebApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetCore.Mvc.Jwt.WebApi.Services
{
    public interface IPolicyService
    {
        Task<Policy> GetById(string id);

        Task<IEnumerable<Policy>> GetByClientId(string id);
    }

    public class PolicyService : IPolicyService
    {
        private const string POLICIES_URL = "http://www.mocky.io/v2/580891a4100000e8242b75c5";

        public async Task<IEnumerable<Policy>> GetAll()
        {
            using (var httpClient = new HttpClient())
            using(var response = await httpClient.GetAsync(POLICIES_URL))
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PoliciesDTO>(json).Policies;
            }
        }

        public async Task<IEnumerable<Policy>> GetByClientId(string clientId)
        {
            var policies = await this.GetAll();
            return policies.Where(x => x.ClientId == clientId);
        }

        public async Task<Policy> GetById(string policyId)
        {
            var policies = await this.GetAll();
            return policies.SingleOrDefault(x => x.Id == policyId);
        }
    }
}
