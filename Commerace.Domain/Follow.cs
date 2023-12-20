using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Domain
{
    public class Follow
    {

        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FollowTo { get; set; }
    }
}
