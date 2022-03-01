using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PressYourLuck.Models
{
    public class AuditType
    {
        [Required]
        public int AuditTypeID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
