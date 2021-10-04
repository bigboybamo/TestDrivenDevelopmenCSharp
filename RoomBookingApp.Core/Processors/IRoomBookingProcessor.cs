using RoomBookingApp.Core.Models;

namespace RoomBookingApp.Core.Processors
{
    public interface IRoomBookingProcessor
    {
        RoomBookingResult BookRoom(RoomBookingrequest bookingrequest);
    }
}