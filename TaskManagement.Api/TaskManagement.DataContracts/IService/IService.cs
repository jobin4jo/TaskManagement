using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataContracts.Models;

namespace TaskManagement.DataContracts;

public  interface IService
{
    Task<int>checkUserAvailability(string name,string phonenumber);
    string TaskStatusMapping(int? taskStatus);
}
