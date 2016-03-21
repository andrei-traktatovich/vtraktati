using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VTraktate.Controllers
{
    public class MyFilter
    {
        public int officeID { get; set; }
        public int[] titles { get; set; }
        public string[] states { get; set; }
    }
    
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get([FromUri] MyFilter filter)
        {
            return new string[] { "value1", "value2" };
        }
        

        // GET api/values/5
        public string Get(int id)
        {
            return string.Format("value{0}", id);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
