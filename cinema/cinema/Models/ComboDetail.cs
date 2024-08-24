using System;
using System.Collections.Generic;

namespace cinema.Models;

public partial class ComboDetail
{
    public int Id { get; set; }

    public int ComboId { get; set; }

    public int BookingId { get; set; }

    public int Quantity { get; set; }

    public bool Status { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Combo Combo { get; set; } = null!;
}
