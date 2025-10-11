using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOria.Shared.ViewModels.Organizer
{
    public class CompleteStoreProfileVM
    {
        [Required(ErrorMessage = "Owner First Name is required")]
        [StringLength(20 ,ErrorMessage = "Invalid Owner First Name format")]
        [Display(Name = "Owner First Name")]
        public string OwnerFirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Owner Last Name is required")]
        [StringLength(20,ErrorMessage = "Invalid Owner Last Name format")]
        [Display(Name = "Owner Last Name")]
        public string OwnerLastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Store Name is required")]
        [StringLength(100, ErrorMessage = "Store Name cannot exceed 100 characters")]
        [Display(Name = "Store Name")]
        public string StoreName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Identity Number is required")]
        [StringLength(10, ErrorMessage = "Identity Number cannot exceed 10 characters")]
        [Display(Name = "Identity Number")]
        public string IdentityNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Owner email is required")]
        [StringLength(30, ErrorMessage = "OwnerEmail cannot exceed 30 characters")]
        [Display(Name = "Owner Email")]
        public string OwnerEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Owner Phone is required")]
        [StringLength(30, ErrorMessage = "Owner phone cannot exceed 30 characters")]
        [Display(Name = "Owner Phone")]
        public string OwnerPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "OwnerDateOfBirth is required")]
        [StringLength(10, ErrorMessage = "Birthday cannot exceed 10 characters")]
        [Display(Name = "Owner Birthday")]
        public DateTime OwnerDateOfBirth { get; set; } = DateTime.MinValue;

        [Required(ErrorMessage = "Logo Url is not required")]
        [StringLength(50, ErrorMessage = "Logo Url cannot exceed 50 characters")]
        [Display(Name = "Logo Url")]
        public string LogoUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cover Image Url is not required")]
        [StringLength(10, ErrorMessage = "Cover Image Url cannot exceed 10 characters")]
        [Display(Name = "Cover Image Url")]
        public string CoverImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "Title is not required")]
        [StringLength(30, ErrorMessage = "Title cannot exceed 20 characters")]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty; 
        
        [Required(ErrorMessage = "Title Description is not required")]
        [StringLength(200, ErrorMessage = "Title Description cannot exceed 200 characters")]
        [Display(Name = "Title Description")]
        public string TitleDescription { get; set; } = string.Empty; 
        
        [Required(ErrorMessage = "Shortcut Website is not required")]
        [StringLength(50, ErrorMessage = "Shortcut Website cannot exceed 50 characters")]
        [Display(Name = "Shortcut Website")]
        public string ShortcutWebsite { get; set; } = string.Empty;


    }
}
