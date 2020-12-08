using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AgriFarmProj.Models;

namespace AgriFarmProj.Controllers
{
    public class FarmerMarketPlaceController : ApiController
    {
        dbProjectEntities1 db = new dbProjectEntities1();

        [HttpGet]
        [Route("Farmerplace")]
        public HttpResponseMessage Get()
        {
            var cp = (from crps in db.tblCropRequests 
                      join bd in db.tblBiddings on 
                      crps.Requestid equals bd.Requestid 
                      select new {
                      crps.CropType,
                      crps.CropName,
                      bd.InitalPrce,
                      bd.PreviousBidPrice,
                      bd.BidCloseTime,
                      bd.CurrentBidPrice}).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, cp);
        }
    }
}
