using Commerace.Application.Dto;
using Media.Domain;
using Media.Persistence.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Dto
{
    public class PostListModel : BasePageableModel
    {
        public IList<PostViewDto> items { get; set; }

    }
}
