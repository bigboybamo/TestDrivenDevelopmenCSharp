using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using RoomBookingApp.Core.DataServices;
using Moq;
using RoomBookingApp.Core.Domain;
using RoomBookingApp.Core.Enums;

namespace RoomBookingApp.Core
{
    public class RoomBookingrequestprocessortest
    {
        private RoomBookingProcessor _processor;
        private RoomBookingrequest _Request;
        private Mock<IRoomBookingService> _roomBookingServiceMock;
        private List<Room> _availableRooms;

        public RoomBookingrequestprocessortest()
        {
            //Arrange


            _Request = new RoomBookingrequest
            {
                FullName = "Test name",
                Email = "test@test.com",
                Date = new DateTime(2021, 09, 29)
            };
            _availableRooms = new List<Room> { new Room() { id = 1 } };
            _roomBookingServiceMock = new Mock<IRoomBookingService>();
            _roomBookingServiceMock.Setup(x => x.GetAvailableRooms(_Request.Date))
                .Returns(_availableRooms);
            _processor = new RoomBookingProcessor(_roomBookingServiceMock.Object);


        }
        [Fact]
        public void Should_Return_Room_Booking_Response_with_Request_values()
        {
            //Arrange portion


            //var processor = new RoomBookingProcessor();

            //Act portion
            RoomBookingResult result = _processor.BookRoom(_Request);

            //Assert
            //Assert.NotNull(result);
            //Assert.Equal(Request.FullName, result.FullName);
            //Assert.Equal(Request.Email, result.Email);
            //Assert.Equal(Request.Date, result.Date);

            result.ShouldNotBeNull();
            result.FullName.ShouldBe(_Request.FullName);
            result.Email.ShouldBe(_Request.Email);
            result.Date.ShouldBe(_Request.Date);


        }

        [Fact]
        public void Should_Throw_exception_for_null_Request()
        {
            //Arrange
            //since we are returning or want to return a null exception, no arrange is needed


            //Act


            //Assert
            var exception = Should.Throw<ArgumentNullException>(() => _processor.BookRoom(null));
            exception.ParamName.ShouldBe("bookingrequest");
        }

        [Fact]
        public void Should_save_room_booking_request()
        {
            //Arrange
            RoomBooking savedBooking = null;
            _roomBookingServiceMock.Setup(x => x.Save(It.IsAny<RoomBooking>()))
                .Callback<RoomBooking>(booking =>
                {
                    savedBooking = booking;
                });


            _processor.BookRoom(_Request);

            _roomBookingServiceMock.Verify(x => x.Save(It.IsAny<RoomBooking>()), Times.Once);

            savedBooking.ShouldNotBeNull();
            savedBooking.FullName.ShouldBe(_Request.FullName);
            savedBooking.Email.ShouldBe(_Request.Email);
            savedBooking.Date.ShouldBe(_Request.Date);
            savedBooking.RoomId.ShouldBe(_availableRooms.First().id);
        }

        [Fact]
        public void Should_not_save_room_booking_request_if_None_Available()
        {
            _availableRooms.Clear();
            _processor.BookRoom(_Request);
            _roomBookingServiceMock.Verify(x => x.Save(It.IsAny<RoomBooking>()), Times.Never);

        }

        [Theory]
        [InlineData(BookingResultFlag.Failure, false)]
        [InlineData(BookingResultFlag.Success, true)]
        public void Should_return_SuccesorFailure_flag_in_result(BookingResultFlag BookingResultFlag, bool isAvailable)
        {
            if (!isAvailable)
            {
                _availableRooms.Clear();
            }

            var result = _processor.BookRoom(_Request);
            BookingResultFlag.ShouldBe(result.Flag);

        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(null, false)]
        public void Should_Return_RoomBookingId_in_Result(int? roombookingId, bool isAvailable)
        {
            if (!isAvailable)
            {
                _availableRooms.Clear();
            }

            else
            {
                _roomBookingServiceMock.Setup(x => x.Save(It.IsAny<RoomBooking>()))
                .Callback<RoomBooking>(booking =>
                {
                    booking.Id = roombookingId.Value;
                });
            }

            var result = _processor.BookRoom(_Request);
            result.RoomBookingId.ShouldBe(roombookingId);
        }

    }

}
