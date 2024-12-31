using Evertrust.Core.Common.AspNetCore.Misc;
using MeetingRoomBooking.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Repository.Interfaces
{
    public interface IMeetingBookingRepository
    {
        IResult Create(TMeeingBooking booking);
        IResult Update(TMeeingBooking booking);
        IResult Delete(int Id);
        IEnumerable<ViewTmeeingBooking> GetAll(string targetDate, string RoomId,int id);        
        IEnumerable<TMeeingBooking> GetAll(DateTime getFirstDay, DateTime lastDay);
        TMeeingBooking GetById(int Id);
        
    }
}
