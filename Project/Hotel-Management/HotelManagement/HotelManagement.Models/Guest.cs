using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class Guest
    {
        [Key]
        public int GuestId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = "";

        [StringLength(200)]
        public string? Address { get; set; }
    }
}