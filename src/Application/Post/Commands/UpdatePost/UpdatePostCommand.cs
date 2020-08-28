using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Post.Commands.UpdatePost
{
    public class UpdatePostCommand : IRequest
    {
        public int Id { get; set; }
        public string  Body { get; set; }
        public string Title { get; set; }
    }

    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UpdatePostCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.FindAsync(request.Id);

            if (post == null)
            {
                throw new NotFoundException(nameof(post), request.Id);
            }

            if(post.CreatedBy != _currentUserService.UserId)
            {
                throw new NotFoundException("You are not a master of this post!");
            }

            post.Title = request.Title;
            post.Body = request.Body;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
