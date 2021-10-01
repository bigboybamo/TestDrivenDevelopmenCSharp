using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingApp.Persistence.Repositories
{
    public class RoomBookingService : IRoomBookingService
    {
        private readonly RoomBookingAppDbContext _context;
        public RoomBookingService(RoomBookingAppDbContext context)
        {
            this._context = context;
        }
        public IEnumerable<Room> GetAvailableRooms(DateTime Date)
        {
            return _context.Rooms.Where(x => !x.RoomBookings.Any(e => e.Date == Date)).ToList();

        }

        public void Save(RoomBooking roomBooking)
        {
            _context.Add(roomBooking);
            _context.SaveChanges();
        }
    }
}
