using MeetingRoomBooking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Repository.Interfaces
{
    public interface IUserRepository
    {
        TUser GetById(string UserId);
        bool IsExist(TUser user);
    }
}
