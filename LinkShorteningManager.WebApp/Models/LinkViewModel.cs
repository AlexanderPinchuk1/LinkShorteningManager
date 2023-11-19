using LinkShorteningManager.Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace LinkShorteningManager.WebApp.Models
{
    public class LinkViewModel
    {
        [NotEmpty(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Url is required.")]
        [StringLength(1000, ErrorMessage = "Url length must be between {2} and {1}.", MinimumLength = 1)]
        [RegularExpression(@"https?:\/\/[a-zA-Z0-9.\/]+.[a-z]*[a-zA-Z0-9.\/_?=%\-\&]*", ErrorMessage = "Url is invalid.")]
        public string? Url { get; set; }

        [Required(ErrorMessage = "Key is required.")]
        [StringLength(6, ErrorMessage = "Key length must be {1}.", MinimumLength = 6)]
        public string? ShortKey { get; set; }

        [Required(ErrorMessage = "Creation date is required.")]
        [NotInFuture(ErrorMessage = "Creation date is invalid.")]
        public DateTime? CreationDate { get; set; }

        [Required(ErrorMessage = "Click count is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Click count must be {1} or greater.")]
        public int? ClickCount { get; set; }
    }
}
