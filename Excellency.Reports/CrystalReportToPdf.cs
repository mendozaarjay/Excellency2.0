﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excellency.Reports
{
    public class CrystalReportToPdf 
    {
        public CrystalReportToPdf()
        {

        }
        #region Samples

        //public CrystalReportPdfResult(string reportPath, object dataSet)
        //{
        //    ReportDocument reportDocument = new ReportDocument();
        //    reportDocument.Load(reportPath);
        //    reportDocument.SetDataSource(dataSet);
        //    _contentBytes = StreamToBytes(reportDocument.ExportToStream(ExportFormatType.PortableDocFormat));
        //}

        //public override void ExecuteResult(ControllerContext context)
        //{

        //    var response = context.HttpContext.ApplicationInstance.Response;
        //    response.Clear();
        //    response.Buffer = false;
        //    response.ClearContent();
        //    response.ClearHeaders();
        //    response.Cache.SetCacheability(HttpCacheability.Public);
        //    response.ContentType = "application/pdf";

        //    using (var stream = new MemoryStream(_contentBytes))
        //    {
        //        stream.WriteTo(response.OutputStream);
        //        stream.Flush();
        //    }
        //}

        //private static byte[] StreamToBytes(Stream input)
        //{
        //    byte[] buffer = new byte[16 * 1024];
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        int read;
        //        while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
        //        {
        //            ms.Write(buffer, 0, read);
        //        }
        //        return ms.ToArray();
        //    }
        //}
        //var items = SCObjects.LoadDataTable(string.Format("EXEC [ISPA].[spPOSlip_Report] @iBranchID = {0},@tReferenceNo = '{1}'", UnitId, ReferenceNo), this.UserConnectionString);
        //var ReportParth = Server.MapPath("~/Reports/PurchaseOrderSlip.rpt");
        //    return new CrystalReportPdfResult(ReportParth, items);
        #endregion
    }
}
