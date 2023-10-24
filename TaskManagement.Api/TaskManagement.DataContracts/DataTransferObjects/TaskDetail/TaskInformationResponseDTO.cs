using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataContracts;

public class TaskInformationResponseDTO
{
    public int Taskid { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateOnly? Duedate { get; set; }

    public int? Status { get; set; }
    public string TaskStatusDetail { get; set; }
    public int? Taskstatus { get; set; }

    public int? Createdby { get; set; }

    public DateOnly? Createdon { get; set; }
}
