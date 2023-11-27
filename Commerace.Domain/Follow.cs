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
        public int FollowerId { get; set; }
        public int FollowingId { get; set; }

    }
}
