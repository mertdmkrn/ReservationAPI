using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReservationAPI.Model
{
    [Table("Reservation")]
    public class Reservation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required, MaxLength(100)]
        public string CustomerName { get; set; }


        [Required, MaxLength(100)]
        public string CustomerEmail { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public int GuestCount { get; set; }

        [Required]
        public Guid? TableId { get; set; } = Guid.Empty;

        [JsonIgnore]
        public Table? Table { get; set; }
    }
}
