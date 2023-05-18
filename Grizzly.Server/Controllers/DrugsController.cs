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
    public class DrugsController : Controller
    {
        private DrugService _service;

        public DrugsController(DrugService service)
        {
            _service = service;
        }

        //GET: api/drugs
        public async Task<JObject> Get()
        {
            var drugs = await _service.GetAllAsync();

            JArray array = new JArray();
            try
            {
                foreach (var drug in drugs)
                {
                    array.Add(JToken.FromObject(drug));
                }
                JObject result = new JObject();
                result["Drugs"] = array;
                return result;
            }
            catch (Exception ex)
            {
                string respone = @"{
                                    Drugs: []
                                    }";
                return JObject.FromObject(respone);
            }
        }

        // GET: api/drugs/5
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
                                        Message: Drug with with id is not found
                                    }}";
                return JObject.FromObject(response);
            }
        }

        public async Task<JObject> Post([FromBody] JObject value)
        {
            DrugDTO drug = JsonConvert.DeserializeObject<DrugDTO>(value.ToString());
            drug = _service.Add(drug);
            return JObject.FromObject(drug);
        }

        public async Task<JObject> Put([FromBody] JObject value)
        {
            DrugDTO drug = JsonConvert.DeserializeObject<DrugDTO>(value.ToString());
            _service.Update(drug);
            return JObject.FromObject(drug);
        }

        public async Task Delete([FromBody] JObject value)
        {
            DrugDTO drug = JsonConvert.DeserializeObject<DrugDTO>(value.ToString());
            _service.Delete(drug);
        }
    }
}