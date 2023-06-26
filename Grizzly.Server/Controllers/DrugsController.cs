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
    public class DrugsController : ApiController
    {
        private DrugService _service;
        private HttpResponseMessage _message;

        public DrugsController(DrugService service)
        {
            _service = service;
        }

        //GET: api/drugs
        public async Task<HttpResponseMessage> Get()
        {
            JArray array = new JArray();
            JObject result;
            try
            {
                var drugs = await _service.GetAllAsync();
                if(drugs.Count() == 0)
                {
                    _message = Request.CreateResponse(System.Net.HttpStatusCode.NoContent);
                    string json = @"{
                                        Count: 0,
                                        Drugs: []
                                    }";
                    result = JObject.Parse(json);
                }
                else
                {
                    _message = Request.CreateResponse(System.Net.HttpStatusCode.OK);
                    foreach (var drug in drugs)
                    {
                        array.Add(drug);
                    }
                    result = new JObject();
                    result["Count"] = array.Count;
                    result["Drugs"] = array;
                }
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch(Exception ex)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
            }
            return _message;
        }

        // GET: api/drugs/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            JObject result;
            try
            {
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Found);
                DrugDTO drug = _service.Get(id);
                if(drug == null)
                {
                    _message.StatusCode = System.Net.HttpStatusCode.NotFound;
                    string json = @"{
                                        Drug: null
                                    }";
                    result = JObject.Parse(json);
                }
                else
                {
                    result = JObject.FromObject(drug);
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
                DrugDTO drug = JsonConvert.DeserializeObject<DrugDTO>(value.ToString());
                drug = _service.Add(drug);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Created);
                result = JObject.FromObject(drug);
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
                DrugDTO drug = JsonConvert.DeserializeObject<DrugDTO>(value.ToString());
                _service.Update(drug);
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
                DrugDTO drug = JsonConvert.DeserializeObject<DrugDTO>(value.ToString());
                _service.Delete(drug);
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