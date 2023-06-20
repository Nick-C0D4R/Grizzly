using BLL.DTO;
using BLL.Services;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using System.Linq;
using System.Data.Entity.Infrastructure;

namespace Grizzly.Server.Controllers
{
    public class OrdersController : ApiController
    {
        private OrderService _service;
        private HttpResponseMessage _message;

        public OrdersController(OrderService service)
        {
            _service = service;
        }

        //GET: api/orders
        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                var orders = await _service.GetAllAsync();
                JArray array = new JArray();
                JObject result;
                
                if(orders.Count() == 0)
                {
                    _message = Request.CreateResponse(System.Net.HttpStatusCode.NoContent);
                    string json = @"{
                                        Count: 0,
                                        Orders: []
                                    }";
                    result = JObject.Parse(json);
                }
                else
                {
                    foreach (var order in orders)
                    {
                        array.Add(order);
                    }
                    result = new JObject();
                    result["Count"] = array.Count;
                    result["Orders"] = array;
                    _message = Request.CreateResponse(System.Net.HttpStatusCode.OK);
                }
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch(Exception ex)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
            }
            return _message;
        }

        // GET: api/pharmacies/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            try
            {
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Found);
                OrderDTO order = _service.Get(id);
                JObject result;
                
                if(order == null)
                {
                    _message.StatusCode = System.Net.HttpStatusCode.NotFound;
                    string json = @"{
                                        Order: null
                                    }";
                    result = JObject.Parse(json);
                }
                else
                {
                    result = JObject.FromObject(order);
                }
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch(Exception e)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
            }
            return _message;
        }

        public async Task<HttpResponseMessage> Post([FromBody] JObject value)
        {
            try
            {
                var order = JsonConvert.DeserializeObject<OrderDTO>(value.ToString());
                order = _service.Add(order);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Created);
                _message.Content = new StringContent(value.ToString(), Encoding.UTF8, "application/json");
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
                var order = JsonConvert.DeserializeObject<OrderDTO>(value.ToString());
                _service.Update(order);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
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
                var order = JsonConvert.DeserializeObject<OrderDTO>(value.ToString());
                _service.Delete(order);
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