using Media.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Domain
{
    public class Follow : BaseEntity
    {
        public string FollowerId { get; set; }
        public virtual AppUser Follower { get; set; }

        public string FollowingId { get; set; }
        public virtual AppUser Following { get; set; }

    }
}
