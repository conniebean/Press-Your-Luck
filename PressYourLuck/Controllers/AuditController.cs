using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PressYourLuck.Models;
using PressYourLuck.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PressYourLuck.Controllers
{
    public class AuditController : Controller
    {
        private AuditContext _auditContext;
        public AuditController(AuditContext auditContext)
        {
            _auditContext = auditContext;
        }

        public IActionResult Index(int activeAuditTypeID = 0)
        {
            var auditList = _auditContext.Audits
                    .Include(a => a.AuditType)
                    .OrderByDescending(a => a.CreatedDate)
                    .ToList();
            HttpContext.Session.SetInt32("active-auditType-id", activeAuditTypeID);

            var auditViewModel = new AuditViewModel(_auditContext, HttpContext);

            return View(auditViewModel);
        }
    }
}
