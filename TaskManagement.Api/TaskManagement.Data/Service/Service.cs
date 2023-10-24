using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataContracts;
using TaskManagement.DataContracts.Models;

namespace TaskManagement.Data;

public class Service : IService
{
    private readonly DBContext _bcontext;
    public Service(DBContext bcontext)
    {
        _bcontext = bcontext;
    }

    public async Task<int> checkUserAvailability(string name, string phonenumber)
    {
        var response =  _bcontext.Users.Where(x => x.Name == name && x.Phonenumber == phonenumber).Select(x => x.Id).FirstOrDefault();
        return response > 0 ? response : 0;  
    }

    public string TaskStatusMapping(int? taskStatus)
    {
       
            switch (taskStatus)
            {
                case 1:
                    return "Task Asign ";
                    break;
                    case 2:
                    return "Development Progress";
                    break;
                case 3:
                    return "Development Completed ";
                    break;
                default:
                    return "";
                    
            }
        return "";
    }
}
