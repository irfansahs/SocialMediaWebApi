using Media.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Domain
{
    public class Post : BaseEntity
    {
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }
        public string Content { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
