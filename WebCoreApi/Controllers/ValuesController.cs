using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Web.Service.Domain;

namespace WebCoreApi.Controllers
{
    [Authorize(Roles = "2,1")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        Teacher teacher;
        public ValuesController(Teacher teacher)
        {
            this.teacher = teacher;
        }


        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            await teacher.list("");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
