// CreateGameCommandHandler.cs
using Gameoria.Application.Common.Interfaces;
using Gameoria.Application.Common.Models;
using Gameoria.Domains.Entities.Games;
using Gameoria.Domains.Events.Games;
using Gameoria.Domains.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gameoria.Application.Features.Games.Commands.CreateGame;

public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateGameCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result<Guid>> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Verify store ownership
            var store = await _context.Stores
                .FirstOrDefaultAsync(s => s.Id == request.StoreId && s.OwnerId == _currentUserService.UserId, cancellationToken);

            if (store == null)
                return Result<Guid>.Failure(new[] { "You don't have permission to add games to this store" });

            var game = new Game(
                request.Title,
                request.Description,
                new Money(request.Price),
                request.CoverImageUrl,
                request.ReleaseDate,
                request.StoreId
            );

            // Add categories
            foreach (var categoryId in request.CategoryIds)
            {
                game.Categories.Add(new GameCategory(game.Id, categoryId));
            }

            // Add images
            foreach (var imageDto in request.Images)
            {
                game.Images.Add(new GameImage(
                    imageDto.Url,
                    imageDto.AltText,
                    imageDto.DisplayOrder,
                    game.Id
                ));
            }

            if (!string.IsNullOrEmpty(request.TrailerUrl))
            {
                game.TrailerUrl = request.TrailerUrl;
            }

            _context.Games.Add(game);

            await _context.SaveChangesAsync(cancellationToken);

            // Add domain event
            game.AddDomainEvent(new GameCreatedEvent(game));

            return Result<Guid>.Success(game.Id);
        }
        catch (Exception ex)
        {
            return Result<Guid>.Failure(new[] { $"Error creating game: {ex.Message}" });
        }
    }
}