using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Comments.Commands.CreateComment;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers
{
    // [Authorize]
    public class CommentController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCommentCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
