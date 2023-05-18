using BLL.DTO;
using BLL.Services;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Grizzly.Server.Controllers
{
    public class CartsController : Controller
    {
        private CartService _service;

        public CartsController(CartService service)
        {
            _service = service;
        }

        //GET: api/carts
        public async Task<JObject> Get()
        {
            var carts = await _service.GetAllAsync();

            JArray array = new JArray();
            try
            {
                foreach (var cart in carts)
                {
                    array.Add(JToken.FromObject(cart));
                }
                JObject result = new JObject();
                result["Carts"] = array;
                return result;
            }
            catch (Exception ex)
            {
                string respone = @"{
                                    Carts: []
                                    }";
                return JObject.FromObject(respone);
            }
        }

        // GET: api/contraindications/5
        public async Task<JObject> Get(int id)
        {
            try
            {
                return await Task.Run(() => JObject.FromObject(_service.Get(id)));
            }
            catch (Exception ex)
            {
                string response = @"
                                    {{
                                        Status Code: 404 Not Found,
                                        Message: Cart with with id is not found
                                    }}";
                return JObject.FromObject(response);
            }
        }

        public async Task<JObject> Post([FromBody] JObject value)
        {
            CartDTO cart = JsonConvert.DeserializeObject<CartDTO>(value.ToString());
            cart = _service.Add(cart);
            return JObject.FromObject(cart);
        }

        public async Task<JObject> Put([FromBody] JObject value)
        {
            CartDTO cart = JsonConvert.DeserializeObject<CartDTO>(value.ToString());
            _service.Update(cart);
            return JObject.FromObject(cart);
        }

        public async Task Delete([FromBody] JObject value)
        {
            CartDTO cart = JsonConvert.DeserializeObject<CartDTO>(value.ToString());
            _service.Delete(cart);
        }
    }
}