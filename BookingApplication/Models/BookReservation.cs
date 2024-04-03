using System.ComponentModel.DataAnnotations;

namespace BookingApplication.Models
{
    public class BookReservation
    {
        [Key]
        public Guid Id { get; set; }
        public Reservation? reservation { get; set; }
        public BookingList? bookingList { get; set; }
        public Guid reservationId { get; set; }
        public Guid bookingListId { get; set; }
        public int Number_of_nights { get; set; }
    }
}
