using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReservationAPI.Model
{
    [Table("Table")]
    public class Table
    {
        public Table()
        {
            this.Reservations = new HashSet<Reservation>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [MaxLength(6)]
        public string Number { get; set; }
        public int Capacity { get; set; }      
        public virtual IEnumerable<Reservation> Reservations { get; set; }      
    }
}
