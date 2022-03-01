using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PressYourLuck.Helpers;

namespace PressYourLuck.Models.ViewModels
{
    public class GameViewModel
    {

        [Required(ErrorMessage = "Your bet must be higher than 1 and less than your total coins.")]
        
        public double CurrentBet { get; set; }

       
    }
}
