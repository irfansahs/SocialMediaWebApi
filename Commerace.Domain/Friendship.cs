using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Domain
{
    public class Friendship
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public Friendship Friend { get; set; }
    }
}
