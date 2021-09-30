using RoomBookingApp.Core.Models;
using System;

namespace RoomBookingApp.Core.Domain

{
    public class RoomBooking : RoomBookingBase
    {
        public int RoomId { get; set; }
        public object Id { get; set; }
    }
}