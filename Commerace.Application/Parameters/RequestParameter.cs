using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Application.Parameters
{
    public class RequestParameter
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; } = 0;
        public int PageCount { get; set; }  

    }
}
