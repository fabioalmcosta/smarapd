using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarAPDChallenge.ViewModels.Rooms
{
    public class RoomListViewModel
    {
        public List<RoomListItemViewModel> items { get; set; }
    }

    public class RoomListItemViewModel
    {
        [Display(Name = "Code")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
