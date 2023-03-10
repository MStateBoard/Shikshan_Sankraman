using CopyOfShikshanSankraman.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CopyOfShikshanSankraman.Controllers
{
    public class AdminController : Controller
    {
        NewDatabaseEntities1 NDBObj = new NewDatabaseEntities1();


        // GET: Admin
        public ActionResult Homepage()
        {
            return View();

        }

        public ActionResult AddData(Admin_tbl1 model, HttpPostedFileBase PdfUrl, HttpPostedFileBase Image)
        {
            Admin_tbl1 obj = new Admin_tbl1();
            string path = Uploadfile(PdfUrl);
            string path1 = Uploadfile1(Image);


            if (model.Id != 0)
            {
                var oldrecord = NDBObj.Admin_tbl1.Where(x => x.Id == model.Id).FirstOrDefault();
                oldrecord.Year = model.Year;
                oldrecord.Month = model.Month;
                //oldrecord.IPAddress = model.IPAddress;
                string hostName = Dns.GetHostName();
                oldrecord.IPAddress = Dns.GetHostByName(hostName).AddressList[0].ToString();
                oldrecord.FileName = model.FileName;
                //oldrecord.DateTime = model.DateTime;
                oldrecord.DateTime = DateTime.Now;
                //oldrecord.PdfUrl = model.PdfUrl;
                
                NDBObj.Admin_tbl1.Attach(oldrecord);
                NDBObj.Entry(oldrecord).Property(x => x.Year).IsModified = true;
                NDBObj.Entry(oldrecord).Property(x => x.Month).IsModified = true;
                NDBObj.Entry(oldrecord).Property(x => x.IPAddress).IsModified = true;
                NDBObj.Entry(oldrecord).Property(x => x.FileName).IsModified = true;
                NDBObj.Entry(oldrecord).Property(x => x.DateTime).IsModified = true;
                


                //------------------------------------------------------------------------------------------------------------------------



                if (PdfUrl != null)
                {
                    // Get File Name
                    var fileName = Path.GetFileName(PdfUrl.FileName);

                    // If file present
                    if (fileName != "")
                    {
                        // Get Application folder path and combine it with file name
                        var filePath = Path.Combine(Server.MapPath("~/Upload/PDF/"), fileName);

                        // If same name of file present then delete that file first
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        // Save file
                        PdfUrl.SaveAs(filePath);
                        oldrecord.PdfUrl = "../Upload/PDF/"+fileName+"";
                        PdfUrl.SaveAs(filePath);
                        NDBObj.Entry(oldrecord).Property(x => x.PdfUrl).IsModified = true;
                    }

                }

                if (Image != null)
                {
                    // Get File Name
                    var fileName = Path.GetFileName(Image.FileName);

                    // If file present
                    if (fileName != "")
                    {
                        // Get Application folder path and combine it with file name
                        var filePath = Path.Combine(Server.MapPath("~/Upload/Image/"), fileName);

                        // If same name of file present then delete that file first
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        // Save file
                        Image.SaveAs(filePath);
                        oldrecord.Image = "../Upload/Image/" + fileName + "";
                        Image.SaveAs(filePath);
                        NDBObj.Entry(oldrecord).Property(x => x.Image).IsModified = true;
                    }

                }


                int ID = NDBObj.SaveChanges();


                //--------------------------------------------------------------------------------------------------------------------------------
                //if (ID != null || ID != 0)
                //{
                //    if (PdfUrl != null && PdfUrl.ContentLength > 0)
                //    {

                //        string fileName = Path.GetFileName(PdfUrl.FileName);
                //        string folder = Server.MapPath("~/Upload/PDF/");
                //        Directory.CreateDirectory(folder);
                //        PdfUrl.SaveAs(Path.Combine(folder, fileName));
                //        try
                //        {

                //            Response.Write("Uploaded: " + fileName);
                //        }
                //        catch
                //        {

                //        }
                //    }
                //}
            }
            else
            {
                if (path.Equals("-1"))
                {

                }
                else
                {

                    string hostName = Dns.GetHostName();
                    model.IPAddress = Dns.GetHostByName(hostName).AddressList[0].ToString();
                    model.DateTime = DateTime.Now;
                    model.PdfUrl = path;
                    model.Image = path1;
                    
                    //int ID=dbObj.SaveChanges();

                    if (PdfUrl != null && PdfUrl.ContentLength > 0)
                    {

                        string fileName = Path.GetFileName(PdfUrl.FileName);
                        string folder = Server.MapPath("~/Upload/PDF/");
                        Directory.CreateDirectory(folder);
                        string filepath = Path.Combine(folder, fileName);
                        PdfUrl.SaveAs(filepath);
                        model.PdfUrl = "https://drive.google.com/file/d/" + fileName + "/preview";
                        //model.PdfUrl = "D:/akshada/CopyOfShikshanSankraman/CopyOfShikshanSankraman/Upload/PDF/" + fileName ;
                    
                        PdfUrl.SaveAs(Path.Combine(folder, fileName));
                        //model.PdfUrl = filepath;
                        try
                        {
                            Response.Write("Uploaded: " + fileName);
                        }
                        catch
                        {

                        }
                    }
                    if (Image != null && Image.ContentLength > 0)
                    {

                        string fileName = Path.GetFileName(Image.FileName);
                        string folder = Server.MapPath("~/Upload/Image/");
                        Directory.CreateDirectory(folder);
                        string filepath = Path.Combine(folder, fileName);
                        Image.SaveAs(filepath);
                        model.Image = "../Upload/Image/" + fileName + "";
                        Image.SaveAs(Path.Combine(folder, fileName));
                        try
                        {
                            Response.Write("Uploaded: " + fileName);
                        }
                        catch
                        {

                        }
                    }

                    NDBObj.Admin_tbl1.Add(model);
                    NDBObj.SaveChanges();
                    /*
                    if(model.Id==0)
                    {
                        dbObj.Admin_tbl.Add(obj);
                        dbObj.SaveChanges();
                    }
                    else
                    {
                        dbObj.Entry(obj).State = EntityState.Modified;
                        dbObj.SaveChanges();
                    }
                    */
                }
            }

            return View("Homepage");
        }


        /*
            private string SaveToPhysicalLocation(HttpPostedFileBase file)
            {
                if(file.ContentLength>0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Design"), fileName);
                    file.SaveAs(path);
                    return path;
                }

                return string.Empty;
            }
        */
        public JsonResult GetPrductDetails()
        {
            var records = (from p in NDBObj.Admin_tbl1 select p).ToList();
            return Json(new { Result = true, Response = records }, JsonRequestBehavior.AllowGet);
        }
      
        public JsonResult GetProdByID(int ID)
        {
            try
            {
                var record = NDBObj.Admin_tbl1.Where(x => x.Id == ID).FirstOrDefault();
                return Json(new { Result = true, Response = record }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Cannot Fetch" }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult DeleteProdByID(int ID)
        {
            try
            {
                var record = NDBObj.Admin_tbl1.Where(x => x.Id == ID).FirstOrDefault();
                NDBObj.Admin_tbl1.Remove(record);
                NDBObj.SaveChanges();
                return Json(new { Result = true, Response = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Response = "Cannot delete" }, JsonRequestBehavior.AllowGet);
            }

        }







        public string Uploadfile(HttpPostedFileBase file)

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



                        path = Path.Combine(Server.MapPath("~/Design"), random + Path.GetFileName(file.FileName));

                        file.SaveAs(path);

                        path = "~/Content/upload/" + random + Path.GetFileName(file.FileName);



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
        public string Uploadfile1(HttpPostedFileBase file)

        {

            Random r = new Random();

            string path1 = "-1";

            int random = r.Next();

            if (file != null && file.ContentLength > 0)

            {

                string extension = Path.GetExtension(file.FileName);

                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))

                {

                    try

                    {



                        path1 = Path.Combine(Server.MapPath("~/Design"), random + Path.GetFileName(file.FileName));

                        file.SaveAs(path1);

                        path1 = "~/Content/upload/" + random + Path.GetFileName(file.FileName);



                        //    ViewBag.Message = "File uploaded successfully";

                    }

                    catch (Exception ex)

                    {

                        path1 = "-1";

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

                path1 = "-1";

            }



            return path1;

        }
        public ActionResult AdminList()
        {
            var res = NDBObj.Admin_tbl1.ToList();

            return View(res);
        }

        public ActionResult Delete(int id)
        {
            var res = NDBObj.Admin_tbl1.Where(x => x.Id == id).First();
            NDBObj.Admin_tbl1.Remove(res);
            NDBObj.SaveChanges();

            var list = NDBObj.Admin_tbl1.ToList();
            return View("AdminList", list);
        }


    }
}