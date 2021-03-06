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
            string verbdata = "";

            if (msg.verb.id.ToString().Contains("initialized"))
            {
                verbdata = string.Format("{0} initiated course \"{1}\"", msg.actor.name, msg.@object.definition.name.en);
            }
            else if (msg.verb.id.ToString().Contains("launched"))
            {
                verbdata = string.Format("{0} launched course \"{1}\"", msg.actor.name, msg.@object.definition.name.en);
            }
            else if (msg.verb.id.ToString().Contains("passed"))
            {
                verbdata = string.Format("{0} completed and passed course \"{1}\"", msg.actor.name, msg.@object.definition.name.en);
            }
            else if (msg.verb.id.ToString().Contains("failed"))
            {
                verbdata = string.Format("{0} completed and failed to pass course \"{1}\"", msg.actor.name, msg.@object.definition.name.en);
            }
            else if (msg.verb.id.ToString().Contains("validated"))
            {
                verbdata = string.Format("{0} was vaidated for Competency \"{1}\"", msg.actor.name, msg.@object.definition.name.en);
            }
            else if (msg.verb.id.ToString().Contains("conferred"))
            {
                if (msg.@object.definition.type.ToString().Contains("credential"))
                {
                    verbdata = string.Format("{0} was confereed Credential \"{1}\"", msg.actor.name, msg.@object.definition.name.en);
                }
                else if (msg.@object.definition.type.ToString().Contains("badge"))
                {
                    verbdata = string.Format("{0} was conferred a badge for \"{1}\"", msg.actor.name, msg.@object.definition.name.en);
                }
                else
                {
                    verbdata = string.Format("{0} was conferred an unknosn conferral", msg.actor.name);
                }
            }
            else if (msg.verb.id.ToString().Contains("abandoned"))
            {
                verbdata = string.Format("Course Abandoned after {0}", GetDuration(msg.result.duration));
            }
            else if (msg.verb.id.ToString().Contains("satisfied"))
            {
                verbdata = string.Format("Requirements Satisfied");
            }
            else if (msg.verb.id.ToString().Contains("completed"))
            {
                verbdata = string.Format("{0} completed course \"{1}\" after {2}", msg.actor.name, msg.@object.definition.name.en, GetDuration(msg.result.duration));
            }
            else
            {
                verbdata = string.Format("xAPI verb {0} unknown", msg.verb.id.ToString());
            }

            string soutput = String.Format("Actor: {0}, email: {1}, Action: {2}, Registration {4}, Timestamp {3}",
                                  msg.actor.name, msg.actor.mbox, verbdata,
                                  msg.timestamp, msg.context.registration.ToString());
            string textstring = soutput;
            textstring += "\r\n";
            Log(textstring);
            return rsp;
        }

        private string GetDuration(dynamic duration)
        {
            string Duration = duration.ToString().Replace("PT", "").Replace("S", "");
            int iH = Duration.IndexOf("H");
            int iM = Duration.IndexOf("M");
            int iS = Duration.Length;
            string Hour = Duration.Substring(0, iH);
            string Min = Duration.Substring(iH + 1, iM - (iH + 1));
            string Sec = Duration.Substring(iM + 1);

            return string.Format("{0} hours, {1} minutes {2} seconds", Hour, Min, Sec);
        }

        private void Log(string textstring)
        {
            StreamWriter sw = null;
            string logfilename = string.Format("C:\\Text\\Log{0}.txt", DateTime.Now.Date.ToString("MM-dd-yyyy"));
            if (!System.IO.Directory.Exists("C:\\Text\\"))
            {
                System.IO.Directory.CreateDirectory("C:\\Text");
            }
            if ((System.IO.File.Exists(logfilename)) && (new System.IO.FileInfo(logfilename).Length > 200000))
            {
                string oldLogFileName = string.Format("{0}-{1}.txt", logfilename.Replace(".txt", ""), DateTime.Now.ToString("HH-mm-ss"));
                System.IO.File.Move(logfilename, oldLogFileName);
            }
            if (!System.IO.File.Exists(logfilename))
            {
                sw = System.IO.File.CreateText(logfilename);
            }
            else
            {
                sw = System.IO.File.AppendText(logfilename);
            }
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

