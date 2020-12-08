using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AgriFarmProj.Models;
namespace AgriFarmProj.Controllers
{
    public class CropController : ApiController
    { 
        dbProjectEntities1 db = new dbProjectEntities1();

        [Route("Request")]
        public HttpResponseMessage Get()
        {
            var cp = db.tblCropRequests.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, cp);
        }

        [HttpPost]
        [Route("List")]

        public void Listing([FromBody] tblCropRequest cropRequest)
        {
            /*
            //Saving image 
            
            string imageName = null;
            var httpRequest = HttpContext.Current.Request;
            //Upload image
            var postedFile = httpRequest.Files["Image"];
            imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
            var filePath = HttpContext.Current.Server.MapPath("~/Image/" + imageName);
            postedFile.SaveAs(filePath);
            */
            cropRequest.SoilPhCertificate=cropRequest.SoilPhCertificate.Replace("/","-");
            cropRequest.Farmerid = 100;
            cropRequest.CropApproved = false;
            db.tblCropRequests.Add(cropRequest);
            db.SaveChanges();
        }

        [HttpPost]
        [Route("CropRequest")]
        public HttpResponseMessage WayTwo()
        {
            string imageName = "";
            string Qty = "";
            var httpRequest = HttpContext.Current.Request;
            //Upload Image
            var postedFile = httpRequest.Files["Image"];
            //Create custom filename
            imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
            var filePath = HttpContext.Current.Server.MapPath("~/Image/" + imageName);
            postedFile.SaveAs(filePath);

            //Save to DB
            using (dbProjectEntities1 db = new dbProjectEntities1())
            {
                tblCropRequest cpr = new tblCropRequest();

                cpr.SoilPhCertificate = imageName;
                cpr.CropType = httpRequest["CropType"];
                cpr.CropName = httpRequest["CropName"];
                cpr.FertilizerType = httpRequest["FertilizerType"];
                Qty = (httpRequest["Quantity"]);
                //Quantity = 25
                cpr.Quantity = Convert.ToInt32(Qty);
                cpr.Farmerid = Convert.ToInt32(httpRequest["FarmerID"]);
                cpr.CropApproved = false;
                db.tblCropRequests.Add(cpr);
                db.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
