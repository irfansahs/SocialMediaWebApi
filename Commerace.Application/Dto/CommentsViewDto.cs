using Media.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Application.Dto
{
    public class CommentsViewDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public int PostId { get; set; }
        public string? UserName { get; set; }
        public string? ProfileImage { get; set; }

    }
}
