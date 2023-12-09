using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Domain
{
    public class Like
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public int PostId { get; set; }
    }

}
