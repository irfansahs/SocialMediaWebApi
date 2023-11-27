using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Domain
{
    public class Tag
    {
        public int Id { get; set; }
        public Post Post { get; set; }
    }


}
