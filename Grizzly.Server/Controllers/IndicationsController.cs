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
    public class IndicationsController : ApiController
    {
        private DrugIndicationService _service;
        private HttpResponseMessage _message;

        public IndicationsController(DrugIndicationService service)
        {
            _service = service;
        }

        //GET: api/indications
        public async Task<HttpResponseMessage> Get()
        {
            JArray array = new JArray();
            JObject result;
            try
            {
                var indications = await _service.GetAllAsync();

                if(indications.Count() == 0)
                {
                    _message = Request.CreateResponse(System.Net.HttpStatusCode.NoContent);
                    string json = @"{
                                    Count:0,
                                    Indications: []
                                }";
                    result = JObject.Parse(json);
                    _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
                    return _message;
                }

                foreach (var indication in indications)
                {
                    array.Add(JToken.FromObject(indication));
                }
                result = new JObject();
                result["Count"] = array.Count;
                result["Indications"] = array;
                _message = Request.CreateResponse(System.Net.HttpStatusCode.OK);
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch (Exception ex)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
            }
            return _message;
        }

        // GET: api/indications/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            JObject result;
            try
            {
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Found);
                var indication = _service.Get(id);
                if(indication == null)
                {
                    _message.StatusCode = System.Net.HttpStatusCode.NotFound;
                    string json = @"{
                                        Indication: null
                                    }";
                    result = JObject.Parse(json);
                }
                else
                {
                    result = JObject.FromObject(indication);
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
                DrugIndicationDTO indication = JsonConvert.DeserializeObject<DrugIndicationDTO>(value.ToString());
                indication = _service.Add(indication);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Created);
                result = JObject.FromObject(indication);
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch(Exception e)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
            }
            return _message;
        }

        public async Task<HttpResponseMessage> Put([FromBody] JObject value)
        {
            try
            {
                DrugIndicationDTO indication = JsonConvert.DeserializeObject<DrugIndicationDTO>(value.ToString());
                _service.Update(indication);
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
                DrugIndicationDTO indication = JsonConvert.DeserializeObject<DrugIndicationDTO>(value.ToString());
                _service.Delete(indication);
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