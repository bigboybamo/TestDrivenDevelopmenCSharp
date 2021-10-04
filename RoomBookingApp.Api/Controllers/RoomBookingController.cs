using Microsoft.AspNetCore.Mvc;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomBookingApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomBookingController : Controller
    {
        private IRoomBookingProcessor _roomBookingProcessor;
        public RoomBookingController(IRoomBookingProcessor roomBookingProcessor)
        {
            this._roomBookingProcessor = roomBookingProcessor;
        }
        [HttpPost]
        public async Task<IActionResult> BookRoom(RoomBookingrequest request)
        {
            if (ModelState.IsValid)
            {
                var result = _roomBookingProcessor.BookRoom(request);
                if (result.Flag == Core.Enums.BookingResultFlag.Success)
                {
                    return Ok(result);
                }

                ModelState.AddModelError(nameof(RoomBookingrequest.Date), "No Rooms Available For Given Date");
            }

            return BadRequest(ModelState);
        }
    }
}
