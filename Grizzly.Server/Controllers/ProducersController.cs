using BLL.DTO;
using BLL.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
namespace Grizzly.Server.Controllers
{
    public class ProducersController : ApiController
    {
        private ProducerService _service;
        public ProducersController(ProducerService service)
        {
            _service = service;
        }

        //GET: api/producers
        public async Task<JObject> Get()
        {
            var producers = await _service.GetAllAsync();
            JArray array = new JArray();
            try
            {
                foreach (var producer in producers)
                {
                    array.Add(JToken.FromObject(producer));
                }
                JObject result = new JObject();
                result["Producers"] = array;
                return result;
            }
            catch(Exception ex)
            {
                string respone = @"{
                                    Producers: []
                                    }";
                return JObject.FromObject(respone);
            }

        }

        // GET: api/producers/5
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
                                        Message: Producer with with id is not found
                                    }}";
                return JObject.FromObject(response);
            }
        }
        //POST api/prudcers
        public async Task<JObject> Post([FromBody] JObject value)
        {
            ProducerDTO producer = JsonConvert.DeserializeObject<ProducerDTO>(value.ToString());
            producer = _service.Add(producer); return JObject.FromObject(producer);
        }
        public async Task<JObject> Put([FromBody] JObject value)
        {
            ProducerDTO producer = JsonConvert.DeserializeObject<ProducerDTO>(value.ToString());
            _service.Update(producer);
            return JObject.FromObject(producer);
        }
        public async Task Delete([FromBody] JObject value)
        {
            ProducerDTO producer = JsonConvert.DeserializeObject<ProducerDTO>(value.ToString());
            _service.Delete(producer);
        }
    }
}