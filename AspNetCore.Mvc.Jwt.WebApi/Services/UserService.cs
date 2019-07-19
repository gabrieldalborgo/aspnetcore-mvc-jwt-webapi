using AspNetCore.Mvc.Jwt.WebApi.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetCore.Mvc.Jwt.WebApi.Services
{
    public interface IUserService
    {
        Task<User> GetById(string userId);

        Task<User> GetByName(string userName);
    }

    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;
        private readonly string url;

        public UserService(HttpClient httpClient, IOptions<AppConfig> options)
        {
            this.httpClient = httpClient;
            this.url = options.Value.UrlUsers;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            using(var response = await httpClient.GetAsync(url))
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UsersDTO>(json).Users;
            }
        }

        public async Task<User> GetById(string userId)
        {
            var users = await this.GetAll();
            return users.SingleOrDefault(x => x.Id == userId);
        }

        public async Task<User> GetByName(string userName)
        {
            var users = await this.GetAll();
            return users.SingleOrDefault(x => x.Name == userName);
        }
    }
}
