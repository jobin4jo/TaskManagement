using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using TaskManagement.Api.Controllers;
using TaskManagement.DataContracts;

namespace TestProject_TaskMangement;


public  class UnitTestController
{
    private readonly Mock<ITaskRepository> _taskRepository;
    public UnitTestController()
    {
        _taskRepository = new Mock<ITaskRepository>();
    }

    //Task get case 
    [Fact]

    public async Task GetTaskList_ReturnsData()
    {
        // Arrange
        
        var _taskRepository = new Mock<ITaskRepository>();
        var testData = Gettask();
        _taskRepository.Setup(repo => repo.GetTaskInformationList()).ReturnsAsync(testData);

        var controller = new TaskManagementController(_taskRepository.Object);

        // Act
        var result = await controller.GetTaskList();

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var responseData = okResult.Value as List<TaskInformationResponseDTO>;
        Assert.NotNull(responseData);
        Assert.Same(testData, responseData);
    }
    [Fact]
    public async Task GetTaskList_ReturnsErrorResult()
    {
        // Arrange
        var exception = new Exception("Test exception message");
        var taskRepositoryMock = new Mock<ITaskRepository>();
        _taskRepository.Setup(repo => repo.GetTaskInformationList()).ThrowsAsync(exception);
        var controller = new TaskManagementController(_taskRepository.Object);

        // Act
        var result = await controller.GetTaskList();

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal(201, createdResult.StatusCode);
        var responseData = createdResult.Value as List<TaskInformationResponseDTO>;
        Assert.Null(responseData);
       
    }

    //GetbyId Criteria 
    [Fact]
    public async Task GetTaskInformationById_ReturnsOkResult()
    {
        // Arrange
        int id =2;
        TaskInformationResponseDTO data = GetTaskbyId();


        _taskRepository.Setup(repo => repo.GetTaskInformationById(id)).ReturnsAsync(data);
        var controller = new TaskManagementController(_taskRepository.Object);

        // Act
        var result = await controller.GetTaskInformationById(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        var responseData = okResult.Value as TaskInformationResponseDTO;
        
        Assert.NotNull(responseData);
      
        Assert.Same(data, responseData);
    }

    [Fact]
    public async Task GetTaskInformationById_ReturnsError()
    {
        // Arrange
        var id = 2;
        var _taskRepository = new Mock<ITaskRepository>();
        TaskInformationResponseDTO data = GetTaskbyId();
        _taskRepository.Setup(repo => repo.GetTaskInformationById(id)).ThrowsAsync(new Exception("Sample Exception"));

        var controller = new TaskManagementController(_taskRepository.Object);

        // Act
        var result = await controller.GetTaskInformationById(id);

        // Assert
        var okResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal(201, okResult.StatusCode);
        var responseData = okResult.Value as TaskInformationResponseDTO;
        Assert.Null(responseData);
       
    }

    [Fact]
    public async Task CreateTaskinformation_ReturnsCreatedResult()
    {
        // Arrange
        var request = new TaskInformationRequestDTO();
        request.Title = "sample title";
        request.Description = "Description";
        request.Createdby = 1;
        request.Duedate= DateTime.UtcNow.AddDays(8);

        var expectedTaskData = 1;
        var userIdClaim = new Claim("jti", "1"); 
        var identity = new ClaimsIdentity(new[] { userIdClaim });
        var claimsPrincipal = new ClaimsPrincipal(identity);

        var _taskRepository = new Mock<ITaskRepository>();
        _taskRepository.Setup(repo => repo.CreateTask(request, int.Parse(userIdClaim.Value)))
                         .ReturnsAsync(expectedTaskData);

        var controller = new TaskManagementController(_taskRepository.Object);
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = claimsPrincipal }
        };

        // Act
        var result = await controller.CreateTaskinformation(request);

        // Assert
        var createdResult = Assert.IsType<OkObjectResult>(result);
       
        var responseData = createdResult.Value as int?;
        Assert.Equal(200,createdResult.StatusCode);
        Assert.NotNull(responseData);
       
        Assert.NotNull(responseData);
        Assert.Equal(expectedTaskData, responseData);
        
      
    }

    //update case
    [Fact]
    public async Task UpdateTaskInformation_ReturnsSuccess()
    {
        // Arrange
        var updateTaskRequest = new UpdateTaskInformationRequestDTO
        {
            Taskid = 1,
            Description = "sample",
            Duedate = DateTime.UtcNow.AddDays(5),
            Taskstatus = 2,
            Title = "sample title"
        };

        var userId = 1;

        var _taskRepository = new Mock<ITaskRepository>();
        var updatedTask = 1;
        _taskRepository.Setup(repo => repo.UpdateTask(updateTaskRequest, userId)).ReturnsAsync(updatedTask);

        var controller = new TaskManagementController(_taskRepository.Object);

        // Act
        var result = await controller.UpdateTaskInformation(updateTaskRequest);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result);
        var response = Assert.IsType<ApiResponse>(createdResult.Value);

        Assert.Equal(200, response.Code);
        Assert.True(response.Status);
        Assert.Equal("DATA_UPDATE_SUCCESS", response.Message);

        var data = Assert.IsType<int>(response.Data);
    
    }

    [Fact]
    public async Task UpdateTaskInformation_ReturnsError()
    {
        // Arrange
        var updateTaskRequest = new UpdateTaskInformationRequestDTO
        {
            Taskid = 1,
            Description = "sample",
            Duedate = DateTime.Now,
            Taskstatus = 2,
            Title = "sample title"

        };

        var userId = 123; // Replace with a valid user ID.

        var _taskRepository = new Mock<ITaskRepository>();
        _taskRepository.Setup(repo => repo.UpdateTask(updateTaskRequest, userId)).ThrowsAsync(new Exception("Sample Exception"));

        var controller = new TaskManagementController(_taskRepository.Object);

        // Act
        var result = await controller.UpdateTaskInformation(updateTaskRequest);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result);
        var response = Assert.IsType<ApiResponse>(createdResult.Value);

        Assert.Equal(401, response.Code);
        Assert.False(response.Status);
        Assert.Equal("Sample Exception", response.Message);
        Assert.NotNull(response.Data);
    }

    private List<TaskInformationResponseDTO> Gettask()
    {
        List<TaskInformationResponseDTO> data = new List<TaskInformationResponseDTO>()
        {
            new TaskInformationResponseDTO
            {
                
            Taskid= 1,
            Title= "API Documentation",
            Description= "API documentation for Application layer",
            //Duedate= "2023-10-27",
            Status= 1,
            Createdby= 1,
            //Createdon= "2023-10-27T16:18:14.080029Z",
            Taskstatus= 1,
            TaskStatusDetail= "Task Asign "

            }
           
           
        };
        return data;
    }
    private TaskInformationResponseDTO GetTaskbyId()
    {
        TaskInformationResponseDTO result = new TaskInformationResponseDTO();
        result.Taskid = 2;
        result.Title = "API Documentation";
        result.Description = "API documentation for Application layer";
        result.Status = 1;
        result.Createdby = 1;
        result.Taskstatus = 1;
        result.TaskStatusDetail = "Task Asign ";
        return result;
    }
}



public class ApiResponse
{
    public int Code { get; set; }
    public bool Status { get; set; }
    public string Message { get; set; }
    public dynamic Data { get; set; }
}