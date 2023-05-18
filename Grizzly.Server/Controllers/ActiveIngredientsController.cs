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
    public class ActiveIngredientsController : Controller
    {
        private ActiveIngredientService _service;

        public ActiveIngredientsController(ActiveIngredientService service)
        {
            _service = service;
        }

        //GET: api/activeingredients
        public async Task<JObject> Get()
        {
            var activeIngredients = await _service.GetAllAsync();

            JArray array = new JArray();
            try
            {
                foreach (var activeIngredient in activeIngredients)
                {
                    array.Add(JToken.FromObject(activeIngredient));
                }
                JObject result = new JObject();
                result["ActiveIngredients"] = array;
                return result;
            }
            catch (Exception ex)
            {
                string respone = @"{
                                    ActiveIngredients: []
                                    }";
                return JObject.FromObject(respone);
            }
        }

        // GET: api/activeingredients/5
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
                                        Message: Active ingredients with id is not found
                                    }}";
                return JObject.FromObject(response);
            }
        }

        public async Task<JObject> Post([FromBody] JObject value)
        {
            ActiveIngredientDTO activeIngredient = JsonConvert.DeserializeObject<ActiveIngredientDTO>(value.ToString());
            activeIngredient = _service.Add(activeIngredient);
            return JObject.FromObject(activeIngredient);
        }

        public async Task<JObject> Put([FromBody] JObject value)
        {
            ActiveIngredientDTO activeIngredient = JsonConvert.DeserializeObject<ActiveIngredientDTO>(value.ToString());
            _service.Update(activeIngredient);
            return JObject.FromObject(activeIngredient);
        }

        public async Task Delete([FromBody] JObject value)
        {
            ActiveIngredientDTO activeIngredient = JsonConvert.DeserializeObject<ActiveIngredientDTO>(value.ToString());
            _service.Delete(activeIngredient);
        }
    }
}