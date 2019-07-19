using AspNetCore.Mvc.Jwt.WebApi.Models;
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
        private const string USERS_URL = "http://www.mocky.io/v2/5808862710000087232b75ac";

        public async Task<IEnumerable<User>> GetAll()
        {
            using (var httpClient = new HttpClient())
            using(var response = await httpClient.GetAsync(USERS_URL))
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
