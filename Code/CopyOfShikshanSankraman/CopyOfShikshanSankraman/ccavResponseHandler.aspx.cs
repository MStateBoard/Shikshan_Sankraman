using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Specialized;
using CCA.Util;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


public partial class ResponseHandler : System.Web.UI.Page
    {
     SqlConnection _Con;
     SqlCommand _Command;

    protected void Page_Load(object sender, EventArgs e)
    {
        string workingKey = "8004CA82F3973451171537A3558199F7";//put in the 32bit alpha numeric key in the quotes provided here
        CCACrypto ccaCrypto = new CCACrypto();
        string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
        NameValueCollection Params = new NameValueCollection();
        string[] segments = encResponse.Split('&');
        foreach (string seg in segments)
        {
            string[] parts = seg.Split('=');
            if (parts.Length > 0)
            {
                string Key = parts[0].Trim();
                string Value = parts[1].Trim();
                Params.Add(Key, Value);
            }
        }

        for (int i = 0; i < Params.Count; i++)
        {

            //Response.Write(Params.Keys[i] + " = " + Params[i] + "<br>");
        }

        //----------------extra code----------------------------

        try
        {
            // Response.Write("insert into Tbl_Payment Values ('" + Params[0] + "', '" + Params[1] + "', '" + Params[2] + "', '" + Params[3] + "', '" + Params[4] + "', '" + Params[5] + "', '" + Params[6] + "', '" + Params[7] + "', '" + Params[8] + "', '" + Params[9] + "', '" + Params[10] + "', '" + Params[11] + "', '" + Params[12] + "', '" + Params[13] + "', '" + Params[14] + "', '" + Params[15] + "', '" + Params[16] + "', '" + Params[17] + "', '" + Params[18] + "', '" + Params[19] + "', '" + Params[20] + "', '" + Params[21] + "', '" + Params[22] + "', '" + Params[23] + "', '" + Params[24] + "', '" + Params[25] + "', '" + Params[26] + "', '" + Params[27] + "', '" + Params[28] + "', '" + Params[29] + "', '" + Params[30] + "', '" + Params[31] + "', '" + Params[32] + "', '" + Params[33] + "', '" + Params[34] + "', '" + Params[35] + "', '" + Params[36] + "', '" + Params[37] + "', '" + Params[38] + "', '" + Params[39] + "', '" + Params[40] + "', '" + Params[41] + "', '1')");

            _Con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr1"].ConnectionString);
            _Con.Open();
            _Command = new SqlCommand("insert into Tbl_payment Values ('" + Params[0] + "',	'" + Params[1] + "',	'" + Params[2] + "',	'" + Params[3] + "',	'" + Params[4] + "',	'" + Params[5] + "',	'" + Params[6] + "',	'" + Params[7] + "',	'" + Params[8] + "',	'" + Params[9] + "',	'" + Params[10] + "',	'" + Params[11] + "',	'" + Params[12] + "',	'" + Params[13] + "',	'" + Params[14] + "',	'" + Params[15] + "',	'" + Params[16] + "',	'" + Params[17] + "',	'" + Params[18] + "',	'" + Params[19] + "',	'" + Params[20] + "',	'" + Params[21] + "',	'" + Params[22] + "',	'" + Params[23] + "',	'" + Params[24] + "',	'" + Params[25] + "',	'" + Params[26] + "','" + Params[27] + "','" + Params[28] + "','" + Params[29] + "','" + Params[30] + "',	'" + Params[31] + "',	'" + Params[32] + "',	'" + Params[33] + "',	'" + Params[34] + "',	'" + Params[35] + "',	'" + Params[36] + "','" + Params[37] + "','" + Params[38] + "','" + Params[39] + "','" + Params[40] + "','" + Params[41] + "','1')", _Con);
            _Command.ExecuteNonQuery();


   
            DataTable dt = new DataTable();
            SqlDataAdapter _dataAdapter = new SqlDataAdapter("select * from Tbl_payment where order_id='" + Params[0] + "' and amount_money='" + Params[10] + "'", _Con);
            dt = new DataTable();
            _dataAdapter.Fill(dt);
            string ss = "";


           if (dt.Rows.Count >1) 
            {

                //ss += "<table border=1>";
                //ss += "<tr><td>Order ID:-</td><td>" + Params[0] + " </td></tr>";
                //ss += "<tr><td>Tracking ID:-</td><td>" + Params[1] + " </td</tr>";
                //ss += "<tr><td>Reference ID:-</td><td>" + Params[2] + " </td></tr>";                                                                                                                              
                //ss += "<tr><td>Payment Status is :-</td><td> Duplicate Payment </td></tr>";
                //ss += "<tr><td>Payment Mode:-</td><td>" + Params[5] + " </td></tr>";
                //ss += "<tr><td>Amount:-</td><td>" + Params[10] + " </td></tr>";
                //ss += "<tr><td>Trans Date:-</td><td>" + Params[40] + " </td></tr>";
                //ss += "<tr></table>";
                //Response.Write("<div align='center'><center> <h1>Payment Transaction Details</h1><br/>" + ss + "<span style=" + " font-size:x-large; font-weight:bold " + ">आपली रक्कम स्वीकारण्यात आली नाही पुन्हा पर्यंत करावा 1 <a href='http://shikshansankraman.msbshse.ac.in'>Click Here</a></span></center> </div>");

                Response.Write("<div style='border: 1px double; background-color:#152f49; color:white;'  align='center' ><center>  <h1>  शिक्षण संक्रमण </h1>  <h1>  महाराष्ट्र राज्य माध्यमिक व उच्च माध्यमिक शिक्षण मंडळ, पुणे </h1> Survey No. 832-A, Final Plot No. 178 & 179, Near Balchitrawani, Behind Agharkar Research Institute, Bhamburda, Shivajinagar, Pune - 411 004.Maharashtra(INDIA) Phone - 020 - 25705172</div></center></div>  <div style='margin-bottom:40px;'></div>  <div style='text - align:center'><center> <jumbotron style = 'color:red;font-size:2em;text-align:center;' ><i class='fa fa-check' aria-hidden='true'></i> Your payment for order ID<i class='fa fa-inr' aria-hidden='true'></i> " + Params[0] + " is Failed !</jumbotron></center></div>    <div style='margin-bottom:10px;'></div>   <div style = 'background:#F0F0F0; padding:10px; border:2px solid #c6c6c6; margin-right:400px; margin-left:400px' ><div style = 'background:#ffffff; margin:auto 2em 0 2em;border:2px solid #c6c6c6'><div style='margin-right: 40px; margin-left: 30px;'>   <h6 style='border: none;  border-bottom: 0px solid; margin: 0 0 5px 0; color: black;font-size:18; font-weight: 600;'>Transaction Date</h6>  <h3 style='font-size: 18px; color: #06213F;  font-weight: 600;  margin: 2px;  line-height: 24px; border-bottom: 0px solid;'>   <span id = 'Transaction_Date' > " + Params[40] + "</span> </h3> </div>      <div style='margin-bottom:30px;'></div> <div style='  margin-right: 40px; margin-left: 30px;'>  <h6 style='border: none;  border-bottom: 0px solid; margin: 0 0 5px 0; color: black; font-size:18; font-weight: 600;'>Transaction Amount</h6>  <h3 style='font-size: 18px; color: #06213F;  font-weight: 600;  margin: 2px;  line-height: 24px; border-bottom: 0px solid;'>    <span id = 'Transaction_Amount' > " + Params[10] + "</span>  </h3> </div>  </div> <div class='transact_box' style='background:#ffffff; margin:30px 2em 0 2em;border:2px solid #c6c6c6'> <div style = 'padding: 10px;' > <p class='message' style='margin-bottom:2px; color: #06213F; font-weight: 600;  margin: 2px 10px 20px 1px;  text-align: center;'> Your payment is <u>Failed</u>.We have sent an email confirmation to <span style='color:blue'> " + Params[18] + "</span> and a SMS   to " + Params[17] + "  confirming your transaction.   </p> </div>  <div style = 'padding: 10px; padding-bottom:0px' > <p class='message'>  If you don’t get an Email or SMS from us within 1 hour then, write to us at<span style='color:blue'> support@sabpaisa.in.</span>  </p>  </div> </div> </div> <div style='margin-bottom:20px;'></div> <center>  <div ><a href='http://shikshansankraman.msbshse.ac.in'>Try Again</a></div>");

            }
            //---------------------------------------------

            else if (Params[3].ToString() == "Success" && (Params[10].ToString() == "250.00" || Params[10].ToString() == "500.00" || Params[10].ToString() == "1.00"))
            {

                //ss += "<table border=1>";
                //ss += "<tr><td>Name:-</td><td>" + Params[11] + " </td></tr>";
                //ss += "<tr><td>Order ID:-</td><td>" + Params[0] + " </td></tr>";
                //ss += "<tr><td>Tracking ID:-</td><td>" + Params[1] + " </td</tr>";
                //ss += "<tr><td>Reference ID:-</td><td>" + Params[2] + " </td></tr>";
                //ss += "<tr><td>Payment Status is :-</td><td> Success </td></tr>";
                //ss += "<tr><td>Payment Mode:-</td><td>" + Params[5] + " </td></tr>";
                //ss += "<tr><td>Amount:-</td><td>" + Params[10] + " </td></tr>";
                //ss += "<tr><td>Trans Date:-</td><td>" + Params[40] + " </td></tr>";
                //ss += "<tr></table>";
                //               Response.Write(@"  <div style='border: 1px double;' align='center'><center>  <img src='logo.ico'  alt='शिक्षण संक्रमण' /> <h1>  शिक्षण संक्रमण </h1> 
                //               <h1>  महाराष्ट्र राज्य माध्यमिक व उच्च माध्यमिक शिक्षण मंडळ, पुणे </h1> Survey No. 832-A, Final Plot No. 178 & 179, Near Balchitrawani,
                //               Behind Agharkar Research Institute, Bhamburda, Shivajinagar,
                //               Pune - 411 004.Maharashtra(INDIA)
                //               Phone - 020 - 25705172
                //               <h4> Payment Transaction Details</h4>" + ss + "<span style=" + " font-size:x-large; font-weight:bold " + ">" +
                //"आपली रक्कम यशस्वीरीत्या स्वीकारण्यात आली आहे आपले मासिक सुरु करण्यात येईल  <a href='http://shikshansankraman.msbshse.ac.in'>Click Here</a></span></center> </div>");



                Response.Write("<div style='border: 1px double; background-color:#152f49; color:white;'  align='center' ><center>  <h1>  शिक्षण संक्रमण </h1>  <h1>  महाराष्ट्र राज्य माध्यमिक व उच्च माध्यमिक शिक्षण मंडळ, पुणे </h1> Survey No. 832-A, Final Plot No. 178 & 179, Near Balchitrawani, Behind Agharkar Research Institute, Bhamburda, Shivajinagar, Pune - 411 004.Maharashtra(INDIA) Phone - 020 - 25705172</div></center></div>  <div style='margin-bottom:40px;'></div>  <div style='text - align:center'><center> <jumbotron style = 'color:green;font-size:2em;text-align:center;' ><i class='fa fa-check' aria-hidden='true'></i> Your payment for order ID<i class='fa fa-inr' aria-hidden='true'></i> " + Params[0] + " is successful !</jumbotron></center></div>    <div style='margin-bottom:10px;'></div>   <div style = 'background:#F0F0F0; padding:10px; border:2px solid #c6c6c6; margin-right:400px; margin-left:400px' ><div style = 'background:#ffffff; margin:auto 2em 0 2em;border:2px solid #c6c6c6'><div style='margin-right: 40px; margin-left: 30px;'>   <h6 style='border: none;  border-bottom: 0px solid; margin: 0 0 5px 0; color: black;font-size:18; font-weight: 600;'>Transaction Date</h6>  <h3 style='font-size: 18px; color: #06213F;  font-weight: 600;  margin: 2px;  line-height: 24px; border-bottom: 0px solid;'>   <span id = 'Transaction_Date' > " + Params[40] + "</span> </h3> </div>      <div style='margin-bottom:30px;'></div> <div style='  margin-right: 40px; margin-left: 30px;'>  <h6 style='border: none;  border-bottom: 0px solid; margin: 0 0 5px 0; color: black; font-size:18; font-weight: 600;'>Transaction Amount</h6>  <h3 style='font-size: 18px; color: #06213F;  font-weight: 600;  margin: 2px;  line-height: 24px; border-bottom: 0px solid;'>    <span id = 'Transaction_Amount' > " + Params[10] + "</span>  </h3> </div>  </div> <div class='transact_box' style='background:#ffffff; margin:30px 2em 0 2em;border:2px solid #c6c6c6'> <div style = 'padding: 10px;' > <p class='message' style='margin-bottom:2px; color: #06213F; font-weight: 600;  margin: 2px 10px 20px 1px;  text-align: center;'> Your payment is <u>Successful</u>.We have sent an email confirmation to <span style='color:blue'> " + Params[18] + "</span> and a SMS   to " + Params[17] + "  confirming your transaction.   </p> </div>  <div style = 'padding: 10px; padding-bottom:0px' > <p class='message'>  If you don’t get an Email or SMS from us within 1 hour then, write to us at<span style='color:blue'> support@sabpaisa.in.</span>  </p>  </div> </div> </div> <div style='margin-bottom:20px;'></div> <center>  <button ><a href='http://shikshansankraman.msbshse.ac.in'>OK</a></button>");



            }
            else
            {

                //Response.Write("<div align='center'><center> <h1>Payment Transaction Details</h1><br/> <span style=" + " font-size:x-large; font-weight:bold " + ">आपली रक्कम स्वीकारण्यात आली नाही पुन्हा पर्यंत करावा 2 <a href='http://shikshansankraman.msbshse.ac.in'>Click Here</a></span></center> </div>");
                Response.Write("<div style='border: 1px double; background-color:#152f49; color:white;'  align='center' ><center>  <h1>  शिक्षण संक्रमण </h1>  <h1>  महाराष्ट्र राज्य माध्यमिक व उच्च माध्यमिक शिक्षण मंडळ, पुणे </h1> Survey No. 832-A, Final Plot No. 178 & 179, Near Balchitrawani, Behind Agharkar Research Institute, Bhamburda, Shivajinagar, Pune - 411 004.Maharashtra(INDIA) Phone - 020 - 25705172</div></center></div>  <div style='margin-bottom:40px;'></div>  <div style='text - align:center'><center> <jumbotron style = 'color:red;font-size:2em;text-align:center;' ><i class='fa fa-check' aria-hidden='true'></i> Your payment for order ID<i class='fa fa-inr' aria-hidden='true'></i> " + Params[0] + " is Failed !</jumbotron></center></div>    <div style='margin-bottom:10px;'></div>   <div style = 'background:#F0F0F0; padding:10px; border:2px solid #c6c6c6; margin-right:400px; margin-left:400px' ><div style = 'background:#ffffff; margin:auto 2em 0 2em;border:2px solid #c6c6c6'><div style='margin-right: 40px; margin-left: 30px;'>   <h6 style='border: none;  border-bottom: 0px solid; margin: 0 0 5px 0; color: black;font-size:18; font-weight: 600;'>Transaction Date</h6>  <h3 style='font-size: 18px; color: #06213F;  font-weight: 600;  margin: 2px;  line-height: 24px; border-bottom: 0px solid;'>   <span id = 'Transaction_Date' > " + Params[40] + "</span> </h3> </div>      <div style='margin-bottom:30px;'></div> <div style='  margin-right: 40px; margin-left: 30px;'>  <h6 style='border: none;  border-bottom: 0px solid; margin: 0 0 5px 0; color: black; font-size:18; font-weight: 600;'>Transaction Amount</h6>  <h3 style='font-size: 18px; color: #06213F;  font-weight: 600;  margin: 2px;  line-height: 24px; border-bottom: 0px solid;'>    <span id = 'Transaction_Amount' > " + Params[10] + "</span>  </h3> </div>  </div> <div class='transact_box' style='background:#ffffff; margin:30px 2em 0 2em;border:2px solid #c6c6c6'> <div style = 'padding: 10px;' > <p class='message' style='margin-bottom:2px; color: #06213F; font-weight: 600;  margin: 2px 10px 20px 1px;  text-align: center;'> Your payment is <u>Failed</u>.We have sent an email confirmation to <span style='color:blue'> " + Params[18] + "</span> and a SMS   to " + Params[17] + "  confirming your transaction.   </p> </div>  <div style = 'padding: 10px; padding-bottom:0px' > <p class='message'>  If you don’t get an Email or SMS from us within 1 hour then, write to us at<span style='color:blue'> support@sabpaisa.in.</span>  </p>  </div> </div> </div> <div style='margin-bottom:20px;'></div> <center>  <div ><a href='http://shikshansankraman.msbshse.ac.in'>Try Again2</a></div>");

            }
            _Con.Close();

        }
        catch (Exception exe)
        {
          
            Response.Write("<div align='center'><center> <h1>Payment Transaction Details</h1><br/> <span style=" + " font-size:x-large; font-weight:bold " + ">आपली रक्कम स्वीकारण्यात आली नाही पुन्हा पर्यंत करावा 3<a href='http://shikshansankraman.msbshse.ac.in'>"+exe+"Click Here</a></span></center> </div>");
            _Con.Close();
        }

       
    }
}
