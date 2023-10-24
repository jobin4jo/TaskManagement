using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.DataContracts;
using TaskManagement.DataContracts.Models;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IService _service;
        public UserController(IUserRepository userRepository, IService service)
        {
            _userRepository = userRepository;
            _service = service;
        }

        [HttpPost("Admin/Register")]
        public async Task<ActionResult> AdminRegisteration(AdminRegisterationRequestDTO userRequest)
        {
            try
            {
                if (await this._service.checkUserAvailability(userRequest.Name,userRequest.Phonenumber) == 0)
                {
                    var registerationdata = await _userRepository.AdminUserRegisteration(userRequest);
                    if(registerationdata > 0)
                    {
                        return new CreatedResult(string.Empty, new { Code = 200, Status = true, Message = "DATA_INSERT_SUCCESS", Data = new { userdId = registerationdata } });
                    }
                    else
                    {
                        return new CreatedResult(string.Empty, new { Code = 200, Status = true, Message = "DATA_INSERT_ERROR", Data = new { } });
                    }
                }
                else
                {
                    return new CreatedResult(string.Empty, new { Code = 200, Status = true, Message = "USER_EXISTS", Data = new { } });
                }
            }
            catch (Exception ex)
            {
                return new CreatedResult(string.Empty, new { Code = 401, Status = false, Message = ex, Data = new { } });
            }
        }

        /// <summary>
        /// this APi testing Purpose 
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "User,Admin")]
        [HttpGet("AdminList")]
        public async Task<ActionResult> GetAdminList()
        {
            //var data = User.Claims.FirstOrDefault(c => c.Type == "jti").Value;
            List<User> adminlist = await _userRepository.GetAdminList();
            return Ok(new { data = adminlist });
        }

        [HttpPost("CreateUserbyAdmin")]
        public async Task<ActionResult>CreateUserByAdmin(UserRequestDTO userReq)
        {
            try
            {
                var userResponse =await _userRepository.CreateUserbyAdmin(userReq);
                return new CreatedResult(string.Empty, new { Code = 200, Status = true, Message = "DATA_INSERT_SUCCESS", Data = new { data= userResponse} });
            }
            catch (Exception ex)
            {
                return new CreatedResult(string.Empty, new { Code = 401, Status = false, Message = ex, Data = new { } });
            }
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(userLoginRequestDTO userLogin)
        {
            try
            {
                var userData = await _userRepository.UserLogin(userLogin);
                return new CreatedResult(string.Empty, new { Code = 200, Status = true, Message = "", Data = userData });
            }
            catch (Exception ex)
            {
                return new CreatedResult(string.Empty, new { Code = 401, Status = false, Message = ex, Data = new { } });
            }
        }
    }
}
