using Gameoria.Application.Common.Models;
using Gameoria.Domains.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Gameoria.Application.Features.Games.Commands.CreateGame;

public record CreateGameCommand : IRequest<Result<Guid>>
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public string CoverImageUrl { get; init; } = string.Empty;
    public string? TrailerUrl { get; init; }
    public DateTime ReleaseDate { get; init; }
    public Guid StoreId { get; init; }
    public List<Guid> CategoryIds { get; init; } = new();
    public List<GameImageDto> Images { get; init; } = new();
}

public record GameImageDto
{
    public string Url { get; init; } = string.Empty;
    public string AltText { get; init; } = string.Empty;
    public int DisplayOrder { get; init; }
}