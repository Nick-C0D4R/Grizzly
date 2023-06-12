using BLL.DTO;
using BLL.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Grizzly.Server.Controllers
{
    public class RegisterController : ApiController
    {
        private UserService _service;
        private HttpResponseMessage _message;

        public RegisterController(UserService service)
        {
            _service = service;
        }


        // POST: api/register
        public async Task<HttpResponseMessage> Post([FromBody] JObject value)
        {
            try
            {
                var signupUsers = await _service.GetAllAsync();
                UserDTO user = JsonConvert.DeserializeObject<UserDTO>(value.ToString());
                if (signupUsers.Contains(user))
                {
                    _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.Forbidden, new ArgumentException("User with this login and password is already exists"));
                    return _message;
                }
                user.JoinDate = DateTime.Now;
                user.RoleId = 2;
                _service.Add(user);
                JObject result = JObject.FromObject(user);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Created);
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch(Exception ex)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
            }
            return _message;
        }
    }
}