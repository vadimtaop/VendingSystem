using System;
using System.Collections.Generic;

namespace VendingSystemApi.Models;

public partial class MessageHistory
{
    public int MessageHistotyId { get; set; }

    public string? Type { get; set; }

    public string? Note { get; set; }

    public DateOnly? Date { get; set; }
}
