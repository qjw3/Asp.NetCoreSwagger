using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class SwaggerClass
    {
        public static Dictionary<string, OpenApiInfo> Get()
        {
            var dic = new Dictionary<string, OpenApiInfo>()
            {
                {"ClassLibrary1",new OpenApiInfo(){Title="就是ClassLibrary1",Version="V1" }},
                {"WebApplication1",new OpenApiInfo(){Title="就是WebApplication1",Version="V1" }}
            };

            return dic;
        }
    }
}
