using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataContracts.Models;

namespace TaskManagement.DataContracts;

public  interface IUserRepository
{

    Task<int> AdminUserRegisteration(AdminRegisterationRequestDTO userLoginRequest);
    Task<List<User>> GetAdminList();
    Task<User> CreateUserbyAdmin(UserRequestDTO userRequest);
    Task<UserResponseDTO> UserLogin(userLoginRequestDTO userLoginRequest);



}
