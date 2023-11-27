using Media.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Application.Dto
{
    public class ProductViewDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; }

    }
}
