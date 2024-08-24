using System;
using System.Collections.Generic;

namespace cinema.Models;

public partial class Showtime
{
    public int Id { get; set; }

    public int MovieId { get; set; }

    public int CinemaId { get; set; }

    public DateTime ShowDate { get; set; }

    public int SubId { get; set; }

    public int RoomId { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Cinema Cinema { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;

    public virtual Sub Sub { get; set; } = null!;
}
