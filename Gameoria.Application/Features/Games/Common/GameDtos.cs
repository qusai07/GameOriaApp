using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOria.Application.Features.Games.Common
{
    public record GameDto
    {
        public int Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public decimal? DiscountedPrice { get; init; }
        public string Category { get; init; } = string.Empty;
        public string Platform { get; init; } = string.Empty;
        public List<string> Tags { get; init; } = new();
        public GameContentRatingDto ContentRating { get; init; } = new();
        public DateTime ReleaseDate { get; init; }
    }

    public record GameDetailsDto 
    {
        public List<GameCodeDto> AvailableCodes { get; init; } = new();
        public List<GameAccountDto> AvailableAccounts { get; init; } = new();
        public DateTime CreatedAt { get; init; }
        public string CreatedBy { get; init; } = string.Empty;
        public DateTime? LastModifiedAt { get; init; }
        public string? LastModifiedBy { get; init; }
    }

    public record GameCodeDto
    {
        public int Id { get; init; }
        public string Code { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public bool IsUsed { get; init; }
        public string Platform { get; init; } = string.Empty;
        public string Region { get; init; } = string.Empty;
        public DateTime ExpiryDate { get; init; }
    }
    public record GameContentRatingDto
    {
        public string RatingSystem { get; init; } = string.Empty; // مثل ESRB, PEGI
        public string Rating { get; init; } = string.Empty;
        public List<string> ContentDescriptors { get; init; } = new();
    }

    public record GameAccountDto
    {
        public int Id { get; init; }
        public decimal SellingPrice { get; init; }
        public string SellerName { get; init; } = string.Empty;
        public List<string> IncludedGames { get; init; } = new();
        public DateTime CreatedAt { get; init; }
        public bool IsSold { get; init; }
        public string Platform { get; init; } = string.Empty;
        public string Region { get; init; } = string.Empty;
    }
}
