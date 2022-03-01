using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PressYourLuck.Models
{
    public class Player
    {
        private double _coins;
        [Required(ErrorMessage ="You must enter a name to continue.")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Your coins must be higher than 1 and 10,000 or less.")]
        [Range(1.00, 10000.00, ErrorMessage = "Your coins must be higher than 1 and 10,000 or less.")]
        public double Coins { get => _coins; set => _coins = value; }
    }
}
