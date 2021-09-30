using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;

namespace RoomBookingApp.Core
{
    public class RoomBookingrequestprocessortest
    {
        [Fact]
        public void Should_Return_Room_Booking_Response_with_Request_values()
        {
            //Arrange portion
            var Request = new RoomBookingrequest
            {
                FullName = "Test name",
                Email = "test@test.com",
                Date = new DateTime(2021, 09, 29)
            };

            var processor = new RoomBookingProcessor();

            //Act portion
            RoomBookingResult result = processor.BookRoom(Request);

            //Assert
            //Assert.NotNull(result);
            //Assert.Equal(Request.FullName, result.FullName);
            //Assert.Equal(Request.Email, result.Email);
            //Assert.Equal(Request.Date, result.Date);

            result.ShouldNotBeNull();
            result.FullName.ShouldBe(Request.FullName);
            result.Email.ShouldBe(Request.Email);
            result.Date.ShouldBe(Request.Date);


        }
    }
}
