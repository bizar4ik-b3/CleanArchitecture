using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Post.Commands.DeletePost
{

    public class DeletePostCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTodoItemCommandHandler : IRequestHandler<DeletePostCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public DeleteTodoItemCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
    }

        public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.FindAsync(request.Id);

            if (post == null)
            {
                throw new NotFoundException(nameof(Post), request.Id);
            }

            if (post.CreatedBy != _currentUserService.UserId)
            {
                throw new NotFoundException("You can't delete this post!");
            }

            if(post.Comments != null)
            {
                post.Comments.ToList().ForEach(x => _context.Comments.Remove(x));
            }
            await _context.SaveChangesAsync(cancellationToken);

            _context.Posts.Remove(post);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
