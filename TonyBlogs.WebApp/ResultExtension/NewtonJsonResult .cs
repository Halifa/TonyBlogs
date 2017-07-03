using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Converters;
using System.Text;

namespace TonyBlogs.WebApp
{
    public class NewtonJsonResult : JsonResult
    {
        public JsonSerializerSettings JsonSerializerSettings { get; set; }
        public NewtonJsonResult()
            : this(null)
        {
        }
        public NewtonJsonResult(object obj)
            : this(obj, JsonRequestBehavior.DenyGet)
        {
        }

        public NewtonJsonResult(object obj, JsonRequestBehavior reqBehavior)
            : this(obj, reqBehavior, null, null)
        {

        }

        public NewtonJsonResult(object obj, JsonRequestBehavior reqBehavior, Encoding contentEncoding)
            :this(obj, reqBehavior, contentEncoding, null)
        { }

        public NewtonJsonResult(object obj, JsonRequestBehavior reqBehavior, Encoding contentEncoding, JsonSerializerSettings jsonSerializerSettings)
        {
            this.Data = obj;
            this.JsonRequestBehavior = reqBehavior;
            this.ContentEncoding = contentEncoding;
            if (jsonSerializerSettings == null)
            {
                this.JsonSerializerSettings = new JsonSerializerSettings() { DateFormatString = "yyyy-MM-dd HH:mm:ss" };
            }
            else
            {
                this.JsonSerializerSettings = jsonSerializerSettings;
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;
            if (!string.IsNullOrEmpty(this.ContentType))
            {
                response.ContentType = this.ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }
            if (this.Data != null)
            {
                string strJson = JsonConvert.SerializeObject(this.Data, JsonSerializerSettings);
                response.Write(strJson);
                response.End();
            }
        }
    }
}