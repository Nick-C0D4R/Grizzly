using BLL.DTO;
using BLL.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
namespace Grizzly.Server.Controllers
{
    public class ProducersController : ApiController
    {
        private ProducerService _service;
        private HttpResponseMessage _message;
        public ProducersController(ProducerService service)
        {
            _service = service;
        }

        //GET: api/producers
        public async Task<HttpResponseMessage> Get()
        {
            var producers = await _service.GetAllAsync();
            JArray array = new JArray();
            JObject result;

            if (producers.Count() == 0)
            {
                _message = Request.CreateResponse(System.Net.HttpStatusCode.NoContent);
                string json = @"{
                                    Count: 0,
                                    Producers: []
                                }";
                result = JObject.Parse(json);
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
                return _message;
            }
            try
            {
                foreach(var producer in producers)
                {
                    array.Add(producer);
                }
                result = new JObject();
                result["Count"] = array.Count;
                result["Producers"] = array;
                _message = Request.CreateResponse(System.Net.HttpStatusCode.OK);
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch(Exception ex)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex);
            }
            return _message;
        }

        // GET: api/producers/5
        public async Task<HttpResponseMessage> Get(int id)
        {
            JObject result;
            try
            {
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Found);
                var producer = _service.Get(id);
                if(producer == null)
                {
                    _message.StatusCode = System.Net.HttpStatusCode.NotFound;
                    string json = @"{
                                        Producer: null
                                    }";
                    result = JObject.Parse(json);
                }
                else
                {
                    result = JObject.FromObject(producer);
                }
                _message.Content = new StringContent(result.ToString(), Encoding.UTF8, "application/json");
            }
            catch(Exception e)
            {
                _message = Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, e);
            }
            return _message;
        }
        //POST api/prudcers
        public async Task<HttpResponseMessage> Post([FromBody] JObject value)
        {
            try
            {
                ProducerDTO producer = JsonConvert.DeserializeObject<ProducerDTO>(value.ToString());
                producer = _service.Add(producer);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Created);
                JObject result = JObject.FromObject(producer);
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
                ProducerDTO producer = JsonConvert.DeserializeObject<ProducerDTO>(value.ToString());
                _service.Update(producer);
                _message = Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
                _message.Content = new StringContent(JObject.FromObject(producer).ToString(), Encoding.UTF8, "application/json");
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
                ProducerDTO producer = JsonConvert.DeserializeObject<ProducerDTO>(value.ToString());
                _service.Delete(producer);
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