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
    public class IndicationsController : Controller
    {
        private DrugIndicationService _service;

        public IndicationsController(DrugIndicationService service)
        {
            _service = service;
        }

        //GET: api/indications
        public async Task<JObject> Get()
        {
            var indications = await _service.GetAllAsync();

            JArray array = new JArray();
            try
            {
                foreach (var indication in indications)
                {
                    array.Add(JToken.FromObject(indication));
                }
                JObject result = new JObject();
                result["Indications"] = array;
                return result;
            }
            catch (Exception ex)
            {
                string respone = @"{
                                    Indications: []
                                    }";
                return JObject.FromObject(respone);
            }
        }

        // GET: api/indications/5
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
                                        Message: Indication with with id is not found
                                    }}";
                return JObject.FromObject(response);
            }
        }

        public async Task<JObject> Post([FromBody] JObject value)
        {
            DrugIndicationDTO indication = JsonConvert.DeserializeObject<DrugIndicationDTO>(value.ToString());
            indication = _service.Add(indication);
            return JObject.FromObject(indication);
        }

        public async Task<JObject> Put([FromBody] JObject value)
        {
            DrugIndicationDTO indication = JsonConvert.DeserializeObject<DrugIndicationDTO>(value.ToString());
            _service.Update(indication);
            return JObject.FromObject(indication);
        }

        public async Task Delete([FromBody] JObject value)
        {
            DrugIndicationDTO indication = JsonConvert.DeserializeObject<DrugIndicationDTO>(value.ToString());
            _service.Delete(indication);
        }
    }
}