using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataContracts;

public class TaskInformationRequestDTO
{
   public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime Duedate { get; set; }
    public int? Createdby { get; set; }
}

public class UpdateTaskInformationRequestDTO
{
    public int Taskid { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? Duedate { get; set; }


    public int? Taskstatus { get; set; }


}


