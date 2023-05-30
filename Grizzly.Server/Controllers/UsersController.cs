using BLL.DTO;
using BLL.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
namespace Grizzly.Server.Controllers
{
    public class UsersController : ApiController
    {
        private UserService _service;
        private HttpResponseMessage _message;

        public UsersController(UserService service)
        {
            _service = service;
        }
        
        //GET: api/users
        public async Task<HttpResponseMessage> Get()
        {
            var users = await _service.GetAllAsync();
            JArray array = new JArray();

            if(users.Count() == 0)
            {
                _message = Request.CreateResponse(System.Net.HttpStatusCode.NoContent);
                string json = @"{
                                    Count: 0,
                                    Users: []
                                }";
                JObject obj = JObject.Parse(json);
                _message.Content = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");
                return _message;
            }
            try
            {
                foreach (var user in users)
                {
                    array.Add(JToken.FromObject(user));
                }
                JObject result = new JObject();
                result["Count"] = array.Count;
                result["Users"] = array;
                _message = Request.CreateResponse(System.Net.HttpStatusCode.OK);
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");

            }
            catch(Exception ex)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
            }
            return _message;
        }
        
        // GET: api/users/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            JObject result;
            try
            {
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Found);
                var user = _service.Get(id);
                if(user == null)
                {
                    _message.StatusCode = System.Net.HttpStatusCode.NotFound;
                    string json = @"{
                                        User : null
                                    }";
                    result = JObject.Parse(json);
                }
                else
                {
                    result = JObject.FromObject(user);
                }
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch(Exception ex)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
            }
            return _message;
        }
        public async Task<HttpResponseMessage> Post([FromBody] JObject value)
        {
            try
            {
                UserDTO user = JsonConvert.DeserializeObject<UserDTO>(value.ToString());
                user = _service.Add(user);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Created);
                JObject result = JObject.FromObject(user);
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch(Exception ex)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
            }

            return _message;
        }
        public async Task<HttpResponseMessage> Put([FromBody] JObject value)
        {
            try
            {
                UserDTO user = JsonConvert.DeserializeObject<UserDTO>(value.ToString());
                _service.Update(user);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
                _message.Content = new StringContent(JObject.FromObject(user).ToString(), Encoding.UTF8, "application/json");
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
                UserDTO user = JsonConvert.DeserializeObject<UserDTO>(value.ToString());
                _service.Delete(user);
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