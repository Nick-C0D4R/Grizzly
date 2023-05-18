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
    public class ApplicationsController : Controller
    {
        private ApplicationTypeService _service;

        public ApplicationsController(ApplicationTypeService service)
        {
            _service = service;
        }

        //GET: api/applications
        public async Task<JObject> Get()
        {
            var applications = await _service.GetAllAsync();

            JArray array = new JArray();
            try
            {
                foreach (var application in applications)
                {
                    array.Add(JToken.FromObject(application));
                }
                JObject result = new JObject();
                result["Applications"] = array;
                return result;
            }
            catch (Exception ex)
            {
                string respone = @"{
                                    Applications: []
                                    }";
                return JObject.FromObject(respone);
            }
        }

        // GET: api/applications/5
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
                                        Message: Applications type with id is not found
                                    }}";
                return JObject.FromObject(response);
            }
        }

        public async Task<JObject> Post([FromBody] JObject value)
        {
            DrugApplicationTypeDTO application = JsonConvert.DeserializeObject<DrugApplicationTypeDTO>(value.ToString());
            application = _service.Add(application);
            return JObject.FromObject(application);
        }

        public async Task<JObject> Put([FromBody] JObject value)
        {
            DrugApplicationTypeDTO application = JsonConvert.DeserializeObject<DrugApplicationTypeDTO>(value.ToString());
            _service.Update(application);
            return JObject.FromObject(application);
        }

        public async Task Delete([FromBody] JObject value)
        {
            DrugApplicationTypeDTO application = JsonConvert.DeserializeObject<DrugApplicationTypeDTO>(value.ToString());
            _service.Delete(application);
        }
    }
}