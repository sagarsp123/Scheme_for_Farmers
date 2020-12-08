using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using AgriFarmProj.Models;
using AgriFarmProj.ViewModel;
using System.Net;

namespace AgriFarmProj.Controllers
{
    public class MailController : ApiController
    {
        dbProjectEntities1 db = new dbProjectEntities1();

        [HttpPost]
        public async Task<HttpResponseMessage> SendEmail([FromBody]Email email)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress("<" + email.To + ">"));
            message.From = new MailAddress("AgriFarm <rakshandahedawoo@gmail.com>");
            message.Subject = email.Subject;
            message.Body = email.Body;
            message.IsBodyHtml = true;
            try
            {
                var smtp = new SmtpClient();
                await smtp.SendMailAsync(message);
                return Request.CreateResponse(HttpStatusCode.OK, ("mail sent",email.To));
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeStatus([FromUri] string mail,int id)
        {
            using(var db=new dbProjectEntities1())
            {
                var currentmsg = db.tblContactUs.Where(c => c.Email.Equals(mail))
                    .FirstOrDefault<tblContactU>();
                if (currentmsg != null)
                {
                    currentmsg.status = "Replied";
                    currentmsg.ApprovalAdminId = id;
                    db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }
    }
}
