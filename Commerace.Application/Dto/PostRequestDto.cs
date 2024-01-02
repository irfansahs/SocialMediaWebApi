using Media.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Dto
{
    public class PostRequestDto
    {
        public string UserName { get; set; }
        public string Content { get; set; }
        public List<Tag>? TagNames { get; set; }
    }
}
