using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;


namespace xAPIReceiver.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class xAPIController : ControllerBase
    {
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
        
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public HttpResponseMessage Post()
        {
            if (Request.IsHttps)
            {
                return Parsemessage(Request.Body, Request.Headers);
            }
            else
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }

        }

        private HttpResponseMessage Parsemessage(Stream body, IHeaderDictionary headers)
        {
            
            HttpResponseMessage rsp = new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
            StreamReader sr = new StreamReader(body);
            string message = sr.ReadToEnd();
            dynamic msg = JsonConvert.DeserializeObject<dynamic>(message);
            string output = String.Format("Actor: {0}, email: {1}, Verb: {2}, Category: {3}, Timestamp {4}", 
                                  msg.actor.name,msg.actor.mbox,msg.verb.id,msg.context.contextActivities.category[0].id,msg.timestamp);            

            return rsp;
        }
    }
}
