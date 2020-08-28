using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Post.Queries
{

    public class GetPosts: IRequest<IEnumerable<CleanArchitecture.Domain.Entities.Post>>
    {
        public string UserId { get; set; }
    }


    public class GetPostsHandler : IRequestHandler<GetPosts, IEnumerable<CleanArchitecture.Domain.Entities.Post>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetPostsHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<CleanArchitecture.Domain.Entities.Post>> Handle(GetPosts request, CancellationToken cancellationToken)
        {
            return await _context.Posts.Where(x => x.UserId == _currentUserService.UserId).Include(x => x.Comments).ToListAsync(cancellationToken);
        }
    }

}
