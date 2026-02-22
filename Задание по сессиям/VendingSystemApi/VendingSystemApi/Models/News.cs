using System;
using System.Collections.Generic;

namespace VendingSystemApi.Models;

public partial class News
{
    public int NewId { get; set; }

    public DateOnly? Date { get; set; }

    public string? Note { get; set; }
}
