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
        public UsersController(UserService service)
        {
            _service = service;
        }
        
        //GET: api/users
        public async Task<HttpResponseMessage> Get()
        {
            var users = await _service.GetAllAsync();
            HttpResponseMessage message;
            JArray array = new JArray();

            if(users.Count() == 0)
            {
                message = Request.CreateResponse(System.Net.HttpStatusCode.OK);
                string json = @"{
                                    Count: 0,
                                    Users: []
                                }";
                JObject obj = JObject.Parse(json);
                message.Content = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");
                return message;
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
                message = Request.CreateResponse(System.Net.HttpStatusCode.OK);
                message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");

            }
            catch(Exception ex)
            {
                message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
                message.Content = new StringContent(JObject.Parse(ex.Message).ToString(), Encoding.UTF8, "application/json");
            }
            return message;
        }
        
        // GET: api/users/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            HttpResponseMessage message;
            JObject result;
            try
            {
                message = Request.CreateResponse(System.Net.HttpStatusCode.OK);
                //message.Headers.Add("Content-Type", "application/json");
                var user = _service.Get(id);
                if(user == null)
                {
                    string json = @"{
                                        User : null
                                    }";
                    result = JObject.Parse(json);
                }
                else
                {
                    result = JObject.FromObject(user);
                }
                message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch(Exception ex)
            {
                message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
                message.Content = new StringContent(JObject.Parse(ex.Message).ToString(), Encoding.UTF8, "application/json");
            }
            return message;
        }
        public async Task<HttpResponseMessage> Post([FromBody] JObject value)
        {
            HttpResponseMessage message;
            try
            {
                UserDTO user = JsonConvert.DeserializeObject<UserDTO>(value.ToString());
                user = _service.Add(user);
                message = Request.CreateResponse(System.Net.HttpStatusCode.OK);
                JObject result = JObject.FromObject(user);
                message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch(Exception ex)
            {
                message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
                message.Content = new StringContent(JObject.Parse(ex.Message).ToString(), Encoding.UTF8, "application/json");
            }

            return message;
        }
        public async Task<JObject> Put([FromBody] JObject value)
        {
            UserDTO user = JsonConvert.DeserializeObject<UserDTO>(value.ToString());
            _service.Update(user);
            return JObject.FromObject(user);
        }
        public async Task Delete([FromBody] JObject value)
        {
            UserDTO user = JsonConvert.DeserializeObject<UserDTO>(value.ToString());
            _service.Delete(user);
        }
    }
}