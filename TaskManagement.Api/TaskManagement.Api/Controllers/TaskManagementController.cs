using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.DataContracts;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagementController : ControllerBase
    {
        private readonly ITaskRepository _taskrepository;
        public TaskManagementController(ITaskRepository taskRepository)
        {
            _taskrepository = taskRepository;   
        }
        [HttpPost("CreateTask")]
        public async Task<ActionResult> CreateTaskinformation(TaskInformationRequestDTO taskInformationRequest)
        {
            try
            {
                var taskData = await _taskrepository.CreateTask(taskInformationRequest);
                return new CreatedResult(string.Empty, new { Code = 200, Status = true, Message = "DATA_INSERT_SUCCESS", Data = new { data = taskData } });
            }
            catch (Exception ex)
            {
                return new CreatedResult(string.Empty, new { Code = 401, Status = false, Message = ex, Data = new { } });
            }
        }
        [HttpGet("List")]
        public async Task<ActionResult> GetTaskList()
        {
            try
            {
                var data = _taskrepository.GetTaskInformationList();
                return new CreatedResult(string.Empty, new { Code = 200, Status = true, Message = "", Data = data });
            }
            catch (Exception ex)
            {
                return new CreatedResult(string.Empty, new { Code = 401, Status = false, Message = ex, Data = new { } });
            }
        }

        [HttpGet("TaskbyId")]
        public async Task<ActionResult>GetTaskInformationById(int id)
        {
            try
            {
                var taskInformation = await _taskrepository.GetTaskInformationById(id);
                return new CreatedResult(string.Empty, new { Code = 200, Status = true, Message = "", Data = taskInformation });
            }
            catch (Exception ex)
            {
                return new CreatedResult(string.Empty, new { Code = 401, Status = false, Message = ex, Data = new { } });
            }
        }
        [HttpDelete("DeleteById")]
        public async Task<ActionResult> DeleteTaskInformationbyId(int id )
        {
            try
            {
                var deleteresponse = await _taskrepository.DeleteTaskById(id);
                return new CreatedResult(string.Empty, new { Code = 200, Status = true, Message = "DATA_DELETE_SUCCESS", Data = deleteresponse });

            }
            catch (Exception ex)
            {
                return new CreatedResult(string.Empty, new { Code = 401, Status = false, Message = ex, Data = new { } });
            }
        }
        [HttpPut("UpdateTask")]
        public async Task<ActionResult>UpdateTaskInformation(UpdateTaskInformationRequestDTO updateTaskRequest)
        {
            try
            {
                var updateTask = await _taskrepository.UpdateTask(updateTaskRequest); 
                return new CreatedResult(string.Empty, new { Code = 200, Status = true, Message = "DATA_UPDATE_SUCCESS", Data = updateTask });
            }
            catch (Exception ex)
            {
                return new CreatedResult(string.Empty, new { Code = 401, Status = false, Message = ex, Data = new { } });
            }
        }
    }
}
