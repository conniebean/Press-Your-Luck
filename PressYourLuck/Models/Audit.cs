using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PressYourLuck.Models
{
    public class Audit
    {
        public int AuditID { get; set; }
        public string PlayerName { get; set; }
        public DateTime CreatedDate { get; set; }
        public double Amount { get; set; }

        public AuditType AuditType { get; set; }
        
        public int AuditTypeID { get;set; }

    }
}
