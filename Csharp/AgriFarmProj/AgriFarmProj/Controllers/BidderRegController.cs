using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AgriFarmProj.Models;
using AgriFarmProj.ViewModel;

namespace AgriFarmProj.Controllers
{
    public class BidderRegController : ApiController
    {
        int bid;

        dbProjectEntities1 db = new dbProjectEntities1();

        [HttpPost]
        public void Post([FromBody] BidderReg bm)
        {
            tblBidder tb = new tblBidder();
            tb.BidderName = bm.BidderName;
            tb.BidderEmail = bm.BidderEmail;
            tb.BidderContactNo = bm.BidderContactNo;
            tb.BidderAddress = bm.BidderAddress;
            tb.BidderCity = bm.BidderCity;
            tb.BidderState = bm.BidderState;
            tb.BidderPincocde = bm.BidderPincocde;
            tb.BidderAadhar = bm.BidderAadhar;
            tb.BidderTradeLicense = bm.BidderTradeLicense;
            tb.BidderPassword = bm.BidderPassword;
            byte[] encData_byte = new byte[tb.BidderPassword.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(tb.BidderPassword);
            string encodedpassword = Convert.ToBase64String(encData_byte);
            tb.BidderPassword = encodedpassword;

            tb.ApprovalAdminId = null;
            tb.BidderApproved = false;
            try
            {
                db.tblBidders.Add(tb);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            List<tblBidder> res = db.tblBidders.ToList();
            foreach (tblBidder item in res)
            {
                if (item.BidderEmail == bm.BidderEmail)
                {
                    bid = item.Bidderid;
                    break;
                }
            }

            tblBank tb1 = new tblBank();
            tb1.Bidderid = bid;
            tb1.AccountNo = bm.AccountNo;
            tb1.IFSC_Code = bm.IFSC_Code;
            tb1.Farmerid = null;
            db.tblBanks.Add(tb1);
            db.SaveChanges();

        }

        [HttpPost]
        [Route("RegBidder")]
        public HttpResponseMessage WayTwo()
        {
            string imageName = "";
            string imageName1 = "";

            string Qty = "";
            var httpRequest = HttpContext.Current.Request;
            //Upload Image
            var postedFile = httpRequest.Files["Image"];
            var postedFile1 = httpRequest.Files["Image1"];

            //Create custom filename
            imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
            var filePath = HttpContext.Current.Server.MapPath("~/Image/" + imageName);
            postedFile.SaveAs(filePath);

            imageName1 = new String(Path.GetFileNameWithoutExtension(postedFile1.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName1 = imageName1 + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile1.FileName);
            var filePath1 = HttpContext.Current.Server.MapPath("~/Image/" + imageName1);
            postedFile1.SaveAs(filePath1);


            tblBidder br = new tblBidder();
            //Save to DB
            using (dbProjectEntities1 db = new dbProjectEntities1())
            {
                br.BidderAadhar = imageName;
                br.BidderTradeLicense = imageName1;

                br.BidderName = httpRequest["BidderName"];
                br.BidderEmail = httpRequest["BidderEmail"];
                br.BidderContactNo = httpRequest["BidderContactNo"];
                br.BidderAddress = httpRequest["BidderAddress"];
                br.BidderCity = httpRequest["BidderCity"];
                br.BidderState = httpRequest["BidderState"];
                br.BidderPincocde = httpRequest["BidderPincocde"];
                br.BidderPassword = httpRequest["BidderPassword"];
                byte[] encData_byte = new byte[br.BidderPassword.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(br.BidderPassword);
                string encodedpassword = Convert.ToBase64String(encData_byte);
                br.BidderPassword = encodedpassword;

                br.BidderApproved = false;
                db.tblBidders.Add(br);
                db.SaveChanges();

            }

            List<tblBidder> res = db.tblBidders.ToList();
            foreach (tblBidder item in res)
            {
                if (item.BidderEmail == br.BidderEmail)
                {
                    bid = item.Bidderid;
                    break;
                }
            }

            tblBank tb1 = new tblBank();

            using (dbProjectEntities1 db = new dbProjectEntities1())
            {

                tb1.AccountNo = httpRequest["AccountNo"];
                tb1.IFSC_Code = httpRequest["IFSC_Code"];

            }

            /*
             formData.append('Image', fileToUpload, fileToUpload.name);
    formData.append('Image1', fileToUpload1, fileToUpload1.name);

    formData.append('BidderName', BidderName);
    formData.append('BidderEmail', BidderEmail);
    formData.append('BidderContactNo', BidderContactNo);
    formData.append('BidderAddress', BidderAddress);
    formData.append('BidderCity', BidderCity);
    formData.append('BidderState', BidderState);
    formData.append('BidderPincocde', BidderPincocde);
    
    formData.append('BidderPassword', BidderPassword);
    formData.append('AccountNo', AccountNo);
    formData.append('IFSC_Code', IFSC_Code);
    formData.append('AccountNo', AccountNo);            */
            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
