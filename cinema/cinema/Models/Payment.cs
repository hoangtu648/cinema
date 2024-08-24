using System;
using System.Collections.Generic;

namespace cinema.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public int PaymentType { get; set; }

    public string TransactionNo { get; set; } = null!;

    public int TicketNumber { get; set; }

    public string Qr { get; set; } = null!;

    public DateTime Created { get; set; }

    public string Description { get; set; } = null!;

    public double Price { get; set; }

    public bool Status { get; set; }

    public virtual Booking Booking { get; set; } = null!;
}
