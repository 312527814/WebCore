using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Web.Service.Domain;
using Web.Service.Core;

namespace WebCoreApi.Controllers
{
    //[Authorize(Roles = "2,1")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        IMySession session;
        Teacher teacher;
        public ValuesController(Teacher teacher,IMySession session)
        {
            this.teacher = teacher;
            this.session = session;
        }


        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            //throw new Exception("dada");
            await teacher.list("");

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return session.UserId.ToString();
            //return "value";
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
