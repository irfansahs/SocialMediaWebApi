using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Domain.Identity
{
    public class AppUser : IdentityUser
    {
        public int? ConfirmCode { get; set; }
        public string? ProfileImage { get; set; }
        public string? UserColor { get; set; }
        public List<Post> Posts { get; set; }
    }
}
