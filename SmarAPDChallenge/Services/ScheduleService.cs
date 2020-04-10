using SmarAPDChallenge.Models;
using SmarAPDChallenge.Repositories;
using SmarAPDChallenge.ViewModels.Home;
using SmarAPDChallenge.ViewModels.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarAPDChallenge.Services
{
    public class ScheduleService
    {
        private ScheduleRepository _scheduleRepo;
        private RoomRepository _roomRepo;

        public ScheduleService(ScheduleRepository scheduleRepo, RoomRepository roomRepo)
        {
            _scheduleRepo = scheduleRepo;
            _roomRepo = roomRepo;
        }

        public IQueryable<ScheduleModel> GetSchedules()
        {
            return _scheduleRepo.GetSchedules();
        }

        public IQueryable<ScheduleModel> GetSchedulesByRange(DateTime? inicio, DateTime? fim)
        {
            return _scheduleRepo.GetSchedulesByRange(inicio, fim);
        }

        public IQueryable<RoomModel> GetRooms(string name = null)
        {
            return _roomRepo.GetRooms(name);
        }

        public void CreateSchedule(ScheduleCreateViewModal schedule, string UserId)
        {
            var available = _scheduleRepo.GetSchedulesByRange(schedule.TimeStart, schedule.TimeEnd).ToList();

            if (available.Count() == 0)
            {
                _scheduleRepo.create(new ScheduleModel()
                {
                    Title = schedule.Title,
                    TimeStart = (DateTime)schedule.TimeStart,
                    TimeEnd = (DateTime)schedule.TimeEnd,
                    RoomId = schedule.RoomId,
                    UserId = UserId
                });

                _scheduleRepo.SaveChanges();
            }
            else
            {
                throw new Exception();
            }
        }

        public void DeleteSchedule(int id)
        {
            var schedule = _scheduleRepo.find(id);
            _scheduleRepo.delete(schedule);

            _scheduleRepo.SaveChanges();

        }

        public void CreateRoom(RoomCreateViewModel room)
        {
            _roomRepo.create(new RoomModel()
            {
                Name = room.Name
            });

            _roomRepo.SaveChanges();
        }

        public void DeleteRoom(int id)
        {
            var room = _roomRepo.find(id);
            _roomRepo.delete(room);

            _roomRepo.SaveChanges();
        }

        public IQueryable<ScheduleModel> GetRoomSchedules(int id)
        {
            return _scheduleRepo.GetRoomSchedules(id);
        }

        public IQueryable<ScheduleModel> GetAvailableRoom(ScheduleCreateViewModal schedule)
        {
            return _scheduleRepo.GetSchedulesByRangeAndRoom((DateTime)schedule.TimeStart, (DateTime)schedule.TimeEnd, schedule.RoomId);
        }
    }
}
