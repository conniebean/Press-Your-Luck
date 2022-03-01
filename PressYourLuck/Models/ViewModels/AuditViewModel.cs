using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace PressYourLuck.Models.ViewModels
{
    public class AuditViewModel
    {
        public List<AuditType> AuditTypeList { get; set; }
        public List<Audit> AuditList { get; set; }

        public int ActiveAuditTypeID { get; set; }

        public AuditViewModel(AuditContext context,
            HttpContext httpContext)
        {
            IQueryable<Audit> query = context.Audits;

            var activeAuditTypeID = httpContext.Session.GetInt32("active-auditType-id") ?? 0;

            if (activeAuditTypeID != 0)
            {
                query = query.Where(a => a.AuditTypeID == activeAuditTypeID);
                AuditList = query.ToList();
            }
            else
            {
                AuditList = context.Audits
                    .Include(a => a.AuditType)
                    .OrderByDescending(a => a.CreatedDate)
                    .ToList();
            }

            var auditTypeListJson = httpContext.Session.GetString("audit-list");

            List<AuditType> auditTypes = new List<AuditType>();

            if (string.IsNullOrWhiteSpace(auditTypeListJson))
            {
                auditTypes = context.AuditTypes
                .OrderBy(a => a.Name)
                .ToList();
                auditTypes.Insert(0, new AuditType { AuditTypeID = 0, Name = "All" });

                auditTypeListJson = JsonConvert.SerializeObject(auditTypes);
                httpContext.Session.SetString("audit-list", auditTypeListJson);
            }
            else
            {
                auditTypes = JsonConvert.DeserializeObject<List<AuditType>>(auditTypeListJson);
            }

            

            AuditTypeList = auditTypes;

            ActiveAuditTypeID = activeAuditTypeID;

        }

        public string CheckActiveAuditTypeId(int currentId)
        {
            return (currentId == ActiveAuditTypeID) ? "active" : "";
        }
    }
}
