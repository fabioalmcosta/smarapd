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
        [Display(Name = "Titulo")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Data e Hora Inicial")]
        public DateTime? TimeStart { get; set; }

        [Required]
        [Display(Name = "Data e Hora Final")]
        public DateTime? TimeEnd { get; set; }

        [Required]
        [Display(Name = "Sala")]
        public int RoomId { get; set; }

        public SelectList Rooms { get; set; }
    }
}
