using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataContracts;
using TaskManagement.DataContracts.Models;
using TaskManagment.Common.Authentication;

namespace TaskManagement.Data;

public class UserRepository : IUserRepository
{
    private readonly DBContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    public UserRepository(DBContext context,IMapper mapper,IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;  
        _configuration = configuration; 
    }

    public async  Task<int> AdminUserRegisteration(AdminRegisterationRequestDTO userLoginRequest)
    {
        try
        {
            User userrequest= _mapper.Map<User>(userLoginRequest);
            userrequest.Role = "Admin";
            userrequest.Status = 1.ToString();
            _context.Users.Add(userrequest);
            await _context.SaveChangesAsync();
            return userrequest.Id;

        }
        catch(Exception ex)
        {
            return 0;
        }
    }

    public async Task<User> CreateUserbyAdmin(UserRequestDTO userRequest)
    {
      User userrequest = _mapper.Map<User>(userRequest);
            userrequest.Role = "User";
            userrequest.Status = 1.ToString();
           
            _context.Users.Add(userrequest);
            await _context.SaveChangesAsync();
            return userrequest;
     }

    public async Task<List<User>> GetAdminList()
    {
        List<User> data =  _context.Users.Where(x => x.Role == "Admin").ToList();
        return data;
     }

    public async  Task<UserResponseDTO> UserLogin(userLoginRequestDTO userLoginRequest)
    {
        UserResponseDTO data = new UserResponseDTO();
        User userdata= _context.Users.FirstOrDefault(x=>x.Name.ToLower()==userLoginRequest.Name.ToLower()&& x.Password==userLoginRequest.Password);
        if (userdata == null)
        {
            data.Role = null;
            data.AcessToken = "";
            data.Name = "";
            return data;
        }
        else
        {
            data.Role = userdata.Role;
            data.Name = userdata.Name;
            data.AcessToken = Jwt.GenerateToken(userdata.Role,userdata.Id, this._configuration["Jwt:Key"], this._configuration["Jwt:Issuer"]);
            return data;
        }
      
    }
}
