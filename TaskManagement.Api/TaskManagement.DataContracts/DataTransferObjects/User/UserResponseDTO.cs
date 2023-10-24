using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataContracts;

public class UserResponseDTO
{
    public string? AcessToken { get; set; } 
    public string? Name { get; set; }
    public string? Role { get; set; }

}
