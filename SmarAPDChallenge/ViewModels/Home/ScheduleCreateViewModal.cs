using Microsoft.AspNetCore.Mvc.Rendering;
using SmarAPDChallenge.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarAPDChallenge.ViewModels.Home
{
    public class ScheduleCreateViewModal
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Start Date and Time")]
        public DateTime? TimeStart { get; set; }

        [Required]
        [Display(Name = "End Date and Time")]
        public DateTime? TimeEnd { get; set; }

        [Required]
        [Display(Name = "Room")]
        public int RoomId { get; set; }

        public SelectList Rooms { get; set; }
    }
}
