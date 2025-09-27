using FluentValidation;
using GameOria.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace GameOria.Application.Features.Games.Commands.CreateGame;

public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateGameCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters");

        RuleFor(v => v.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(4000).WithMessage("Description must not exceed 4000 characters");

        RuleFor(v => v.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(v => v.CoverImageUrl)
            .NotEmpty().WithMessage("Cover image URL is required")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Cover image URL must be a valid URL");

        RuleFor(v => v.TrailerUrl)
            .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("Trailer URL must be a valid URL");

        RuleFor(v => v.ReleaseDate)
            .NotEmpty().WithMessage("Release date is required");

        //RuleFor(v => v.StoreId)
        //    .MustAsync(async (id, cancellation) => await context.Stores.AnyAsync(s => s.Id == id))
        //    .WithMessage("Store does not exist");

        //RuleFor(v => v.CategoryIds)
        //    .NotEmpty().WithMessage("At least one category must be selected")
        //    .MustAsync(async (ids, cancellation) =>
        //        await context.Category.CountAsync(c => ids.Contains(c.Id)) == ids.Count)
        //    .WithMessage("One or more selected categories do not exist");

        RuleForEach(v => v.Images)
            .ChildRules(image =>
            {
                image.RuleFor(x => x.Url)
                    .NotEmpty().WithMessage("Image URL is required")
                    .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                    .WithMessage("Image URL must be a valid URL");

                image.RuleFor(x => x.AltText)
                    .NotEmpty().WithMessage("Image alt text is required")
                    .MaximumLength(200).WithMessage("Alt text must not exceed 200 characters");

                image.RuleFor(x => x.DisplayOrder)
                    .GreaterThanOrEqualTo(0).WithMessage("Display order must be greater than or equal to 0");
            });
    }
}
