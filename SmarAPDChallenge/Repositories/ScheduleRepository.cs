using Microsoft.EntityFrameworkCore;
using SmarAPDChallenge.Data;
using SmarAPDChallenge.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SmarAPDChallenge.Repositories
{
    public class ScheduleRepository
    {
        private readonly ApplicationDbContext _context;

        public ScheduleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<ScheduleModel> GetSchedules()
        {
            return _context.Schedules;
        }

        public IQueryable<ScheduleModel> GetRoomSchedules(int id)
        {
            return _context.Schedules.Where(x => x.RoomId == id);
        }

        public ScheduleModel find(int id)
        {
            return _context.Schedules.Find(id);
        }

        public void create(ScheduleModel schedule)
        {
            _context.Schedules.Add(schedule);
        }

        public void update(ScheduleModel schedule)
        {
            var entry = _context.Entry(schedule);
            _context.Attach(schedule);
            entry.State = EntityState.Modified;
        }

        public void delete(ScheduleModel schedule)
        {
            _context.Remove(schedule);
        }

        public IQueryable<ScheduleModel> GetSchedulesByRange(DateTime? inicio, DateTime? fim)
        {
            IQueryable<ScheduleModel> query = _context.Schedules;

            if (inicio.HasValue && fim.HasValue)
            {                
                query = query.Where(x => x.TimeStart <= fim.Value && x.TimeEnd >= inicio.Value);
            }

            query = query.OrderBy(x => x.TimeStart);

            return query;
        }

        public IQueryable<ScheduleModel> GetSchedulesByRangeAndRoom(DateTime inicio, DateTime fim, int roomId)
        {
            IQueryable<ScheduleModel> query = _context.Schedules
                .Where(x => x.TimeStart <= fim && x.TimeEnd >= inicio)
                .Where(x => x.RoomId == roomId)
                .OrderBy(x => x.TimeStart);

            return query;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
