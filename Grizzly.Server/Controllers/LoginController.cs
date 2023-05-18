using BLL.DTO;
using Grizzly.ConnectionManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Grizzly.Server.Controllers
{
    public class LoginController : Controller
    {
        private UsersController controller;

        public LoginController(UsersController controller)
        {
            this.controller = controller;
        }

        //POST api/login
        public async Task<JObject> Post([FromBody] JObject value)
        {
            var usersJson = await controller.Get();
            var users = JsonConvert.DeserializeObject<List<UserDTO>>(usersJson.ToString());
            var loggedUser = JsonConvert.DeserializeObject<UserDTO>(value.ToString());
            UserDTO user = users.Find(x => x.Login == loggedUser.Login && x.Password == loggedUser.Password);
            if (user != null)
            {
                return JObject.FromObject(user);
            }
            string response = @"{
                                Status code : 404 Not Found,
                                Message :  Invalid login or password
                                }";
            return JObject.FromObject(response);
        }
    }
}