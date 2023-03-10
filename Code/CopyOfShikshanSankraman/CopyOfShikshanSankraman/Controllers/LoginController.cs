using CopyOfShikshanSankraman.Models;
using Dapper;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CopyOfShikshanSankraman.Controllers
{
    public class LoginController : Controller
    {
        NewDatabaseEntities1 NDBObj = new NewDatabaseEntities1();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            Session["UserName"] = UserName;
            Session["Password"] = Password;
            if(UserName== "Admin"  && Password== "Admin")
            {
                return RedirectToAction("Login_Dashboard", "Login");
            }
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(Registration_tbl modal)
        {
            NDBObj.Registration_tbl.Add(modal);
            NDBObj.SaveChanges();
            TempData["msg"]= "Successfully Registered!Please Login";
            return RedirectToAction("Login");
        }

        public ActionResult WriterList()
        {
            var res = NDBObj.NewWriter_tbl.ToList();

            return View(res);
        }
        public void ExportToExcelW()
        {
            var res = NDBObj.NewWriter_tbl.ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

           

            
            ws.Cells["H3"].Value = "लेखक यादी";

            ws.Cells["A6"].Value = "FullName";
            ws.Cells["B6"].Value = "EmailId";
            ws.Cells["C6"].Value = "MobileNo1";
            ws.Cells["D6"].Value = "MobileNo2";
            ws.Cells["E6"].Value = "AadharNo";
            ws.Cells["F6"].Value = "LekhachaVishay";
            ws.Cells["G6"].Value = "ShaikshanikPatrata";
            ws.Cells["H6"].Value = "KaryalayachaPrakar";
            ws.Cells["I6"].Value = "VyavasaikSthan";
            ws.Cells["J6"].Value = "LekhPrakashanStatus";
            ws.Cells["K6"].Value = "Anubhav";
            ws.Cells["L6"].Value = "KaryaratSthal";
            ws.Cells["M6"].Value = "PrakashitLekh";
            ws.Cells["N6"].Value = "PrakashitKashyamadhe";
            ws.Cells["O6"].Value = "Vishay";
            ws.Cells["P6"].Value = "AavadichaVishay";
            ws.Cells["Q6"].Value = "Payment";
           

            int rowStart = 8;
            foreach (var item in res)
            {
                //ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;


                ws.Cells[string.Format("A{0}", rowStart)].Value = item.FullName;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.EmailId;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.MobileNo1;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.MobileNo2;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.AadharNo;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.LekhachaVishay;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.ShaikshanikPatrata;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.KaryalayachaPrakar;
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.VyavasaikSthan;
                ws.Cells[string.Format("J{0}", rowStart)].Value = item.LekhPrakashanStatus;
                ws.Cells[string.Format("K{0}", rowStart)].Value = item.Anubhav;
                ws.Cells[string.Format("L{0}", rowStart)].Value = item.KaryaratSthal;
                ws.Cells[string.Format("M{0}", rowStart)].Value = item.PrakashitLekh;
                ws.Cells[string.Format("N{0}", rowStart)].Value = item.PrakashitKashyamadhe;
                ws.Cells[string.Format("O{0}", rowStart)].Value = item.Vishay;
                ws.Cells[string.Format("P{0}", rowStart)].Value = item.AavadichaVishay;
                ws.Cells[string.Format("Q{0}", rowStart)].Value = item.Payment;
                
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }

        public FileResult WDownloadFile(int id)
        {
            List<NewWriter_tbl> ObjFiles = WGetFileList();
            var FileById = (from FC in ObjFiles
                            where FC.Id.Equals(id)
                            select new { FC.SamtiPatra, FC.Lekh, }).ToList().FirstOrDefault();

            return File(FileById.SamtiPatra, FileById.Lekh);
        }
        public FileResult WDownloadFile1(int id)
        {
            List<NewWriter_tbl> ObjFiles = WGetFileList1();
            var FileById = (from FC in ObjFiles
                            where FC.Id.Equals(id)
                            select new { FC.Lekh, FC.SamtiPatra, }).ToList().FirstOrDefault();

            return File(FileById.Lekh, FileById.SamtiPatra);
        }
        public FileResult WDownloadFile2(int id)
        {
            List<NewWriter_tbl> ObjFiles = WGetFileList2();
            var FileById = (from FC in ObjFiles
                            where FC.Id.Equals(id)
                            select new { FC.Font, FC.Lekh }).ToList().FirstOrDefault();

            return File(FileById.Font, FileById.Lekh);
        }
        [HttpGet]
        public PartialViewResult WFileDetails()
        {
            List<NewWriter_tbl> DetList = WGetFileList();

            return PartialView("WFileDetails", DetList);


        }
        public PartialViewResult WFileDetails1()
        {
            List<NewWriter_tbl> DetList1 = WGetFileList1();

            return PartialView("WFileDetails1", DetList1);


        }
        public PartialViewResult WFileDetails2()
        {
            List<NewWriter_tbl> DetList2 = WGetFileList2();

            return PartialView("WFileDetails2", DetList2);
        }

        private List<NewWriter_tbl> WGetFileList()
        {
            List<NewWriter_tbl> DetList = new List<NewWriter_tbl>();

            DbConnection();
            con.Open();
            DetList = SqlMapper.Query<NewWriter_tbl>(con, "GetFilesOfWriter1", commandType: CommandType.StoredProcedure).ToList();

           
            con.Close();
            return DetList;
        }
        private List<NewWriter_tbl> WGetFileList1()
        {
            List<NewWriter_tbl> DetList1 = new List<NewWriter_tbl>();

            DbConnection();
            con.Open();
            DetList1 = SqlMapper.Query<NewWriter_tbl>(con, "GetFilesOfWriter2", commandType: CommandType.StoredProcedure).ToList();
            con.Close();
            return DetList1;
        }
        private List<NewWriter_tbl> WGetFileList2()
        {
            List<NewWriter_tbl> DetList2 = new List<NewWriter_tbl>();

            DbConnection();
            con.Open();
            DetList2 = SqlMapper.Query<NewWriter_tbl>(con, "GetFilesOfWriter3", commandType: CommandType.StoredProcedure).ToList();
            con.Close();
            return DetList2;
        }



        /*..........Chitrakar List..........*/
        public ActionResult ChitrakarList()
        {
            var res = NDBObj.NewChitrakar_tbl.ToList();

            return View(res);
        }

        public void ExportToExcelC()
        {
            var res = NDBObj.NewChitrakar_tbl.ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");




            ws.Cells["H3"].Value = "चित्रकार यादी";

            ws.Cells["A6"].Value = "FullName";
            ws.Cells["B6"].Value = "EmailId";
            ws.Cells["C6"].Value = "MobileNo1";
            ws.Cells["D6"].Value = "MobileNo2";
            ws.Cells["E6"].Value = "AadharNo";
            ws.Cells["F6"].Value = "ChitrachaVishay";
            ws.Cells["G6"].Value = "ShaikshanikPatrata";
            ws.Cells["H6"].Value = "KaryalayachaPrakar";
            ws.Cells["I6"].Value = "VyavasayikSthan";
            ws.Cells["J6"].Value = "ChitraPrakashanStatus";
            ws.Cells["K6"].Value = "Anubhav";
            ws.Cells["L6"].Value = "KaryaratSthal";
            ws.Cells["M6"].Value = "PrakashitChitra";
            ws.Cells["N6"].Value = "PrakashitKashyamadhe";
            ws.Cells["O6"].Value = "Vishay";
            ws.Cells["P6"].Value = "AavadichaVishay";
            ws.Cells["Q6"].Value = "Payment";


            int rowStart = 8;
            foreach (var item in res)
            {
                //ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;


                ws.Cells[string.Format("A{0}", rowStart)].Value = item.FullName;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.EmailId;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.MobileNo1;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.MobileNo2;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.AadharNo;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.ChitrachaVishay;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.ShaikshanikPatrata;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.KaryalayachaPrakar;
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.VyavasayikSthan;
                ws.Cells[string.Format("J{0}", rowStart)].Value = item.ChitraPrakashanStatus;
                ws.Cells[string.Format("K{0}", rowStart)].Value = item.Anubhav;
                ws.Cells[string.Format("L{0}", rowStart)].Value = item.KaryaratSthal;
                ws.Cells[string.Format("M{0}", rowStart)].Value = item.PrakashitChitra;
                ws.Cells[string.Format("N{0}", rowStart)].Value = item.PrakashitKashyamadhe;
                ws.Cells[string.Format("O{0}", rowStart)].Value = item.Vishay;
                ws.Cells[string.Format("P{0}", rowStart)].Value = item.AavadichaVishay;
                ws.Cells[string.Format("Q{0}", rowStart)].Value = item.Payment;

                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }

        public FileResult DownloadFile(int id)
        {
            List<NewChitrakar_tbl> ObjFiles = GetFileList();
            var FileById = (from FC in ObjFiles
                            where FC.Id.Equals(id)
                            select new { FC.Chitra, FC.SamtiPatra, }).ToList().FirstOrDefault();

            return File(FileById.Chitra, FileById.SamtiPatra);
        }
        public FileResult DownloadFile1(int id)
        {
            List<NewChitrakar_tbl> ObjFiles = GetFileList1();
            var FileById = (from FC in ObjFiles
                            where FC.Id.Equals(id)
                            select new { FC.SamtiPatra, FC.Chitra, }).ToList().FirstOrDefault();

            return File(FileById.SamtiPatra, FileById.Chitra);
        }
        public FileResult DownloadFile2(int id)
        {
            List<NewChitrakar_tbl> ObjFiles = GetFileList2();
            var FileById = (from FC in ObjFiles
                            where FC.Id.Equals(id)
                            select new { FC.Chitra2, FC.Chitra }).ToList().FirstOrDefault();

            return File(FileById.Chitra2, FileById.Chitra);
        }
        [HttpGet]
        public PartialViewResult FileDetails()
        {
            List<NewChitrakar_tbl> DetList = GetFileList();

            return PartialView("FileDetails", DetList);


        }
        public PartialViewResult FileDetails1()
        {
            List<NewChitrakar_tbl> DetList1 = GetFileList1();

            return PartialView("FileDetails1", DetList1);


        }
        public PartialViewResult FileDetails2()
        {
            List<NewChitrakar_tbl> DetList2 = GetFileList2();

            return PartialView("FileDetails2", DetList2);


        }

        private List<NewChitrakar_tbl> GetFileList()
        {
            List<NewChitrakar_tbl> DetList = new List<NewChitrakar_tbl>();

            DbConnection();
            con.Open();
            DetList = SqlMapper.Query<NewChitrakar_tbl>(con, "GetFileDetailsNew1", commandType: CommandType.StoredProcedure).ToList();
            con.Close();
            return DetList;
        }
        private List<NewChitrakar_tbl> GetFileList1()
        {
            List<NewChitrakar_tbl> DetList1 = new List<NewChitrakar_tbl>();

            DbConnection();
            con.Open();
            DetList1 = SqlMapper.Query<NewChitrakar_tbl>(con, "GetFileDetailsNew2", commandType: CommandType.StoredProcedure).ToList();
            con.Close();
            return DetList1;
        }
        private List<NewChitrakar_tbl> GetFileList2()
        {
            List<NewChitrakar_tbl> DetList2 = new List<NewChitrakar_tbl>();

            DbConnection();
            con.Open();
            DetList2 = SqlMapper.Query<NewChitrakar_tbl>(con, "GetFileDetailsNew3", commandType: CommandType.StoredProcedure).ToList();
            con.Close();
            return DetList2;
        }


        public ActionResult MadhyamikShalaList()
        {
            var res = NDBObj.School_tbl.ToList();

            return View(res);
        }
        public void ExportToExcelM()
        {
            var res = NDBObj.School_tbl.ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["H3"].Value = "लेखक यादी";

            ws.Cells["A6"].Value = "Vargadar";
            ws.Cells["B6"].Value = "SansthecheNav";
            ws.Cells["C6"].Value = "SchoolName";
            ws.Cells["D6"].Value = "SansthechaPatta";
            ws.Cells["D6"].Value = "SchoolAdd";
            ws.Cells["E6"].Value = "CityName";
            ws.Cells["F6"].Value = "IndexNo";
            ws.Cells["G6"].Value = "Taluka";
            ws.Cells["H6"].Value = "Jhila";
            ws.Cells["I6"].Value = "PincodeNo";
            ws.Cells["J6"].Value = "Email";
            ws.Cells["K6"].Value = "MobileNo1";
            ws.Cells["L6"].Value = "MobileNo2";
            ws.Cells["M6"].Value = "Vargani";
            ws.Cells["N6"].Value = "VarganiDate";
            ws.Cells["O6"].Value = "MonthStart";
            ws.Cells["P6"].Value = "MonthEnd";

            int rowStart = 8;
            foreach (var item in res)
            {
                //ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;


                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Vargadar;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.SansthecheNav;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.SchoolName;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.SansthechaPatta;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.SchoolAdd;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.CityName;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.IndexNo;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.Taluka;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.Jhila;
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.PincodeNo;
                ws.Cells[string.Format("J{0}", rowStart)].Value = item.Email;
                ws.Cells[string.Format("K{0}", rowStart)].Value = item.MobileNo1;
                ws.Cells[string.Format("L{0}", rowStart)].Value = item.MobileNo2;
                ws.Cells[string.Format("M{0}", rowStart)].Value = item.Vargani;
                ws.Cells[string.Format("N{0}", rowStart)].Value = item.VarganiDate;
                ws.Cells[string.Format("O{0}", rowStart)].Value = item.MonthStart;
                ws.Cells[string.Format("P{0}", rowStart)].Value = item.MonthEnd;
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }
        public ActionResult MahavidyalayaList()
        {
            var res = NDBObj.Mahavidyalaya_tbl.ToList();

            return View(res);
        }

        public void ExportToExcel()
        {
            var res = NDBObj.Mahavidyalaya_tbl.ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["H3"].Value = "लेखक यादी";

            ws.Cells["A6"].Value = "Vargadar";
            ws.Cells["B6"].Value = "SansthecheNav";
            ws.Cells["C6"].Value = "MahavidyalayacheNav";
            ws.Cells["D6"].Value = "SansthechaPatta";
            ws.Cells["E6"].Value = "CityName";
            ws.Cells["F6"].Value = "IndexNo";
            ws.Cells["G6"].Value = "Taluka";
            ws.Cells["H6"].Value = "Jhila";
            ws.Cells["I6"].Value = "PincodeNo";
            ws.Cells["J6"].Value = "Email";
            ws.Cells["K6"].Value = "MobileNo1";
            ws.Cells["L6"].Value = "MobileNo2";
            ws.Cells["M6"].Value = "VarganiDate";
            ws.Cells["N6"].Value = "Vargani";
            ws.Cells["O6"].Value = "MonthStart";
            ws.Cells["P6"].Value = "MonthEnd";

            int rowStart = 8;
            foreach (var item in res)
            {
                //ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;


                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Vargadar;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.SansthecheNav;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.MahavidyalayacheNav;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.SansthechaPatta;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.CityName;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.IndexNo;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.Taluka;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.Jhila;
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.PincodeNo;
                ws.Cells[string.Format("J{0}", rowStart)].Value = item.Email;
                ws.Cells[string.Format("K{0}", rowStart)].Value = item.MobileNo1;
                ws.Cells[string.Format("L{0}", rowStart)].Value = item.MobileNo2;
                ws.Cells[string.Format("M{0}", rowStart)].Value = item.VarganiDate;
                ws.Cells[string.Format("N{0}", rowStart)].Value = item.Vargani;
                ws.Cells[string.Format("O{0}", rowStart)].Value = item.MonthStart;
                ws.Cells[string.Format("P{0}", rowStart)].Value = item.MonthEnd;
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }
        public ActionResult KarayalayList()
        {
            var res = NDBObj.NewKaryalay_tbl.ToList();

            return View(res);
        }

        public void ExportToExcelK()
        {
            var res = NDBObj.NewKaryalay_tbl.ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["H3"].Value = "लेखक यादी";

            ws.Cells["A6"].Value = "Vargadar";
            ws.Cells["B6"].Value = "SurnameOfHead";
            ws.Cells["C6"].Value = "Nav";
            ws.Cells["D6"].Value = "VadilancheNav";
            ws.Cells["E6"].Value = "KaryalayacheNav";
            ws.Cells["F6"].Value = "GharNav";
            ws.Cells["G6"].Value = "RoadName";
            ws.Cells["H6"].Value = "NearMark";
            ws.Cells["I6"].Value = "CityName";
            ws.Cells["J6"].Value = "Taluka";
            ws.Cells["K6"].Value = "Jhila";
            ws.Cells["L6"].Value = "PincodeNo";
            ws.Cells["M6"].Value = "Email";
            ws.Cells["N6"].Value = "MobileNo1";
            ws.Cells["O6"].Value = "MobileNo2";
            ws.Cells["P6"].Value = "VarganiDate";
            ws.Cells["P6"].Value = "Vargani";
            ws.Cells["P6"].Value = "MonthStart";
            ws.Cells["P6"].Value = "MonthEnd";

            int rowStart = 8;
            foreach (var item in res)
            {
                //ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;


                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Vargadar;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.SurnameOfHead;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Nav;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.VadilancheNav;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.KaryalayacheNav;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.GharNav;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.RoadName;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.NearMark;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.CityName;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.Taluka;
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.Jhila;
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.PincodeNo;
                ws.Cells[string.Format("J{0}", rowStart)].Value = item.Email;
                ws.Cells[string.Format("K{0}", rowStart)].Value = item.MobileNo1;
                ws.Cells[string.Format("L{0}", rowStart)].Value = item.MobileNo2;
                ws.Cells[string.Format("M{0}", rowStart)].Value = item.VarganiDate;
                ws.Cells[string.Format("N{0}", rowStart)].Value = item.Vargani;
                ws.Cells[string.Format("O{0}", rowStart)].Value = item.MonthStart;
                ws.Cells[string.Format("P{0}", rowStart)].Value = item.MonthEnd;
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }
        public ActionResult VaraganidarList()
        {
            var res = NDBObj.NewVarganidar_tbl.ToList();

            return View(res);
        }
        public void ExportToExcelV()
        {
            var res = NDBObj.NewVarganidar_tbl.ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");




          

            ws.Cells["A6"].Value = "Surname";
            ws.Cells["B6"].Value = "Name";
            ws.Cells["C6"].Value = "FatherName";
            ws.Cells["D6"].Value = "GharNav";
            ws.Cells["E6"].Value = "RoadName";
            ws.Cells["F6"].Value = "NearMark";
            ws.Cells["G6"].Value = "CityName";
            ws.Cells["H6"].Value = "Taluka";
            ws.Cells["I6"].Value = "Jhila";
            ws.Cells["J6"].Value = "PincodeNo";
            ws.Cells["K6"].Value = "Email";
            ws.Cells["L6"].Value = "MobileNo1";
            ws.Cells["M6"].Value = "MobileNo2";
            ws.Cells["N6"].Value = "VarganiDate";
            ws.Cells["O6"].Value = "AadharCardNo";
            ws.Cells["P6"].Value = "MonthStart";
            ws.Cells["Q6"].Value = "MonthEnd";
            ws.Cells["R6"].Value = "Varagani";

            int rowStart = 8;
            foreach (var item in res)
            {
                //ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;


                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Surname;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Name;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.FatherName;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.GharNav;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.RoadName;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.NearMark;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.CityName;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.Taluka;
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.Jhila;
                ws.Cells[string.Format("J{0}", rowStart)].Value = item.PincodeNo;
                ws.Cells[string.Format("K{0}", rowStart)].Value = item.Email;
                ws.Cells[string.Format("L{0}", rowStart)].Value = item.MobileNo1;
                ws.Cells[string.Format("M{0}", rowStart)].Value = item.MobileNo2;
                ws.Cells[string.Format("N{0}", rowStart)].Value = item.VarganiDate;
                ws.Cells[string.Format("O{0}", rowStart)].Value = item.AadharCardNo;
                ws.Cells[string.Format("P{0}", rowStart)].Value = item.MonthStart;
                ws.Cells[string.Format("Q{0}", rowStart)].Value = item.MonthEnd;
                ws.Cells[string.Format("R{0}", rowStart)].Value = item.Varagani;
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }


        public FileResult Downloadpic(string Imagename)
        {
            var FileVirtualPath = "~/DownloadedFiles/Photo/" + Imagename;
            return File(FileVirtualPath, "application/force- download", Path.GetFileName(FileVirtualPath));
        }
        public FileResult DownloadPhoto(int id)
        {
            List<NewVarganidar_tbl> ObjFiles = GetPhotoList();
            var FileById = (from FC in ObjFiles
                            where FC.Id.Equals(id)
                            select new { FC.Photo,FC.Varagani  }).ToList().FirstOrDefault();

            return File(FileById.Photo, "image/png", FileById.Varagani  );
        }
        
        [HttpGet]
        public PartialViewResult PhotoDetails()
        {
            List<NewVarganidar_tbl> DetList = GetPhotoList();

            return PartialView("PhotoDetails", DetList);


        }
      
      
        private List<NewVarganidar_tbl> GetPhotoList()
        {
            List<NewVarganidar_tbl> DetList = new List<NewVarganidar_tbl>();

            DbConnection();
            con.Open();
            DetList = SqlMapper.Query<NewVarganidar_tbl>(con, "GetPhotoDetails", commandType: CommandType.StoredProcedure).ToList();
            con.Close();
            return DetList;
        }


        private SqlConnection con;
        private string constr;


        private void DbConnection()
        {
            constr = ConfigurationManager.ConnectionStrings["constr1"].ToString();
            con = new SqlConnection(constr);

        }

        public ActionResult Login_Dashboard()
        {
            return View();
        }

        

    }
}