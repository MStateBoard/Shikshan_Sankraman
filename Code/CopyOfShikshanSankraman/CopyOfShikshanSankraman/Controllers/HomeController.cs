using CopyOfShikshanSankraman.Models;
using Dapper;
using System;

using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CopyOfShikshanSankraman.Controllers
{
    public class HomeController : Controller


    {
       
        NewDatabaseEntities1 NDBObj = new NewDatabaseEntities1();


        public ActionResult HomePage()
        {
           
            ANK_Model model = new ANK_Model();
            model.Year_2019 = NDBObj.Admin_tbl1.Where(a => a.Year == "2019").ToList();
            model.Year_2020 = NDBObj.Admin_tbl1.Where(a => a.Year == "2020").ToList();
            model.Year_2021 = NDBObj.Admin_tbl1.Where(a => a.Year == "2021").ToList();
            model.Year_2022 = NDBObj.Admin_tbl1.Where(a => a.Year == "2022").ToList();
            return View(model);
        }




        //public JsonResult Get_Year_Data()
        //{
        //    try
        //    {
        //        List<Admin_tbl> tbl_2021 = NDBObj.Admin_tbl.Where(a => a.Year == "2021").ToList();
        //        List<Admin_tbl> tbl_2020 = NDBObj.Admin_tbl.Where(a => a.Year == "2020").ToList();
        //        List<Admin_tbl> tbl_2019 = NDBObj.Admin_tbl.Where(a => a.Year == "2019").ToList();

        //        return Json(new { Result = true, TBL_2021 = tbl_2021, TBL_2020 = tbl_2020, TBL_2019 = tbl_2019 }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exe)
        //    {
        //        return Json(new { Result = false, Message = "Unable to Fetch Record" }, JsonRequestBehavior.AllowGet);
        //    }
        //}


        public ActionResult AddDataByYearList()
        {
            var res = NDBObj.Admin_tbl1.Where(a => a.Year == "2020").ToList();

            return View(res);
        }

       

        public ActionResult Writer()
        {
            return View();
        }
        public ActionResult WriterInside()
        {
            NewWriter_tbl p = new NewWriter_tbl();
            return View(p);
        }
        [HttpPost]
        public ActionResult WriterInside(NewWriter_tbl wmodel)
        {
            NewWriter_tbl p = new NewWriter_tbl();

         

                //if (SamtiPatra != null && SamtiPatra.ContentLength > 0)
                //{


                //    string fileName = Path.GetFileName(SamtiPatra.FileName);
                //    string folder = Server.MapPath("~/DownloadedFiles/Lekh/");
                //    Directory.CreateDirectory(folder);
                //    string filepath = Path.Combine(folder, fileName);
                //    SamtiPatra.SaveAs(filepath);
                //    wmodel.SamtiPatra = "~/DownloadedFiles/Lekh/" + fileName;
                //    SamtiPatra.SaveAs(Path.Combine(folder, fileName));
                //    //model.PdfUrl = filepath;
                //    try
                //    {
                //        Response.Write("Uploaded: " + fileName);
                //    }
                //    catch
                //    {

                //    }
                //}

                //if (Lekh != null && Lekh.ContentLength > 0)
                //{

                //    string fileName = Path.GetFileName(Lekh.FileName);
                //    string folder = Server.MapPath("~/DownloadedFiles/Lekh/");
                //    Directory.CreateDirectory(folder);
                //    string filepath = Path.Combine(folder, fileName);
                //    Lekh.SaveAs(filepath);
                //    wmodel.Lekh = "~/DownloadedFiles/Lekh/" + fileName;
                //    Lekh.SaveAs(Path.Combine(folder, fileName));
                //    //model.PdfUrl = filepath;
                //    try
                //    {
                //        Response.Write("Uploaded: " + fileName);
                //    }
                //    catch
                //    {

                //    }
                //}

                //if (Font != null && Font.ContentLength > 0)
                //{

                //    string fileName = Path.GetFileName(Font.FileName);
                //    string folder = Server.MapPath("~/DownloadedFiles/Lekh/");
                //    Directory.CreateDirectory(folder);
                //    string filepath = Path.Combine(folder, fileName);
                //    Font.SaveAs(filepath);
                //    wmodel.Font = "~/DownloadedFiles/Lekh/" + fileName;
                //    Font.SaveAs(Path.Combine(folder, fileName));
                //    //model.PdfUrl = filepath;
                //    try
                //    {
                //        Response.Write("Uploaded: " + fileName);
                //    }
                //    catch
                //    {

                //    }

                //}




          
            try
            {
                if (ModelState.IsValid)
                {
                    NDBObj.NewWriter_tbl.Add(wmodel);
                    NDBObj.SaveChanges();
                    var data = NDBObj.NewWriter_tbl.Where(x => x.FullName == wmodel.FullName).FirstOrDefault();
                    string file = Path.GetExtension(wmodel.SamtiPatra1.FileName);
                    string file2 = Path.GetExtension(wmodel.Lekh1.FileName);
                    string file3 = Path.GetExtension(wmodel.Font1.FileName);

                    string Filename = data.Id + ".SamtiPatra" + file;
                    string Filename2 =  data.Id + ".Lekh " + file2;
                    string Filename3 = data.Id + ".Font " + file3;
                    wmodel.SamtiPatra = Filename;
                    wmodel.Lekh = Filename2;
                    wmodel.Font = Filename3;

                    wmodel.SamtiPatra1.SaveAs(Path.Combine(Server.MapPath("~/DownloadedFiles/Lekh"), Filename));
                    wmodel.Lekh1.SaveAs(Path.Combine(Server.MapPath("~/DownloadedFiles/Lekh"), Filename2));
                    wmodel.Font1.SaveAs(Path.Combine(Server.MapPath("~/DownloadedFiles/Lekh"), Filename3));

                    

                    NDBObj.NewWriter_tbl.Attach(wmodel);
                    NDBObj.Entry(wmodel).Property(x => x.SamtiPatra).IsModified = true;
                    NDBObj.Entry(wmodel).Property(x => x.Lekh).IsModified = true;
                    NDBObj.Entry(wmodel).Property(x => x.Font).IsModified = true;
                    NDBObj.SaveChanges();
                    TempData["Msg"] = "save successfully...!";

                    ModelState.Clear();
                    return RedirectToAction("WriterInside");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Not save successfully...!"+e;

                return RedirectToAction("WriterInside");
            }
        }

        //public ActionResult WriterList()
        //{
        //    var res = NDBObj.NewWriter_tbl.ToList();

        //    return View(res);
        //}


        //public FileResult WDownloadFile(int id)
        //{
        //    List<NewWriter_tbl> ObjFiles = WGetFileList();
        //    var FileById = (from FC in ObjFiles
        //                    where FC.Id.Equals(id)
        //                    select new { FC.SamtiPatra, FC.Lekh, }).ToList().FirstOrDefault();

        //    return File( FileById.SamtiPatra, FileById.Lekh);
        //}
        //public FileResult WDownloadFile1(int id)
        //{
        //    List<NewWriter_tbl> ObjFiles = WGetFileList1();
        //    var FileById = (from FC in ObjFiles
        //                    where FC.Id.Equals(id)
        //                    select new { FC.Lekh, FC.SamtiPatra, }).ToList().FirstOrDefault();

        //    return File(FileById.Lekh, FileById.SamtiPatra);
        //}
        //public FileResult WDownloadFile2(int id)
        //{
        //    List<NewWriter_tbl> ObjFiles = WGetFileList2();
        //    var FileById = (from FC in ObjFiles
        //                    where FC.Id.Equals(id)
        //                    select new { FC.Font, FC.Lekh }).ToList().FirstOrDefault();

        //    return File(FileById.Font, FileById.Lekh);
        //}
        //[HttpGet]
        //public PartialViewResult WFileDetails()
        //{
        //    List<NewWriter_tbl> DetList = WGetFileList();

        //    return PartialView("WFileDetails", DetList);


        //}
        //public PartialViewResult WFileDetails1()
        //{
        //    List<NewWriter_tbl> DetList1 = WGetFileList1();

        //    return PartialView("WFileDetails1", DetList1);


        //}
        //public PartialViewResult WFileDetails2()
        //{
        //    List<NewWriter_tbl> DetList2 = WGetFileList2();

        //    return PartialView("WFileDetails2", DetList2);


        //}

        //private List<NewWriter_tbl> WGetFileList()
        //{
        //    List<NewWriter_tbl> DetList = new List<NewWriter_tbl>();

        //    DbConnection();
        //    con.Open();
        //    DetList = SqlMapper.Query<NewWriter_tbl>(con, "GetFilesOfWriter1", commandType: CommandType.StoredProcedure).ToList();
        //    con.Close();
        //    return DetList;
        //}
        //private List<NewWriter_tbl> WGetFileList1()
        //{
        //    List<NewWriter_tbl> DetList1 = new List<NewWriter_tbl>();

        //    DbConnection();
        //    con.Open();
        //    DetList1 = SqlMapper.Query<NewWriter_tbl>(con, "GetFilesOfWriter2", commandType: CommandType.StoredProcedure).ToList();
        //    con.Close();
        //    return DetList1;
        //}
        //private List<NewWriter_tbl> WGetFileList2()
        //{
        //    List<NewWriter_tbl> DetList2 = new List<NewWriter_tbl>();

        //    DbConnection();
        //    con.Open();
        //    DetList2 = SqlMapper.Query<NewWriter_tbl>(con, "GetFilesOfWriter3", commandType: CommandType.StoredProcedure).ToList();
        //    con.Close();
        //    return DetList2;
        //}
        public ActionResult Chitrakar()
        {
            return View();
        }
        public ActionResult ChitrakarInside()
        {
            NewChitrakar_tbl chitra = new NewChitrakar_tbl();

            return View(chitra);
        }

        [HttpPost]
        public ActionResult ChitrakarInside(NewChitrakar_tbl ncmodel, HttpPostedFileBase SamtiPatra, HttpPostedFileBase Chitra, HttpPostedFileBase Chitra2)
        {
            NewChitrakar_tbl p = new NewChitrakar_tbl();

         
                //if (SamtiPatra != null && SamtiPatra.ContentLength > 0)
                //{

                //    string fileName = Path.GetFileName(SamtiPatra.FileName);
                //    string folder = Server.MapPath("~/DownloadedFiles/Chitra");
                //    Directory.CreateDirectory(folder);
                //    string filepath = Path.Combine(folder, fileName);
                //    SamtiPatra.SaveAs(filepath);
                //    ncmodel.SamtiPatra = "http://shikshansankraman.msbshse.ac.in/DownloadedFiles/Chitra/" + fileName;
                //    SamtiPatra.SaveAs(Path.Combine(folder, fileName));


                //    //model.PdfUrl = filepath;
                //    try
                //    {
                //        Response.Write("Uploaded: " + fileName);
                //    }
                //    catch
                //    {

                //    }
                //}



        
            try
            {
                if (ModelState.IsValid)
                {
                    NDBObj.NewChitrakar_tbl.Add(ncmodel);
                    NDBObj.NewChitrakar_tbl.Add(ncmodel);
                    NDBObj.SaveChanges();
                    var data = NDBObj.NewChitrakar_tbl.Where(x => x.FullName == ncmodel.FullName).FirstOrDefault();
                    string file = Path.GetExtension(ncmodel.SamtiPatraF.FileName);
                    string file2 = Path.GetExtension(ncmodel.ChitraF.FileName);
                    string file3 = Path.GetExtension(ncmodel.Chitra2F.FileName);

                    string Filename = data.Id + ".SamtiPatra" + file;
                    string Filename2 = data.Id + ".Chitra " + file2;
                    string Filename3 = data.Id + ".Chitra2 " + file3;
                    ncmodel.SamtiPatra = Filename;
                    ncmodel.Chitra = Filename2;
                    ncmodel.Chitra2 = Filename3;

                    ncmodel.SamtiPatraF.SaveAs(Path.Combine(Server.MapPath("~/DownloadedFiles/Chitra"), Filename));
                    ncmodel.ChitraF.SaveAs(Path.Combine(Server.MapPath("~/DownloadedFiles/Chitra"), Filename2));
                    ncmodel.Chitra2F.SaveAs(Path.Combine(Server.MapPath("~/DownloadedFiles/Chitra"), Filename3));



                    NDBObj.NewChitrakar_tbl.Attach(ncmodel);
                    NDBObj.Entry(ncmodel).Property(x => x.SamtiPatra).IsModified = true;
                    NDBObj.Entry(ncmodel).Property(x => x.Chitra).IsModified = true;
                    NDBObj.Entry(ncmodel).Property(x => x.Chitra2).IsModified = true;
                    NDBObj.SaveChanges();
                    TempData["Msg"] = "save successfully...!";

                    ModelState.Clear();
                    return Redirect("ChitrakarInside");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Not save successfully...!";

                return RedirectToAction("ChitrakarInside");
            }
        }



        public ActionResult ChitrakarList()
        {
            var res = NDBObj.NewChitrakar_tbl.ToList();

            return View(res);
        }

        

        public FileResult DownloadFile(int id)
        {
            List<NewChitrakar_tbl> ObjFiles = GetFileList();
            var FileById = (from FC in ObjFiles
                            where FC.Id.Equals(id)
                            select new { FC.Chitra, FC.SamtiPatra,  }).ToList().FirstOrDefault();

            return File(FileById.Chitra, FileById.SamtiPatra );
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
                            select new {  FC.Chitra2,FC.Chitra }).ToList().FirstOrDefault();

            return File( FileById.Chitra2, FileById.Chitra);
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


        private SqlConnection con;
        private string constr;
       

        private void DbConnection()
        {
            constr = ConfigurationManager.ConnectionStrings["constr1"].ToString();
            con = new SqlConnection(constr);

        }
        //#endregion

        public string Uploadfile(HttpPostedFileBase file )

        {

            Random r = new Random();

            string path = "-1";

            int random = r.Next();

            if (file != null && file.ContentLength > 0)

            {

                string extension = Path.GetExtension(file.FileName);

                if (extension.ToLower().Equals(".pdf") || extension.ToLower().Equals(".docx") || extension.ToLower().Equals(".png"))

                {

                    try

                    {



                        path = Path.Combine(Server.MapPath("../DownloadedFiles/Lekh"), Path.GetFileName(file.FileName));

                        file.SaveAs(path);

                        path = "../DownloadedFiles/Lekh/" + Path.GetFileName(file.FileName);



                        //    ViewBag.Message = "File uploaded successfully";

                    }

                    catch (Exception ex)

                    {

                        path = "-1";

                    }

                }

                else

                {

                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");

                }

            }



            else

            {

                Response.Write("<script>alert('Please select a file'); </script>");

                path = "-1";

            }



            return path;

        }


        public string Uploadfont(HttpPostedFileBase file)

        {

            Random r = new Random();

            string path = "-1";

            int random = r.Next();

            if (file != null && file.ContentLength > 0)

            {

                string extension = Path.GetExtension(file.FileName);

                if (extension.ToLower().Equals(".ttf") )

                {

                    try

                    {



                        path = Path.Combine(Server.MapPath("../DownloadedFiles/Lekh"), Path.GetFileName(file.FileName));

                        file.SaveAs(path);

                        path = "../DownloadedFiles/Lekh/" + Path.GetFileName(file.FileName);



                        //    ViewBag.Message = "File uploaded successfully";

                    }

                    catch (Exception ex)

                    {

                        path = "-1";

                    }

                }

                else

                {

                    Response.Write("<script>alert('Only ttf formats are acceptable....'); </script>");

                }

            }



            else

            {

                Response.Write("<script>alert('Please select a file'); </script>");

                path = "-1";

            }



            return path;

        }

        public ActionResult MadhyamikShala()
        {
            School_tbl sch = new School_tbl();
            return View(sch);
        }

        [HttpPost]
        public ActionResult MadhyamikShala(School_tbl mmodel)
        {


            try
            {
                if (ModelState.IsValid)
                {
                    string hostName = Dns.GetHostName();
                    mmodel.Ip_Address = Dns.GetHostByName(hostName).AddressList[0].ToString();
                    mmodel.Date_Time = DateTime.Now;
                    NDBObj.School_tbl.Add(mmodel);
                    NDBObj.SaveChanges();
                    TempData["Msg"] = "save successfully...!";
                    ModelState.Clear();
                    return Redirect("http://shikshansankraman.msbshse.ac.in/dataFrom.htm");
                    //return Redirect("https://localhost:44356/dataFrom.htm");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Not save successfully...!";
                return RedirectToAction("MadhyamikShala");
            }
        }

        public ActionResult Mahavidyalaya()
        {
            Mahavidyalaya_tbl cllg = new Mahavidyalaya_tbl();
            return View(cllg);
        }
        [HttpPost]
        public ActionResult Mahavidyalaya(Mahavidyalaya_tbl kmmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string hostName = Dns.GetHostName();
                    kmmodel.Ip_Address = Dns.GetHostByName(hostName).AddressList[0].ToString();
                    kmmodel.Date_Time = DateTime.Now;
                    NDBObj.Mahavidyalaya_tbl.Add(kmmodel);
                    NDBObj.SaveChanges();
                    TempData["Msg"] = "save successfully...!";
                    ModelState.Clear();
                   // return Redirect("http://shikshansankraman.msbshse.ac.in/dataFrom.htm");
                    return Redirect("../dataFrom.htm");
              
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Not save successfully...!";
                return RedirectToAction("Mahavidyalaya");
            }
        
        }

        public ActionResult Karyalay()
        {
            NewKaryalay_tbl office = new NewKaryalay_tbl();
            return View(office);
        }
        [HttpPost]
        public ActionResult Karyalay(NewKaryalay_tbl kmodel)
        {
           ViewBag.abc = kmodel.Vargani;
            try
            {
                if (ModelState.IsValid)
                {
                    string hostName = Dns.GetHostName();
                    kmodel.Ip_Address = Dns.GetHostByName(hostName).AddressList[0].ToString();
                    kmodel.Date_Time = DateTime.Now;
                    NDBObj.NewKaryalay_tbl.Add(kmodel);
                    NDBObj.SaveChanges();
                    TempData["Msg"] = "save successfully...!";
                    ModelState.Clear();


                    return Redirect("http://shikshansankraman.msbshse.ac.in/dataFrom.htm");

                }
           
                else
                {
                    return View();

                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Not save successfully...!";
                return RedirectToAction("Karyalay");
            }


        }

        public ActionResult Varganidar()
        {
            NewVarganidar_tbl varg = new NewVarganidar_tbl();
            return View(varg);
        }

        [HttpPost]
        public ActionResult Varganidar(NewVarganidar_tbl vmodel, HttpPostedFileBase Photo)
        {
            NewVarganidar_tbl p = new NewVarganidar_tbl();

            string path = Uploadfile(Photo);
            

            if (path.Equals("-1"))
            {

            }
            else
            {
                vmodel.Photo = path;
                if (Photo != null && Photo.ContentLength > 0)
                {

                    string fileName = Path.GetFileName(Photo.FileName);
                    string folder = Server.MapPath("~/DownloadedFiles/Photo/");
                    Directory.CreateDirectory(folder);
                    string filepath = Path.Combine(folder, fileName);
                    Photo.SaveAs(filepath);
                    vmodel.Photo = "~/DownloadedFiles/Photo/" + fileName;
                    Photo.SaveAs(Path.Combine(folder, fileName));
                    //model.PdfUrl = filepath;
                    try
                    {
                        Response.Write("Uploaded: " + fileName);
                    }
                    catch
                    {

                    }
                }
                


            }
            try
            {
                if (ModelState.IsValid)
                {
                    string hostName = Dns.GetHostName();
                    vmodel.Ip_Address = Dns.GetHostByName(hostName).AddressList[0].ToString();
                    vmodel.Date_Time = DateTime.Now;
                    NDBObj.NewVarganidar_tbl.Add(vmodel);
                    NDBObj.SaveChanges();
                    TempData["Msg"] = "save successfully...!";
                    ModelState.Clear();
                    return Redirect("http://shikshansankraman.msbshse.ac.in/dataFrom.htm");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Not save successfully...!";

                return RedirectToAction("Varganidar");
            }
        }


        //public ActionResult AddVarganidar(NewVarganidar_tbl vmodel, HttpPostedFileBase Photo )
        //{
        //    NewVarganidar_tbl vobj = new NewVarganidar_tbl();
        //    string path = Uploadfile(Photo);

        //    if (path.Equals("-1"))
        //    {

        //    }
        //    else
        //    {
        //        vobj.Surname = vmodel.Surname;
        //        vobj.Name = vmodel.Name;
        //        vobj.FatherName = vmodel.FatherName;
        //        vobj.GharNav = vmodel.GharNav;
        //        vobj.RoadName = vmodel.RoadName;
        //        vobj.NearMark = vmodel.NearMark;
        //        vobj.CityName = vmodel.CityName;
        //        vobj.Taluka = vmodel.Taluka;
        //        vobj.Jhila = vmodel.Jhila;
        //        vobj.PincodeNo = vmodel.PincodeNo;
        //        vobj.Email = vmodel.Email;
        //        vobj.MobileNo1 = vmodel.MobileNo1;
        //        vobj.MobileNo2 = vmodel.MobileNo2;
        //        vobj.VarganiDate = vmodel.VarganiDate;
        //        vobj.AadharCardNo = vmodel.AadharCardNo;
        //        vobj.MonthStart = vmodel.MonthStart;
        //        vobj.MonthEnd = vmodel.MonthEnd;
        //        vobj.Photo = path;
        //        NDBObj.NewVarganidar_tbl.Add(vobj);
        //        NDBObj.SaveChanges();
        //    }




        //    return View("Varganidar");
        //}


        public ActionResult Pratikriya()
        {
            Pratikriya_tbl pra = new Pratikriya_tbl();
            return View(pra);
        }

        [HttpPost]
        public ActionResult Pratikriya(Pratikriya_tbl pmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    NDBObj.Pratikriya_tbl.Add(pmodel);
                    NDBObj.SaveChanges();
                    TempData["Msg"] = "save successfully...!";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Not save successfully...!";
                return RedirectToAction("Pratikriya");
            }

        }

        public ActionResult Postas()
        {
            return View();
        }
        public ActionResult Mandalas()
        {
            Tbl_Mandal mandal = new Tbl_Mandal();

            return View(mandal);
        }
        [HttpPost]
        public ActionResult Mandalas(Tbl_Mandal mandal)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    NDBObj.Tbl_Mandal.Add(mandal);
                    NDBObj.SaveChanges();

                    TempData["Msg"] = "save successfully...!";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Not save successfully...!";

                return RedirectToAction("mandal");
            }

        }
        public string Uploadimage(HttpPostedFileBase file)

        {

            string path = "-1";

            if (file != null && file.ContentLength > 0)

            {      
                string extension = Path.GetExtension(file.FileName);

                if (extension.ToLower().Equals(".pdf") || extension.ToLower().Equals(".docx") || extension.ToLower().Equals(".png"))

                {

                    try

                    {

                        path = Path.Combine(Server.MapPath("../DownloadedFiles/Chitra"),  Path.GetFileName(file.FileName));

                        file.SaveAs(path);

                        path = "../DownloadedFiles/Chitra/" + Path.GetFileName(file.FileName);

                        //    ViewBag.Message = "File uploaded successfully";

                    }

                    catch (Exception ex)

                    {

                        path = "-1";

                    }

                }

                else

                {

                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");

                }

            }



            else

            {
           
                Response.Write("<script>alert('Please select a file'); </script>");

                path = "-1";

            }
          


           return path;

        }

        //public string Uploadimage(HttpPostedFileBase file )

        //{

        //    Random r = new Random();

        //    string path = "-1";

        //    int random = r.Next();

        //    if (file != null && file.ContentLength > 0)

        //    {

        //        string extension = Path.GetExtension(file.FileName);

        //        if (extension.ToLower().Equals(".pdf") || extension.ToLower().Equals(".docx") || extension.ToLower().Equals(".png"))

        //        {

        //            try

        //            {



        //                path = Path.Combine(Server.MapPath("~/Design"), random + Path.GetFileName(file.FileName));

        //                file.SaveAs(path);

        //                path = "~/Content/upload/" + random + Path.GetFileName(file.FileName);



        //                //    ViewBag.Message = "File uploaded successfully";

        //            }

        //            catch (Exception ex)

        //            {

        //                path = "-1";

        //            }

        //        }

        //        else

        //        {

        //            Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");

        //        }

        //    }



        //    else

        //    {

        //        Response.Write("<script>alert('Please select a file'); </script>");

        //        path = "-1";

        //    }



        //    return path;

        //}





        public ActionResult Kshanchitre()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact_tbl model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    NDBObj.Contact_tbl.Add(model);
                    NDBObj.SaveChanges();

                    TempData["Msg"] = " save successfully...!";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Not save successfully...!";

                return RedirectToAction("Contact");
            }
        }
        //public ActionResult AddContact(Contact_tbl model)
        //{
        //    Contact_tbl obj = new Contact_tbl();
        //    obj.FirstName = model.FirstName;
        //    obj.MiddleName = model.MiddleName;
        //    obj.LastName = model.LastName;
        //    obj.EmailId = model.EmailId;
        //    obj.MobileNo = model.MobileNo;
        //    obj.Message = model.Message;
        //    NDBObj.Contact_tbl.Add(obj);
        //    NDBObj.SaveChanges();

        //    return View("Contact");
        //}
        
        public ActionResult demo()
        {
            return View();
        }
    }


}