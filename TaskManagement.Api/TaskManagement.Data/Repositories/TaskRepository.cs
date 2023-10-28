using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagement.DataContracts;
using TaskManagement.DataContracts.Models;

namespace TaskManagement.Data;

public class TaskRepository : ITaskRepository
{
    private readonly DBContext _context;
    private readonly IMapper _mapper;
    private readonly IService _service;
    public TaskRepository(DBContext context, IMapper mapper, IService service)
    {
        _context = context;
        _mapper = mapper;
        _service = service;
    }

    public async  Task<int> CreateTask(TaskInformationRequestDTO taskInformationRequest,int UserId)
    {
        TaskInformation taskData = _mapper.Map<TaskInformation>(taskInformationRequest);
        taskData.Status = 1;
        taskData.Taskstatus = 1;   ///Task Asign flag
        taskData.Createdon = DateTime.UtcNow;
        taskData.Duedate = taskInformationRequest.Duedate.Value.ToUniversalTime();
        taskData.Createdby = UserId;
        _context.TaskInformations.Add(taskData);
         await _context.SaveChangesAsync();
        return taskData.Taskid;
       
    }

    public async Task<int> DeleteTaskById(int id, int userId)
    {
        var taskdata = await _context.TaskInformations.FindAsync(id);
        taskdata.Status = 0;
        taskdata.Deletedby = userId;
        taskdata.Deletedon = DateTime.UtcNow;
        _context.Entry(taskdata).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return taskdata.Taskid;
    }

    public async  Task<TaskInformationResponseDTO> GetTaskInformationById(int id)
    {
        TaskInformation information = await _context.TaskInformations.Where(x => x.Status == 1 && x.Taskid == id).FirstOrDefaultAsync();
        TaskInformationResponseDTO  data= _mapper.Map<TaskInformationResponseDTO>(information);
        data.TaskStatusDetail = _service.TaskStatusMapping(data.Taskstatus);
        return data;

    }

    public  async Task<List<TaskInformationResponseDTO>> GetTaskInformationList()
    {
        List<TaskInformation> information = await _context.TaskInformations.Where(x => x.Status == 1).ToListAsync();
        List<TaskInformationResponseDTO> data = _mapper.Map<List<TaskInformationResponseDTO>>(information);
        data.ForEach(x => x.TaskStatusDetail = _service.TaskStatusMapping(x.Taskstatus));
        return data;

    }

    public async Task<int> UpdateTask(UpdateTaskInformationRequestDTO taskInformationRequest,int userId)
    {
        TaskInformation data = _mapper.Map<TaskInformation>(taskInformationRequest);
        data.Updatedon = DateTime.Now;
        data.Updatedby = userId;
        data.Status = 1;
        await _context.SaveChangesAsync();
        return data.Taskid;
    }
}
