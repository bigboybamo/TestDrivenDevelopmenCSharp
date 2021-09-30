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

namespace RoomBookingApp.Core
{
    public class RoomBookingrequestprocessortest
    {
        private RoomBookingProcessor _processor;
        private RoomBookingrequest _Request;
        private Mock<IRoomBookingService> _roomBookingServiceMock;

        public RoomBookingrequestprocessortest()
        {
            //Arrange


            _Request = new RoomBookingrequest
            {
                FullName = "Test name",
                Email = "test@test.com",
                Date = new DateTime(2021, 09, 29)
            };

            _roomBookingServiceMock = new Mock<IRoomBookingService>();
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
        }
    }
}
