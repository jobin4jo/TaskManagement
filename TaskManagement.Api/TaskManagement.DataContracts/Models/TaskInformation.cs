using System;
using System.Collections.Generic;

namespace TaskManagement.DataContracts.Models;

public partial class TaskInformation
{
    public int Taskid { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateOnly? Duedate { get; set; }

    public int? Status { get; set; }

    public int? Createdby { get; set; }

    public DateOnly? Createdon { get; set; }
}
