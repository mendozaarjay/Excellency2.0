using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Excellency.Interfaces;
using Excellency.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Excellency.Controllers
{
    [SessionAuthorized]
    public class EvaluationInformationController : Controller
    {
        private IEvaluationInformation _Services;

        public EvaluationInformationController(IEvaluationInformation service)
        {
            _Services = service;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            List<EvaluationBehavioralItem> behavioralItems = new List<EvaluationBehavioralItem>();
            List<EvaluationKRAItem> kraItems = new List<EvaluationKRAItem>();

            var bitems = _Services.GetAllBehavioralPerEmployee(userId);
            var kitems = _Services.GetAllKRAPerEmployee(userId);
            if(bitems != null)
            {
                foreach(DataRow dr in bitems.Rows)
                {
                    var item = new EvaluationBehavioralItem();
                    var headeritem = new EIEvaluationItem
                    {
                        RecordId = int.Parse(dr["RecordId"].ToString()),
                        Id = int.Parse(dr["Id"].ToString()),
                        EvaluatedBy = dr["EvaluatedBy"].ToString(),
                        EvaluationDate = dr["EvaluationDate"].ToString(),
                        Title = dr["Title"].ToString(),
                        Description = dr["Description"].ToString(),
                        Weight = dr["Weight"].ToString(),
                        Status = dr["Status"].ToString(),
                    };
                    item.Header = headeritem;
                    var litems = _Services.GetAllBehavioralRecordPerId(headeritem.RecordId);
                    List<EIEvaluationLineItem> lineItems = new List<EIEvaluationLineItem>();
                    if(litems != null)
                    {
                        foreach(DataRow row in litems.Rows)
                        {
                            var lineitem = new EIEvaluationLineItem
                            {
                                Id = int.Parse(row["Id"].ToString()),
                                Description = row["Description"].ToString(),
                                Weight = row["Weight"].ToString(),
                                Score = row["Score"].ToString(),
                            };
                            lineItems.Add(lineitem);
                        }
                    }
                    item.LineItems = lineItems;
                    behavioralItems.Add(item);
                }
            }

            if (kitems != null)
            {
                foreach (DataRow dr in kitems.Rows)
                {
                    var item = new EvaluationKRAItem();
                    var headeritem = new EIEvaluationItem
                    {
                        RecordId = int.Parse(dr["RecordId"].ToString()),
                        Id = int.Parse(dr["Id"].ToString()),
                        EvaluatedBy = dr["EvaluatedBy"].ToString(),
                        EvaluationDate = dr["EvaluationDate"].ToString(),
                        Title = dr["Title"].ToString(),
                        Description = dr["Description"].ToString(),
                        Weight = dr["Weight"].ToString(),
                        Status = dr["Status"].ToString(),
                    };
                    item.Header = headeritem;
                    var litems = _Services.GetAllKRARecordPerId(headeritem.RecordId);
                    List<EIEvaluationLineItem> lineItems = new List<EIEvaluationLineItem>();
                    if (litems != null)
                    {
                        foreach (DataRow row in litems.Rows)
                        {
                            var lineitem = new EIEvaluationLineItem
                            {
                                Id = int.Parse(row["Id"].ToString()),
                                Description = row["Description"].ToString(),
                                Weight = row["Weight"].ToString(),
                                Score = row["Score"].ToString(),
                            };
                            lineItems.Add(lineitem);
                        }
                    }
                    item.LineItems = lineItems;
                    kraItems.Add(item);
                }
            }

            var alevel = _Services.GetApprovalLevel(userId);
            var approval = new EIApprovalLevel();
            if(alevel != null)
            {
                if(alevel.Rows.Count > 0)
                {
                    approval.FirstApproval = alevel.Rows[0]["FirstApproval"].ToString();
                    approval.SecondApproval = alevel.Rows[0]["SecondApproval"].ToString();
                    approval.IsWithSecondApproval = alevel.Rows[0]["IsWithSecondApproval"].ToString().Equals("1") ? true : false;
                }
            }

            var model = new EvaluationInfoIndexViewModel
            {
                BehavioralItems = behavioralItems,
                KRAItems = kraItems,
                ApprovalLevel = approval,
                Name = _Services.Name(userId)
            };
            return View(model);
        }
    }
}
