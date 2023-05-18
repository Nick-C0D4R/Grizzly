using BLL.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Grizzly.Server.Controllers
{
    public class RegisterController : Controller
    {
        private UsersController controller;

        public RegisterController(UsersController controller)
        {
            this.controller = controller;
        }

        // POST: api/register
        public async Task<JObject> Post([FromBody] JObject value)
        {
            var usersJson = await controller.Get();
            var users = JsonConvert.DeserializeObject<List<UserDTO>>(usersJson.ToString());
            var loggedUser = JsonConvert.DeserializeObject<UserDTO>(value.ToString());
            UserDTO user = users.Find(x => x.Login == loggedUser.Login && x.Password == loggedUser.Password);
            if (user == null)
            {
                loggedUser = JsonConvert.DeserializeObject<UserDTO>((await controller.Post(value)).ToString());
                return JObject.FromObject(loggedUser);
            }
            string response = @"{
                                    Status code : 400 Bad Request,
                                    Message : This user exists already
                                }";
            return JObject.FromObject(response);
        }
    }
}