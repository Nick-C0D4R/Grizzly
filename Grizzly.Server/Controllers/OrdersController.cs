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
    public class OrdersController : Controller
    {
        private OrderService _service;

        public OrdersController(OrderService service)
        {
            _service = service;
        }

        //GET: api/orders
        public async Task<JObject> Get()
        {
            var orders = await _service.GetAllAsync();

            JArray array = new JArray();
            try
            {
                foreach (var order in orders)
                {
                    array.Add(JToken.FromObject(order));
                }
                JObject result = new JObject();
                result["Orders"] = array;
                return result;
            }
            catch (Exception ex)
            {
                string respone = @"{
                                    Orders: []
                                    }";
                return JObject.FromObject(respone);
            }
        }

        // GET: api/pharmacies/5
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
                                        Message: Order with with id is not found
                                    }}";
                return JObject.FromObject(response);
            }
        }

        public async Task<JObject> Post([FromBody] JObject value)
        {
            OrderDTO order = JsonConvert.DeserializeObject<OrderDTO>(value.ToString());
            order = _service.Add(order);
            return JObject.FromObject(order);
        }

        public async Task<JObject> Put([FromBody] JObject value)
        {
            OrderDTO order = JsonConvert.DeserializeObject<OrderDTO>(value.ToString());
            _service.Update(order);
            return JObject.FromObject(order);
        }

        public async Task Delete([FromBody] JObject value)
        {
            OrderDTO order = JsonConvert.DeserializeObject<OrderDTO>(value.ToString());
            _service.Delete(order);
        }
    }
}