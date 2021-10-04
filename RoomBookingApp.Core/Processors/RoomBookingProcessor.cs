using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Domain;
using RoomBookingApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using RoomBookingApp.Core.Enums;
using RoomBookingApp.Domain.BaseModels;

namespace RoomBookingApp.Core.Processors
{
    public class RoomBookingProcessor : IRoomBookingProcessor
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
            var result = CreateRoomBookingObject<RoomBookingResult>(bookingrequest);
            if (availableRooms.Any())
            {
                var room = availableRooms.First();
                var roombooking = CreateRoomBookingObject<RoomBooking>(bookingrequest);
                roombooking.RoomId = room.Id;
                _roomBookingService.Save(roombooking);

                result.RoomBookingId = (int?)roombooking.Id;
                result.Flag = BookingResultFlag.Success;
            }
            else
            {
                result.Flag = BookingResultFlag.Failure;
            }


            return result;
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