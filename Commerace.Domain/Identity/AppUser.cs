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
        public List<Post> Posts { get; set; }
    }
}
