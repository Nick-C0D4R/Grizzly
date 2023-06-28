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
    public class ActiveIngredientsController : ApiController
    {
        private ActiveIngredientService _service;
        private HttpResponseMessage _message;

        public ActiveIngredientsController(ActiveIngredientService service)
        {
            _service = service;
        }

        //GET: api/activeingredients
        public async Task<HttpResponseMessage> Get()
        {
            JArray array = new JArray();
            JObject result;
            try
            {
                _message = Request.CreateResponse(System.Net.HttpStatusCode.OK);
                var activeIngredients = await _service.GetAllAsync();
                if (activeIngredients.Count() == 0)
                {
                    _message = Request.CreateResponse(System.Net.HttpStatusCode.NoContent);
                    string json = @"{
                                        Count: 0,
                                        ActiveIngredients: []
                                    }";
                    result = JObject.Parse(json);
                }
                else
                {
                    result = new JObject();
                    foreach (var ingredient in activeIngredients)
                    {
                        array.Add(ingredient);
                    }
                    result["Count"] = array.Count;
                    result["ActiveIngredients"] = array;
                }
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch (Exception e)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
            }
            return _message;
        }

        // GET: api/activeingredients/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            JObject result;
            try
            {
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Found);
                var activeIngredient = _service.Get(id);
                if (activeIngredient == null)
                {
                    _message.StatusCode = System.Net.HttpStatusCode.NotFound;
                    string json = @"{
                                        ActiveIngredient: null
                                    }";
                    result = JObject.Parse(json);
                }
                else
                {
                    result = JObject.FromObject(activeIngredient);
                }
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch (Exception e)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
            }
            return _message;
        }

        public async Task<HttpResponseMessage> Post([FromBody] JObject value)
        {
            JObject result;
            try
            {
                var ingredient = JsonConvert.DeserializeObject<ActiveIngredientDTO>(value.ToString());
                ingredient = _service.Add(ingredient);
                result = JObject.FromObject(ingredient);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Created);
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch (Exception e)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
            }
            return _message;
        }

        public async Task<HttpResponseMessage> Put([FromBody] JObject value)
        {
            try
            {
                ActiveIngredientDTO activeIngredient = JsonConvert.DeserializeObject<ActiveIngredientDTO>(value.ToString());
                _service.Update(activeIngredient);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
                _message.Content = new StringContent(value.ToString(), Encoding.UTF8, "application/json");
            }
            catch (Exception e)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
            }
            return _message;
        }

        public async Task<HttpResponseMessage> Delete([FromBody] JObject value)
        {
            try
            {
                ActiveIngredientDTO ingredient = JsonConvert.DeserializeObject<ActiveIngredientDTO>(value.ToString());
                _service.Delete(ingredient);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
            }
            return _message;
        }
    }
}