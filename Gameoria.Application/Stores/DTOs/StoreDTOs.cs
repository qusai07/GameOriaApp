using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOria.Application.Stores.DTOs
{
    public class BaseStore
    { 
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public bool IsVerified { get; set; }
        public string StoreName { get; set; } 
    }
    public class CompleteStore : BaseStore
    {
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; } 
        public string OwnerEmail { get; set; }
        public string OwnerPhone { get; set; }
        public DateTime OwnerDateOfBirth { get; set; }
        public string? LogoUrl { get; set; }
        public string? CoverImageUrl { get; set; }
        public string Title { get; set; } 
        public string TitleDescription { get; set; }
        public string ShortcutWebsite { get; set; } 

    }
    //public class StoreReviewDto : Base
    //{
    //    public string UserId { get; set; } = string.Empty;
    //    public int Rating { get; set; }
    //    public string Title { get; set; } = string.Empty;
    //    public string Content { get; set; } = string.Empty;

    //    public bool IsApproved { get; set; }
    //    public DateTime? ApprovedAt { get; set; }
    //    public string? ApprovedBy { get; set; }
    //}
}
