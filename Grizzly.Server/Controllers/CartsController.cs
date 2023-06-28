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
using System.Net.Http;
using System.Text;

namespace Grizzly.Server.Controllers
{
    public class CartsController : ApiController
    {
        private CartService _service;
        private HttpResponseMessage _message;

        public CartsController(CartService service)
        {
            _service = service;
        }

        //GET: api/carts
        public async Task<HttpResponseMessage> Get()
        {
            JArray array = new JArray();
            JObject result;

            try
            {
                var carts = await _service.GetAllAsync();

                _message = Request.CreateResponse(System.Net.HttpStatusCode.OK);

                if(carts.Count() == 0)
                {
                    _message.StatusCode = System.Net.HttpStatusCode.NoContent;
                    string json = @"{
                                        Count: 0,
                                        Carts: []
                                    }";
                    result = JObject.Parse(json);
                }
                else
                {
                    foreach(var cart in carts)
                    {
                        array.Add(cart);
                    }
                    result = new JObject();
                    result["Count"] = array.Count;
                    result["Carts"] = array;
                }
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "applicaion/json");
            }
            catch(Exception ex)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
            }
            return _message;
        }

        // GET: api/contraindications/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            JObject result;
            try
            {
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Found);
                var cart = _service.Get(id);
                if(cart == null)
                {
                    _message.StatusCode = System.Net.HttpStatusCode.NotFound;
                    string json = @"{
                                        Cart: null
                                    }";
                    result = JObject.Parse(json);
                }
                else
                {
                    result = JObject.FromObject(cart);
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
            JObject result;
            try
            {
                var cart = JsonConvert.DeserializeObject<CartDTO>(value.ToString());
                cart = _service.Add(cart);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Created);
                result = JObject.FromObject(cart);
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch(Exception ex )
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
            }
            return _message;
        }

        public async Task<HttpResponseMessage> Put([FromBody] JObject value)
        {
            try
            {
                CartDTO cart = JsonConvert.DeserializeObject<CartDTO>(value.ToString());
                _service.Update(cart);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
                _message.Content = new StringContent(value.ToString(), Encoding.UTF8, "application/json");
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
                CartDTO cart = JsonConvert.DeserializeObject<CartDTO>(value.ToString());
                _service.Delete(cart);
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