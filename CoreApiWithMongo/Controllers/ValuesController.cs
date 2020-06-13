﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiWithMongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoreApiWithMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IDIDemo _dIDemo;
        public ValuesController(IDIDemo dIDemo )
        {
            _dIDemo = dIDemo;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
           // throw new Exception("THIS IS TEST");

            // DIDemo dIDemo = new DIDemo();           
            return new string[] { _dIDemo.Id.ToString(), _dIDemo.Name, "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
