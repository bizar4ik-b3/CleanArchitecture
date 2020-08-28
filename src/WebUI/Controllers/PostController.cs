using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Post.Commands.CreatePost;
using CleanArchitecture.Application.Post.Commands.DeletePost;
using CleanArchitecture.Application.Post.Commands.UpdatePost;
using CleanArchitecture.Application.Post.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers
{
   // [Authorize]
    public class PostsController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreatePostCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdatePostCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeletePostCommand { Id = id });

            return NoContent();
        }

        [HttpGet]
        public async Task<IEnumerable<CleanArchitecture.Domain.Entities.Post>> Get()
        {
            return await Mediator.Send(new GetPosts());
        }
    }
}
