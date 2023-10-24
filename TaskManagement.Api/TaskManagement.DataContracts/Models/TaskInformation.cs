using System;
using System.Collections.Generic;

namespace TaskManagement.DataContracts.Models;

public partial class TaskInformation
{
    public int Taskid { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? Duedate { get; set; }

    public int? Status { get; set; }

    public int? Createdby { get; set; }

    public DateTime? Createdon { get; set; }

    public int? Taskstatus { get; set; }

    public DateTime? Updatedon { get; set; }

    public DateTime? Deletedon { get; set; }

    public int? Updatedby { get; set; }

    public int? Deletedby { get; set; }
}
