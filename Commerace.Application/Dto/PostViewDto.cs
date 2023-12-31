﻿using Media.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Commerace.Application.Dto
{
    public class PostViewDto
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UserName { get; set; }
        public string? ProfileImage { get; set; }
        public string? UserColor { get; set; }
        public int? LikeCount { get; set; }
        public bool? IsLiked { get; set; }
        public int? CommentsCount { get; set;}

    }
}