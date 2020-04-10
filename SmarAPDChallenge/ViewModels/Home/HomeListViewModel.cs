using SmarAPDChallenge.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarAPDChallenge.ViewModels.Home
{
    public class HomeListViewModel
    {
        public List<HomeListItemViewModel> items { get; set; }
    }

    public class HomeListItemViewModel
    {
        [Display(Name = "Code")]
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Schedule Start")]
        public DateTime TimeStart { get; set; }

        [Display(Name = "Schedule End")]
        public DateTime TimeEnd { get; set; }

        [Display(Name = "Room")]
        public RoomModel Room { get; set; }

        [Display(Name = "User")]
        public string UserName { get; set; }
    }
}