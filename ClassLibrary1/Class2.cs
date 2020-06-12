using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class Class2Controller : ControllerBase
    {
        /// <summary>
        /// aaa
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public ValueRes2 Get(ValueReq2 req)
        {
            return new ValueRes2();
        }
    }
}
