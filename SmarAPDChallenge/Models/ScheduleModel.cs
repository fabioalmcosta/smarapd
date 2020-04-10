using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarAPDChallenge.Models
{
    public class ScheduleModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public int RoomId { get; set; }
        public RoomModel Room { get; set; }
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }

    }
}
