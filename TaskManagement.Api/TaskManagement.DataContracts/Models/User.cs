﻿using System;
using System.Collections.Generic;

namespace TaskManagement.DataContracts.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Role { get; set; }

    public string? Status { get; set; }

    public string? Password { get; set; }

    public string? Phonenumber { get; set; }
}
