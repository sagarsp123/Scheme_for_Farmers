using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using AgriFarmProj.Models;
using AgriFarmProj.ViewModel;

namespace AgriFarmProj.Controllers
{
    public class RegistrationController : ApiController
    {
        int fid;

        dbProjectEntities1 db = new dbProjectEntities1();
        
        [HttpPost]
        public void Post([FromBody] FarmerReg fm)
        {
            tblFarmer tb = new tblFarmer();
                tb.FarmerName = fm.FarmerName;
                tb.FarmerCity = fm.FarmerCity;
                tb.FarmerAddress = fm.FarmerAddress;
                tb.FarmerContactNo = fm.FarmerContactNo;
                tb.FarmerEmail = fm.FarmerEmail;
                tb.FarmerState = fm.FarmerState;
                tb.FarmerPincocde = fm.FarmerPincocde;
                tb.FarmerAadhar = fm.FarmerAadhar;
                tb.FarmerCertificate = fm.FarmerCertificate;
                tb.FarmerPassword = fm.FarmerPassword;
                byte[] encData_byte = new byte[tb.FarmerPassword.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(tb.FarmerPassword);
                string encodedpassword = Convert.ToBase64String(encData_byte);
                tb.FarmerPassword = encodedpassword;
                tb.ApprovalAdminId = null;
                tb.FarmerApproved = false;
            try
            {
                db.tblFarmers.Add(tb);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            List<tblFarmer> res = db.tblFarmers.ToList();
            foreach (tblFarmer item in res)
            {
                if (item.FarmerEmail == fm.FarmerEmail)
                {
                    fid = item.Farmerid;
                    break;
                }
            }

            tblBank tb1 = new tblBank();
            tb1.Farmerid = fid;
            tb1.AccountNo = fm.AccountNo;
            tb1.IFSC_Code = fm.IFSC_Code;
            tb1.Bidderid = null;
            db.tblBanks.Add(tb1);
            db.SaveChanges();

            tblFarmLand tb2 = new tblFarmLand();
            tb2.Farmerid = fid;
            tb2.FarmerLandAddress = fm.FarmerLandAddress;
            tb2.FarmerLandArea = fm.FarmerLandArea;
            tb2.FarmerLandPincode = fm.FarmerLandPincode;
            db.tblFarmLands.Add(tb2);
            db.SaveChanges();

        }



        [HttpPost]
        [Route("Methodtwo")]
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
            //var filePath1 = HttpContext.Current.Server.MapPath("D:/Project_Angular/AgriFarmProj/src/assets/uploadeddocs/" + imageName1);
            postedFile1.SaveAs(filePath1);


            tblFarmer fmr = new tblFarmer();
            //Save to DB
            using (dbProjectEntities1 db = new dbProjectEntities1())
            {
                fmr.FarmerAadhar = imageName;
                fmr.FarmerCertificate = imageName1;

                fmr.FarmerName = httpRequest["FarmerName"];
                fmr.FarmerContactNo = httpRequest["FarmerContactNo"];
                fmr.FarmerAddress = httpRequest["FarmerAddress"];
                fmr.FarmerCity = httpRequest["FarmerCity"];
                fmr.FarmerState = httpRequest["FarmerState"];
                fmr.FarmerPincocde = httpRequest["FarmerPincocde"];
                fmr.FarmerEmail = httpRequest["FarmerEmail"];
                fmr.FarmerPassword = httpRequest["FarmerPassword"];
                fmr.FarmerApproved = false;
                byte[] encData_byte = new byte[fmr.FarmerPassword.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(fmr.FarmerPassword);
                string encodedpassword = Convert.ToBase64String(encData_byte);
                fmr.FarmerPassword = encodedpassword;

                db.tblFarmers.Add(fmr);
                db.SaveChanges();

            }

            List<tblFarmer> res = db.tblFarmers.ToList();
            foreach (tblFarmer item in res)
            {
                if (item.FarmerEmail == fmr.FarmerEmail)
                {
                    fid = item.Farmerid;
                    break;
                }
            }

            tblBank tb1 = new tblBank();

            using (dbProjectEntities1 db = new dbProjectEntities1())
            {

                tb1.AccountNo = httpRequest["AccountNo"];
                tb1.IFSC_Code = httpRequest["IFSC_Code"];

            }

            tblFarmLand tb2 = new tblFarmLand();

            using (dbProjectEntities1 db = new dbProjectEntities1())
            {

                tb2.FarmerLandArea = httpRequest["FarmerLandArea"];
                tb2.FarmerLandAddress = httpRequest["FarmerLandAddress"];
                tb2.FarmerLandPincode = httpRequest["FarmerLandPincode"];
            }

            /*
             formData.append('Image', fileToUpload, fileToUpload.name);
    formData.append('Image1', fileToUpload1, fileToUpload1.name);

    formData.append('FarmerName', FarmerName);
    formData.append('FarmerContactNo', FarmerContactNo);
    formData.append('FarmerAddress', FarmerAddress);
    formData.append('FarmerCity', FarmerCity);
    formData.append('FarmerState', FarmerState);
    formData.append('FarmerPincocde', FarmerPincocde);
    formData.append('FarmerEmail', FarmerEmail);
    formData.append('FarmerAadhar', FarmerAadhar);
    formData.append('FarmerPassword', FarmerPassword);
    formData.append('FarmerLandArea', FarmerLandArea);
    formData.append('FarmerLandPincode', FarmerLandPincode);
    formData.append('AccountNo', AccountNo);
    formData.append('IFSC_Code', IFSC_Code);
    formData.append('FarmerLandAddress', FarmerLandAddress);
    formData.append('FarmerCertificate', FarmerCertificate);
             */
            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
