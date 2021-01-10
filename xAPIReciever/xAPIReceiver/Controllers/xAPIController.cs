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
using Microsoft.Extensions.Logging;
using xAPIReceiver.Models;



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
               return Parsemessage(Request.Body, Request.Headers);
        }

        private HttpResponseMessage Parsemessage(Stream body, IHeaderDictionary headers)
        {
            HttpResponseMessage rsp = new HttpResponseMessage(System.Net.HttpStatusCode.Created);
            string message = getstring(body).Result;
            dynamic msg = JsonConvert.DeserializeObject<dynamic>(message);
            string soutput = String.Format("Actor: {0}, email: {1}, Verb: {2}, Category: {3}, Timestamp {4}", 
                                  msg.actor.name,msg.actor.mbox,msg.verb.id,msg.context.contextActivities.category[0].id,msg.timestamp);
            string textstring = soutput;
            textstring += "\r\n";
            Log(textstring);
            return rsp;
        }

        private void Log(string textstring)
        {
            string logfilename = string.Format("C:\\Text\\Log{0}.txt", DateTime.Now.Date.ToString("MM-dd-yyyy"));
            if (!System.IO.Directory.Exists("C:\\Text\\")) 
            { 
                System.IO.Directory.CreateDirectory("C:\\Text"); 
            }
            if (!System.IO.File.Exists(logfilename))
            {
                System.IO.File.CreateText(logfilename);
            }
            if(new System.IO.FileInfo(logfilename).Length > 200000)
            {
                string oldLogFileName = string.Format("{0}-{1}.txt", logfilename.Replace(".txt", ""), DateTime.Now.ToString("HH-mm-ss"));
                System.IO.File.Move(logfilename, oldLogFileName);
            }
            StreamWriter sw = System.IO.File.AppendText(logfilename);
            sw.Write(textstring);
            sw.Flush();
            sw.Close();

        }

        static async Task<string> getstring(Stream body) 
        {
            string retstring = "";
            StreamReader sr = new StreamReader(body);
            retstring = await sr.ReadToEndAsync();
            return retstring;
        }
    }
}
