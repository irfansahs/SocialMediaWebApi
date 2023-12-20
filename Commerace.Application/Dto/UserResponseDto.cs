using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Dto
{
    public class UserResponseDto
    {

        public string UserName { get; set; }
        public string ProfileImage { get; set; }
        public string UserColor { get; set; }
        public bool IsFollow { get; set; }
        public int FollowCount { get; set; }
        public int FollowersCount { get; set; }

    }
}
