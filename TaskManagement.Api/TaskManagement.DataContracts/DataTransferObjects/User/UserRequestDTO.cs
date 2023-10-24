using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataContracts;

public class UserRequestDTO
{

    public string? Name { get; set; }

    public string? Role { get; set; }

    public string? Status { get; set; }

    public string? Password { get; set; }

    public string? Phonenumber { get; set; }
}



public class AdminRegisterationRequestDTO
{
    [Required(ErrorMessage = " Name is required")]
    public string? Name { get; set; }



    public string? Status { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required(ErrorMessage = " PhoneNumber is required")]
    public string? Phonenumber { get; set; }
}

public class userLoginRequestDTO
{
    [Required(ErrorMessage = " Name is required")]
    public string? Name { get; set; }
    [Required(ErrorMessage = " Password  is required")]
    public string? Password { get; set; }
}