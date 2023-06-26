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
    public class ContraindicationsController : ApiController
    {
        private ContraIndicationService _service;
        private HttpResponseMessage _message;

        public ContraindicationsController(ContraIndicationService service)
        {
            _service = service;
        }

        //GET: api/contraindications
        public async Task<HttpResponseMessage> Get()
        {
            JArray array = new JArray();
            JObject result;
            try
            {
                _message = Request.CreateResponse(System.Net.HttpStatusCode.OK);
                var contraIndications = await _service.GetAllAsync();
                if(contraIndications.Count() == 0)
                {
                    _message = Request.CreateResponse(System.Net.HttpStatusCode.NoContent);
                    string json = @"{
                                        Count: 0,
                                        ContraIndications: []
                                    }";
                    result = JObject.Parse(json);
                }
                else
                {
                    result = new JObject();
                    foreach(var contraIndication in contraIndications)
                    {
                        array.Add(contraIndication);
                    }
                    result["Count"] = array.Count;
                    result["ContraIndications"] = array;
                }
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch(Exception e)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
            }
            return _message;
        }

        // GET: api/contraindications/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            JObject result;
            try
            {
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Found);
                var contraIndication = _service.Get(id);
                if(contraIndication == null)
                {
                    _message.StatusCode = System.Net.HttpStatusCode.NotFound;
                    string json = @"{
                                        ContraIndication: null
                                    }";
                    result = JObject.Parse(json);
                }
                else
                {
                    result = JObject.FromObject(contraIndication);
                }
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch(Exception e)
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
                var contraIndication = JsonConvert.DeserializeObject<DrugContraIndicationDTO>(value.ToString());
                contraIndication = _service.Add(contraIndication);
                result = JObject.FromObject(contraIndication);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Created);
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
                DrugContraIndicationDTO contraIndication = JsonConvert.DeserializeObject<DrugContraIndicationDTO>(value.ToString());
                _service.Update(contraIndication);
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
                DrugContraIndicationDTO contraindication = JsonConvert.DeserializeObject<DrugContraIndicationDTO>(value.ToString());
                _service.Delete(contraindication);
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