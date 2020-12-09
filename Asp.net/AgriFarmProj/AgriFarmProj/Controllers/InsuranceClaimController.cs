﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AgriFarmProj.Models;


namespace AgriFarmProj.Controllers
{
    public class InsuranceClaimController : ApiController
    {

        dbProjectEntities1 db = new dbProjectEntities1();
        [Route("IsuranceRequest")]
        public HttpResponseMessage Get()
        {
            var IC = db.tblInsuranceClaims.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, IC);
        }

        [Route("IsuranceList")]
        public void post([FromBody] tblInsuranceClaim insuranceCliam)
        {
            
            insuranceCliam.ApprovalStatus ="Not Approved";
            db.tblInsuranceClaims.Add(insuranceCliam);
            db.SaveChanges();
        }

        [Route("Specific")]
        public IHttpActionResult Get(int id) 
        {
            var x = db.tblInsurances.Where(d => d.Farmerid == id).ToList();
            return Ok(x);
        }
    }
}
