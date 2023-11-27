using Bogus;
using Media.Domain;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.DataFaker
{
    public class DataFaker
    {

        public string function()
        {

            Post userfaker = new Faker<Post>().RuleFor(i => i.Content, i => i.Commerce.ProductName());


            return null;
        }

    }
}
