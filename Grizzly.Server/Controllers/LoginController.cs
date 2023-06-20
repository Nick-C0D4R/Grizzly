using BLL.DTO;
using BLL.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Grizzly.Server.Controllers
{
    public class LoginController : ApiController
    {
        private HttpResponseMessage _message;
        private UserService _service;

        public LoginController(UserService service)
        {
            _service = service;
        }

        public async Task<HttpResponseMessage> Login([FromBody]JObject value)
        {
            JObject result;
            try
            {
                var user = JsonConvert.DeserializeObject<UserDTO>(value.ToString());
                var users = await _service.GetAllAsync();
                if(users.Count() > 0)
                {
                    var userToFind = users.Where(x => x.Login == user.Login && x.Password == user.Password).First();
                    if (userToFind != null)
                    {
                        result = JObject.FromObject(userToFind);
                        _message = Request.CreateResponse(System.Net.HttpStatusCode.OK, Encoding.UTF8, "application/json");
                    }
                }
                _message = Request.CreateResponse(System.Net.HttpStatusCode.NotFound);
            }
            catch(Exception ex)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
            }
            return _message;
        }
    }
}