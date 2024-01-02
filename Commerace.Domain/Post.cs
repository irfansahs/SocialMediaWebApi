using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Domain
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }
        public string UserName { get; set; }
        public int TotalLikes => Likes?.Count ?? 0;

    }
}
