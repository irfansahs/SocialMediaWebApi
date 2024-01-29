using Bogus;
using Commerace.Infrastructure;
using Media.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Commerace.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakeDataController : ControllerBase
    {

        private readonly IMediator mediator;
        private readonly UserDbContext userContext;
        public FakeDataController(IMediator mediator, UserDbContext userDbContext)
        {
            this.mediator = mediator;
            this.userContext = userDbContext;
        }

        [HttpGet("GetFakePostData")]
        public async Task<IActionResult> GetFakeData()
        {
            // for döngüsü ile 20 kayıt ver tabanına eklendi
            for (int a = 0; a < 20; a++)
            {
                Post userfaker = new Faker<Post>()
                .RuleFor(i => i.Content, i => i.Vehicle.Model())
                .RuleFor(i => i.UserId, "b7efb971-1da1-464e-931a-1b1a309edfa5")
                .RuleFor(i => i.CreatedOn, i => i.Date.Past());

            userContext.Posts.Add(userfaker);
            }
            
            await userContext.SaveChangesAsync();

            return Ok(null);

        }
        [HttpGet("GetFakeCommentData")]
        public async Task<IActionResult> GetFakeCommentData()
        {
            Comment userfaker = new Faker<Comment>()
                .RuleFor(i => i.Content, i => i.Commerce.ProductName())
                .RuleFor(i => i.CreatedOn, i => i.Date.Past());




            userContext.Comments.Add(userfaker);
            await userContext.SaveChangesAsync();

            return Ok(userfaker);

        }

    }
}
