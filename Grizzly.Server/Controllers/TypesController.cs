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
    public class TypesController : Controller
    {
        private DrugTypeService _service;

        public TypesController(DrugTypeService service)
        {
            _service = service;
        }

        //GET: api/types
        public async Task<JObject> Get()
        {
            var types = await _service.GetAllAsync();

            JArray array = new JArray();
            try
            {
                foreach (var type in types)
                {
                    array.Add(JToken.FromObject(type));
                }
                JObject result = new JObject();
                result["Types"] = array;
                return result;
            }
            catch (Exception ex)
            {
                string respone = @"{
                                    Types: []
                                    }";
                return JObject.FromObject(respone);
            }
        }

        // GET: api/types/5
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
                                        Message: Type with with id is not found
                                    }}";
                return JObject.FromObject(response);
            }
        }

        public async Task<JObject> Post([FromBody] JObject value)
        {
            DrugTypeDTO type = JsonConvert.DeserializeObject<DrugTypeDTO>(value.ToString());
            type = _service.Add(type);
            return JObject.FromObject(type);
        }

        public async Task<JObject> Put([FromBody] JObject value)
        {
            DrugTypeDTO type = JsonConvert.DeserializeObject<DrugTypeDTO>(value.ToString());
            _service.Update(type);
            return JObject.FromObject(type);
        }

        public async Task Delete([FromBody] JObject value)
        {
            DrugTypeDTO type = JsonConvert.DeserializeObject<DrugTypeDTO>(value.ToString());
            _service.Delete(type);
        }
    }
}