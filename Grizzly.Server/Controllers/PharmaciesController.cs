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
using System.Net.Http;
using System.Text;

namespace Grizzly.Server.Controllers
{
    public class PharmaciesController : ApiController
    {
        private FarmacyOfficeService _service;
        private HttpResponseMessage _message;

        public PharmaciesController(FarmacyOfficeService service)
        {
            _service = service;
        }

        //GET: api/pharmacies
        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                JObject result;
                JArray pharmacies = new JArray();
                var offices = await _service.GetAllAsync();

                if(offices.Count() == 0)
                {
                    _message = Request.CreateResponse(System.Net.HttpStatusCode.NoContent);
                    string json = @"{
                                        Count: 0,
                                        Offices: []
                                    }";
                    result = JObject.Parse(json);
                    _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
                }
            }
            catch(Exception e)
            {
                _message = Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, e);
            }
            return _message;
        }

        // GET: api/pharmacies/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            JObject result;
            try
            {
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Found);
                var pharmacy = _service.Get(id);
                if(pharmacy == null)
                {
                    _message.StatusCode = System.Net.HttpStatusCode.NotFound;
                    string json = @"{
                                        Pharmacy: null
                                    }";
                    result = JObject.Parse(json);
                }
                else
                {
                    result = JObject.FromObject(pharmacy);
                }
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch (Exception ex)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
            }
            return _message;
        }

        public async Task<HttpResponseMessage> Post([FromBody] JObject value)
        {
            try
            {
                PharmacyOfficeDTO pharmacy = JsonConvert.DeserializeObject<PharmacyOfficeDTO>(value.ToString());
                pharmacy = _service.Add(pharmacy);
                JObject result = JObject.FromObject(pharmacy);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Created);
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch (Exception e)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
            }
            return _message;
        }

        public async Task<HttpResponseMessage> Put([FromBody] JObject value)
        {
            try
            {
                JObject result;
                PharmacyOfficeDTO pharmacy = JsonConvert.DeserializeObject<PharmacyOfficeDTO>(value.ToString());
                _service.Update(pharmacy);
                result = JObject.FromObject(pharmacy);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
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
                PharmacyOfficeDTO pharmacy = JsonConvert.DeserializeObject<PharmacyOfficeDTO>(value.ToString());
                _service.Delete(pharmacy);
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