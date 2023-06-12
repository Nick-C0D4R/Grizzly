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
    public class TypesController : ApiController
    {
        private DrugTypeService _service;
        private HttpResponseMessage _message;

        public TypesController(DrugTypeService service)
        {
            _service = service;
        }

        //GET: api/types
        public async Task<HttpResponseMessage> Get()
        {
            var types = await _service.GetAllAsync();
            JArray array = new JArray();
            
            if(types.Count() == 0)
            {
                _message = Request.CreateResponse(System.Net.HttpStatusCode.NoContent);
                string json = @"{
                                    Count:0,
                                    Types: []
                                }";
                JObject obj = JObject.Parse(json);
                _message.Content = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");
                return _message;
            }
            try
            {
                foreach(var type in types )
                {
                    array.Add(type);
                }
                JObject result = new JObject();
                result["Count"] = array.Count;
                result["Types"] = array;
                _message = Request.CreateResponse(System.Net.HttpStatusCode.OK);
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch(Exception e)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
            }
            return _message;
        }

        // GET: api/types/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            JObject result;
            try
            {
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Found);
                var type = _service.Get(id);
                if (type == null)
                {
                    _message.StatusCode = System.Net.HttpStatusCode.NotFound;
                    string json = @"{
                                        Type: null
                                    }";
                    result = JObject.Parse(json);
                }
                else
                {
                    result = JObject.FromObject(type);
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
            try
            {
                DrugTypeDTO type = JsonConvert.DeserializeObject<DrugTypeDTO>(value.ToString());
                type = _service.Add(type);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Created);
                JObject result = JObject.FromObject(type);
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
                DrugTypeDTO type = JsonConvert.DeserializeObject<DrugTypeDTO>(value.ToString());
                _service.Update(type);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
                JObject result = JObject.FromObject(type);
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
                DrugTypeDTO type = JsonConvert.DeserializeObject<DrugTypeDTO>(value.ToString());
                _service.Delete(type);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch(Exception e )
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
            }
            return _message;
        }
    }
}