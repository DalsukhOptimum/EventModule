using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace EventModule.Filters
{
    public class MyAttribute : ActionFilterAttribute
    {

        public MyAttribute()
        {
            //Trace.WriteLine("i am in MyAttribute Constructer");

        }


        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                
                var Erros = new List<string>();
                // var error = ModelState.Where(e => e.Value.Errors.Count > 0).Select(e =>  e.Value.Errors.First().ErrorMessage).ToList();
                foreach (var values in actionContext.ModelState.Values)
                {
                    foreach (var error in values.Errors)
                    {
                        Erros.Add(error.Exception.Message.ToString().Split('.')[0]);
                    }
                }

                actionContext.Response = new System.Net.Http.HttpResponseMessage()
                {
                    Content = new StringContent(JsonConvert.SerializeObject(string.Join(",", Erros))),
                    ReasonPhrase = "Validation error",
                    StatusCode = (System.Net.HttpStatusCode)422
                };

            }
            


        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            
        }

    }
}