using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<int>
    {
        public string Body { get; set; }
        public int PostId { get; set; }
    }

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public CreateCommentCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            Comment comment = new Comment
            {
               Body = request.Body,
               PostId = request.PostId,
               CreatedBy = _currentUserService.UserId
            };

            _context.Comments.Add(comment);

            await _context.SaveChangesAsync(cancellationToken);

            return comment.Id;
        }
    }

}
