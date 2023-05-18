using BLL.DTO;
using BLL.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Web.Http;
namespace Grizzly.Server.Controllers
{
    public class RolesController : ApiController
    {
        private RoleService _service;

        public RolesController(RoleService service)
        {
            _service = service;
        }

        //GET: api/roles
        public async Task<JObject> Get()
        {

            var roles = await _service.GetAllAsync();
            JArray array = new JArray();
            try
            {
                foreach (var role in roles)
                {
                    array.Add(JToken.FromObject(role));
                }
                JObject result = new JObject();
                result["Roles"] = array;
                return result;
            }
            catch(Exception ex)
            {
                string respone = @"{
                                    Roles: []
                                    }";
                return JObject.Parse(respone);
            }
        }

        // GET: api/roles/5
        public async Task<JObject> Get(int id)
        {
            try
            {
                return await Task.Run(() => JObject.FromObject(_service.Get(id)));
            }
            catch(Exception ex)
            {
                return JObject.FromObject(ex);
            }
        }

        public async Task<JObject> Post([FromBody] JObject value)
        {
            RoleDTO role = JsonConvert.DeserializeObject<RoleDTO>(value.ToString());
            role = _service.Add(role);
            return JObject.FromObject(role);
        }

        public async Task<JObject> Put([FromBody] JObject value)
        {
            RoleDTO role = JsonConvert.DeserializeObject<RoleDTO>(value.ToString());
            _service.Update(role);
            return JObject.FromObject(role);
        }

        public async Task Delete([FromBody] JObject value)
        {
            RoleDTO role = JsonConvert.DeserializeObject<RoleDTO>(value.ToString());
            _service.Delete(role);
        }
    }
}