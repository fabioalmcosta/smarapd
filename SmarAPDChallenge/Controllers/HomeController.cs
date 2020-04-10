using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmarAPDChallenge.Models;
using SmarAPDChallenge.Services;
using SmarAPDChallenge.ViewModels.Home;

namespace SmarAPDChallenge.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private ScheduleService _sheduleService;

        public HomeController(ScheduleService scheduleService, IHttpContextAccessor httpContextAccessor)
        {
            _sheduleService = scheduleService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index(string inicio= null, string fim = null)
        {
            HomeListViewModel data;

            if (!String.IsNullOrEmpty(inicio) && !String.IsNullOrEmpty(fim))
            {
                var dataInicio = DateTime.ParseExact(inicio, "dd/MM/yyyy HH:mm:ss", null);
                var dataFim = DateTime.ParseExact(fim, "dd/MM/yyyy HH:mm:ss", null);
                data = GetItems(dataInicio, dataFim);
            }
            else
            {
                data = GetItems(null, null);
            }

            return View(data);
        }

        [HttpGet]
        public IActionResult CreateModal()
        {
            var data = new ScheduleCreateViewModal();

            data.Rooms = new SelectList(
                _sheduleService.GetRooms().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(), "Value", "Text");

            return PartialView(data);
        }

        [HttpPost]
        public IActionResult Create(ScheduleCreateViewModal model)
        {
            var available = _sheduleService.GetAvailableRoom(model).ToList();

            if (available.Count() == 0)
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _sheduleService.CreateSchedule(model, UserId);

                var data = GetItems(null, null);

                return PartialView("ItemsTable", data);
            }
            else
            {
                var response = "<br />";
                foreach (var item in available)
                {
                    response = response + "Title: " + item.Title + " Start: " + item.TimeStart.ToString("dd/MM/yyyy HH:mm:ss") + " End: " + item.TimeEnd.ToString("dd/MM/yyyy HH:mm:ss") + "<br />";
                }
                return BadRequest("There are already conflicting times for the Requested schedule!" + response);
            }

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _sheduleService.DeleteSchedule(id);
            var data = GetItems(null, null);
            return PartialView("ItemsTable", data);
        }

        private HomeListViewModel GetItems(DateTime? inicio, DateTime? fim)
        {
            var data = new HomeListViewModel();

            data.items = _sheduleService.GetSchedulesByRange(inicio, fim).Select(x => new HomeListItemViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                TimeStart = x.TimeStart,
                TimeEnd = x.TimeEnd,
                Room = x.Room,
                UserName = x.User.UserName
            }).ToList();

            return data;
        }
    }
}
