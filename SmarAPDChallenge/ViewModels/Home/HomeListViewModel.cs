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
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Titulo")]
        public string Title { get; set; }

        [Display(Name = "Início Agendamento")]
        public DateTime TimeStart { get; set; }

        [Display(Name = "Final do Agendamento")]
        public DateTime TimeEnd { get; set; }

        [Display(Name = "Sala")]
        public RoomModel Room { get; set; }

        [Display(Name = "Usuário")]
        public string UserName { get; set; }
    }
}