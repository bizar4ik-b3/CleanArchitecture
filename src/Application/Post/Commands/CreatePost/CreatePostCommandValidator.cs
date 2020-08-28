using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Post.Commands.CreatePost
{
    public class CreatPostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatPostCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(v => v.Body)
                .MaximumLength(500)
                .NotEmpty();
        }
    }
}
