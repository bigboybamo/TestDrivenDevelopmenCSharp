﻿using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

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

            var availableRooms = _roomBookingService.GetAvailableRooms(bookingrequest.Date);
            if (availableRooms.Any())
            {
                var room = availableRooms.First();
                var roombooking = CreateRoomBookingObject<RoomBooking>(bookingrequest);
                roombooking.RoomId = room.id;
                _roomBookingService.Save(roombooking);
            }



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