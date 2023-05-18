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
    public class PharmaciesController : Controller
    {
        private FarmacyOfficeService _service;

        public PharmaciesController(FarmacyOfficeService service)
        {
            _service = service;
        }

        //GET: api/pharmacies
        public async Task<JObject> Get()
        {
            var pharmacies = await _service.GetAllAsync();

            JArray array = new JArray();
            try
            {
                foreach (var pharmacy in pharmacies)
                {
                    array.Add(JToken.FromObject(pharmacy));
                }
                JObject result = new JObject();
                result["Pharmacies"] = array;
                return result;
            }
            catch (Exception ex)
            {
                string respone = @"{
                                    Pharmacies: []
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
                                        Message: Pharmacy with with id is not found
                                    }}";
                return JObject.FromObject(response);
            }
        }

        public async Task<JObject> Post([FromBody] JObject value)
        {
            FarmacyOfficeDTO farmacy = JsonConvert.DeserializeObject<FarmacyOfficeDTO>(value.ToString());
            farmacy = _service.Add(farmacy);
            return JObject.FromObject(farmacy);
        }

        public async Task<JObject> Put([FromBody] JObject value)
        {
            FarmacyOfficeDTO farmacy = JsonConvert.DeserializeObject<FarmacyOfficeDTO>(value.ToString());
            _service.Update(farmacy);
            return JObject.FromObject(farmacy);
        }

        public async Task Delete([FromBody] JObject value)
        {
            FarmacyOfficeDTO farmacy = JsonConvert.DeserializeObject<FarmacyOfficeDTO>(value.ToString());
            _service.Delete(farmacy);
        }
    }
}