using RoomBookingApp.Core.Models;
using System;

namespace RoomBookingApp.Core.Processors
{
    public class RoomBookingProcessor
    {
        public RoomBookingProcessor()
        {

        }

        public RoomBookingResult BookRoom(RoomBookingrequest bookingrequest)
        {
            return new RoomBookingResult
            {
                FullName = bookingrequest.FullName,
                Email = bookingrequest.Email,
                Date = bookingrequest.Date

            };
        }
    }
}