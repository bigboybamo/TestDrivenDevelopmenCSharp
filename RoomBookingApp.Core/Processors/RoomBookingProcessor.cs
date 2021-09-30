using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections;

namespace RoomBookingApp.Core.Processors
{
    public class RoomBookingProcessor
    {

        private readonly IRoomBookingService _roomBookingService;

        public RoomBookingProcessor(IRoomBookingService roomBookingService)
        {
            this._roomBookingService = roomBookingService;
        }

        public RoomBookingResult BookRoom(RoomBookingrequest bookingrequest)
        {
            if (bookingrequest is null)
            {
                throw new ArgumentNullException(nameof(bookingrequest));
            }

            _roomBookingService.Save(CreateRoomBookingObject<RoomBooking>(bookingrequest));

            return CreateRoomBookingObject<RoomBookingResult>(bookingrequest);
        }
        private TRoomBooking CreateRoomBookingObject<TRoomBooking>(RoomBookingrequest bookingrequest) where TRoomBooking : RoomBookingBase, new()
        {
            return new TRoomBooking
            {
                FullName = bookingrequest.FullName,
                Email = bookingrequest.Email,
                Date = bookingrequest.Date
            };
        }
    }
}