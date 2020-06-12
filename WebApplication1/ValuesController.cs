using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public ValueRes Test(ValueReq req)
        {
            var res = new ValueRes();


            return res;
        }
    }
}
