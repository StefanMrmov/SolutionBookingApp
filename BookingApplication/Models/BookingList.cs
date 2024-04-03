using System.ComponentModel.DataAnnotations;

namespace BookingApplication.Models
{
    public class BookingList
    {
        [Key]
        public Guid Id { get; set; }
        public virtual ICollection<BookReservation>? bookReservations { get; set; }
        public Guid OwnerId { get; set; }
    }
}
