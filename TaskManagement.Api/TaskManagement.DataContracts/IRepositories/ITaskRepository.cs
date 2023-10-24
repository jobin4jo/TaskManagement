using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataContracts;

public interface ITaskRepository
{
   Task<int>  CreateTask(TaskInformationRequestDTO taskInformationRequest,int userId);
    Task<int>UpdateTask(UpdateTaskInformationRequestDTO taskInformationRequest,int userId);  
    Task<TaskInformationResponseDTO>GetTaskInformationById(int id); 
    Task<int> DeleteTaskById(int id,int userId );
    Task<List<TaskInformationResponseDTO>> GetTaskInformationList();

}
