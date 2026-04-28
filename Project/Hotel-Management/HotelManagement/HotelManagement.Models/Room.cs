using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        [Display(Name = "Room Number")]
        public string RoomNumber { get; set; } = "";

        [Required]
        public string Type { get; set; } = ""; 

        [Required]
        [Range(100, 100000)]
        [Display(Name = "Price Per Night")]
        public decimal PricePerNight { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; } = true;

        public string? Description { get; set; }
    }
}