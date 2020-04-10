using Microsoft.EntityFrameworkCore;
using SmarAPDChallenge.Data;
using SmarAPDChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarAPDChallenge.Repositories
{
    public class RoomRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<RoomModel> GetRooms(string name = null)
        {
            IQueryable<RoomModel> query = _context.Rooms;

            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            return query;
        }

        public RoomModel find(int id)
        {
            return _context.Rooms.Find(id);
        }

        public void create(RoomModel room)
        {
            _context.Rooms.Add(room);
        }

        public void update(RoomModel room)
        {
            var entry = _context.Entry(room);
            _context.Attach(room);
            entry.State = EntityState.Modified;
        }

        public void delete(RoomModel room)
        {
            _context.Remove(room);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
