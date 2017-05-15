using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIApplication.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Hello World from .NET Core");
        }

        [HttpPost]
        public ActionResult Post([FromBody]Contact c)
        {
            var r = new SimpleResult{
                Result = $"Hello {c.FirstName} {c.LastName} from .NET Core!"
            };
            return this.Created("", r);
        }
    }
}
