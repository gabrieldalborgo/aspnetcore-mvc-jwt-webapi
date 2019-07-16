using AspNetCore.Mvc.Jwt.WebApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetCore.Mvc.Jwt.WebApi.Services
{
    public interface IClientService
    {
        Task<Client> GetById(string id);

        Task<Client> GetByName(string name);
    }

    public class ClientService : IClientService
    {
        private const string CLIENTS_URL = "http://www.mocky.io/v2/5808862710000087232b75ac";

        public async Task<IEnumerable<Client>> GetAll()
        {
            using (var httpClient = new HttpClient())
            using(var response = await httpClient.GetAsync(CLIENTS_URL))
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ClientsDTO>(json).Clients;
            }
        }

        public async Task<Client> GetById(string id)
        {
            var clients = await this.GetAll();
            return clients.SingleOrDefault(x => x.Id == id);
        }

        public async Task<Client> GetByName(string name)
        {
            var clients = await this.GetAll();
            return clients.SingleOrDefault(x => x.Name == name);
        }
    }
}
