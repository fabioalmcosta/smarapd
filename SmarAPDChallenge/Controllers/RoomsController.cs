using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmarAPDChallenge.Services;
using SmarAPDChallenge.ViewModels.Rooms;

namespace SmarAPDChallenge.Controllers
{
    [Authorize]
    public class RoomsController : Controller
    {
        private ScheduleService _sheduleService;

        public RoomsController(ScheduleService scheduleService)
        {
            _sheduleService = scheduleService;
        }

        [HttpGet]
        public IActionResult Index(string name = null)
        {
            var data = GetItems(name);
            return View(data);
        }

        [HttpGet]
        public IActionResult CreateModal()
        {
            var data = new RoomCreateViewModel();
            return PartialView(data);
        }

        [HttpPost]
        public IActionResult Create(RoomCreateViewModel room)
        {
            _sheduleService.CreateRoom(room);
            var data = GetItems(null);
            return PartialView("ItemsTable", data);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var hasSchedule = _sheduleService.GetRoomSchedules(id).ToList();

            if (hasSchedule.Count() == 0)
            {
                _sheduleService.DeleteRoom(id);
                var data = GetItems(null);
                return PartialView("ItemsTable", data);
            }
            else
            {
                return BadRequest("There are room appointments!");
            }
        }

        private RoomListViewModel GetItems(string name = null)
        {
            var data = new RoomListViewModel();

            data.items = _sheduleService.GetRooms(name).Select(x => new RoomListItemViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return data;
        }
    }
}