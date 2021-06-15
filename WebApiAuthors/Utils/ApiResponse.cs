using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApiAuthors.Utils
{
    public class ApiResponse
    {
        public int Code { get; set; }
        public string Status { get; set; } = "invalid";
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Body { get; set; }

        public ApiResponse(int code = 200)
        {
            Code = code;
            if (!Enum.IsDefined(typeof(HttpStatusCode), code))
                throw new Exception($"Invalid {code} status code.");
            Status = GetStatusCodeName(code);
            if (Status == "error")
                Message = GetMessage(code);
        }


        private static string GetStatusCodeName(int code)
        {
            if (code >= 100 && code <= 299)
                return "success";
            if (code >= 300 && code <= 499)
                return "fail";
            if (code >= 500)
                return "error";

            return "invalid";
        }

        private string GetMessage(int code)
        {
            string statusCode = Enum.GetName(typeof(HttpStatusCode), code);
            var statusCodevalues = Regex.Split(statusCode, @"(?<!^)(?=[A-Z])");
            string message = string.Join(" ", statusCodevalues);
            return message;
        }

        private static void SetApiResponseData(int code, object body, string message, out ApiResponse response)
        {
            response = new ApiResponse(code);
            response.Body = body;
            response.Message = message;
        }

        private static ActionResult HttpResult(int code, object body=null, string message = null)
        {
            SetApiResponseData(code, body, message, out ApiResponse response);
            return response;

        }

        public static ActionResult Ok(object data=null, string message=null)
        {
            return HttpResult((int)HttpStatusCode.OK, data, message);
        }

        public static ActionResult Created(object data = null, string message = null)
        {
            return HttpResult((int)HttpStatusCode.Created, data, message);
        }

        public static ActionResult BadRequest(string message = null)
        {
            return HttpResult((int)HttpStatusCode.BadRequest, null, message);
        }

        public static ActionResult NotFound(string message = null)
        {
            return HttpResult((int)HttpStatusCode.NotFound, null, message);
        }

        public static ActionResult ServerError(string message = null)
        {
            return HttpResult((int)HttpStatusCode.InternalServerError, null, message);
        }

        public static implicit operator ActionResult(ApiResponse response)
        {
            return new ContentResult
            {
                Content = response.ToString(),
                StatusCode = response.Code,
                ContentType = "application/json"
            };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}
