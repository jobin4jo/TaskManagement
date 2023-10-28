using AutoMapper;
using TaskManagement.DataContracts;
using TaskManagement.DataContracts.Models;

namespace TaskManagement.Data;

public class UserMappingProfiles : Profile
{
    public UserMappingProfiles()
    {

        CreateMap<AdminRegisterationRequestDTO, User>().ReverseMap();
        CreateMap<UserRequestDTO, User>().ReverseMap();
        
    }
}
