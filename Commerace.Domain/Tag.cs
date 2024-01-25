using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Domain
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }

}