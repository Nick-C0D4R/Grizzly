using BLL.DTO;
using BLL.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
namespace Grizzly.Server.Controllers
{
    public class RolesController : ApiController
    {
        private RoleService _service;
        private HttpResponseMessage _message;

        public RolesController(RoleService service)
        {
            _service = service;
        }

        //GET: api/roles
        public async Task<HttpResponseMessage> Get()
        {

            var roles = await _service.GetAllAsync();
            JArray array = new JArray();
            
            if(roles.Count() == 0)
            {
                _message = Request.CreateResponse(System.Net.HttpStatusCode.NoContent);
                string json = @"{
                                    Count: 0,
                                    Roles: []
                                }";
                JObject obj = JObject.Parse(json);
                _message.Content = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");
                return _message;
            }
            try
            {
                foreach (var role in roles)
                {
                    array.Add(JToken.FromObject(role));
                }
                JObject result = new JObject();
                result["Count"] = array.Count;
                result["Roles"] = array;
                _message = Request.CreateResponse(System.Net.HttpStatusCode.OK);
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");

            }
            catch (Exception ex)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
                _message.Content = new StringContent(JObject.Parse(ex.Message).ToString(), Encoding.UTF8, "application/json");
            }
            return _message;
        }

        // GET: api/roles/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            JObject result;
            try
            {
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Found);
                var role = _service.Get(id);
                if(role == null)
                {
                    _message.StatusCode = System.Net.HttpStatusCode.NotFound;
                    string json = @"{
                                        Role: null
                                    }";
                    result = JObject.Parse(json);
                }
                else
                {
                    result = JObject.FromObject(role);
                }
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch(Exception e)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
                _message.Content = new StringContent(JObject.Parse(e.Message).ToString(), Encoding.UTF8, "application/json");
            }
            return _message;
        }

        public async Task<HttpResponseMessage> Post([FromBody] JObject value)
        {
            try
            {
                RoleDTO role = JsonConvert.DeserializeObject<RoleDTO>(value.ToString());
                role = _service.Add(role);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Created);
                JObject result = JObject.FromObject(role);
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch(Exception e)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
            }
            return _message;
        }

        public async Task<HttpResponseMessage> Put([FromBody] JObject value)
        {
            try
            {
                RoleDTO role = JsonConvert.DeserializeObject<RoleDTO>(value.ToString());
                _service.Update(role);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
                _message.Content = new StringContent(JObject.FromObject(role).ToString(), Encoding.UTF8, "application/json");
            }
            catch(Exception e)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
            }
            return _message;
        }

        public async Task<HttpResponseMessage> Delete([FromBody] JObject value)
        {
            try
            {
                RoleDTO role = JsonConvert.DeserializeObject<RoleDTO>(value.ToString());
                _service.Delete(role);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch(Exception e)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
            }
            return _message;
        }
    }
}