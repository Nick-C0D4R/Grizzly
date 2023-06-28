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
    public class ApplicationsController : ApiController
    {
        private ApplicationTypeService _service;
        private HttpResponseMessage _message;

        public ApplicationsController(ApplicationTypeService service)
        {
            _service = service;
        }

        //GET: api/applications
        public async Task<HttpResponseMessage> Get()
        {
            JArray array = new JArray();
            JObject result;
            try
            {
                _message = Request.CreateResponse(System.Net.HttpStatusCode.OK);
                var applications = await _service.GetAllAsync();
                if (applications.Count() == 0)
                {
                    _message = Request.CreateResponse(System.Net.HttpStatusCode.NoContent);
                    string json = @"{
                                        Count: 0,
                                        Applications: []
                                    }";
                    result = JObject.Parse(json);
                }
                else
                {
                    result = new JObject();
                    foreach (var application in applications)
                    {
                        array.Add(application);
                    }
                    result["Count"] = array.Count;
                    result["Applications"] = array;
                }
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch (Exception e)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
            }
            return _message;
        }

        // GET: api/applications/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            JObject result;
            try
            {
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Found);
                var application = _service.Get(id);
                if (application == null)
                {
                    _message.StatusCode = System.Net.HttpStatusCode.NotFound;
                    string json = @"{
                                        Application: null
                                    }";
                    result = JObject.Parse(json);
                }
                else
                {
                    result = JObject.FromObject(application);
                }
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch (Exception e)
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
                var application = JsonConvert.DeserializeObject<DrugApplicationTypeDTO>(value.ToString());
                application = _service.Add(application);
                result = JObject.FromObject(application);
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
                DrugApplicationTypeDTO application = JsonConvert.DeserializeObject<DrugApplicationTypeDTO>(value.ToString());
                _service.Update(application);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
                _message.Content = new StringContent(value.ToString(), Encoding.UTF8, "application/json");
            }
            catch (Exception e)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
            }
            return _message;
        }

        public async Task<HttpResponseMessage> Delete([FromBody] JObject value)
        {
            try
            {
                DrugApplicationTypeDTO application = JsonConvert.DeserializeObject<DrugApplicationTypeDTO>(value.ToString());
                _service.Delete(application);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
            }
            return _message;
        }
    }
}