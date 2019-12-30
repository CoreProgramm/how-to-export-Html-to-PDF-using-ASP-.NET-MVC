using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Export_HTML_PDF.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace Export_HTML_PDF.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee() { Name="Thomas Hardy", Email="thomashardy@mail.com", Address="89 Chiaroscuro Rd, Portland, USA", Phone="(171) 555-2222" });
            employees.Add(new Employee() { Name="Dominique Perrier", Email = "dominiqueperrier@mail.com", Address = "Obere Str. 57, Berlin, Germany", Phone="(313) 555-5735" });
            employees.Add(new Employee() { Name="Maria Anders", Email = "mariaanders@mail.com", Address = "25, rue Lauriston, Paris, France", Phone = "(503) 555-9931" });
            employees.Add(new Employee() { Name="Fran Wilson", Email = "franwilson@mail.com", Address = "C/ Araquil, 67, Madrid, Spain", Phone = "(204) 619-5731" });
            employees.Add(new Employee() { Name = "Martin Blank", Email = "martinblank@mail.com", Address = "Via Monte Bianco 34, Turin, Italy", Phone = "(480) 631-2097" });
            return View(employees);
        }
        [HttpPost]
        [ValidateInput(false)]
        public FileResult ExportHTML(string ExportData)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader reader = new StringReader(ExportData);
                Document PdfFile = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(PdfFile, stream);
                PdfFile.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, PdfFile, reader);
                PdfFile.Close();
                return File(stream.ToArray(), "application/pdf", "ExportData.pdf");
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}