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
    public class ContraindicationsController : Controller
    {
        private ContraIndicationService _service;

        public ContraindicationsController(ContraIndicationService service)
        {
            _service = service;
        }

        //GET: api/contraindications
        public async Task<JObject> Get()
        {
            var contraindications = await _service.GetAllAsync();

            JArray array = new JArray();
            try
            {
                foreach (var contraindication in contraindications)
                {
                    array.Add(JToken.FromObject(contraindication));
                }
                JObject result = new JObject();
                result["Contraindications"] = array;
                return result;
            }
            catch (Exception ex)
            {
                string respone = @"{
                                    Contraindications: []
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
                                        Message: Contraindication with with id is not found
                                    }}";
                return JObject.FromObject(response);
            }
        }

        public async Task<JObject> Post([FromBody] JObject value)
        {
            DrugContraIndicationDTO contraindication = JsonConvert.DeserializeObject<DrugContraIndicationDTO>(value.ToString());
            contraindication = _service.Add(contraindication);
            return JObject.FromObject(contraindication);
        }

        public async Task<JObject> Put([FromBody] JObject value)
        {
            DrugContraIndicationDTO contraindication = JsonConvert.DeserializeObject<DrugContraIndicationDTO>(value.ToString());
            _service.Update(contraindication);
            return JObject.FromObject(contraindication);
        }

        public async Task Delete([FromBody] JObject value)
        {
            DrugContraIndicationDTO contraindication = JsonConvert.DeserializeObject<DrugContraIndicationDTO>(value.ToString());
            _service.Delete(contraindication);
        }
    }
}