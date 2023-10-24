using AutoMapper;
using TaskManagement.DataContracts;
using TaskManagement.DataContracts.Models;

namespace TaskManagement.Data.MappingProfile;

public  class TaskMappingProfiles:Profile
{
    public TaskMappingProfiles()
    {
        CreateMap<TaskInformationRequestDTO, TaskInformation>();
        CreateMap<TaskInformation, TaskInformationResponseDTO>();
    }
}
