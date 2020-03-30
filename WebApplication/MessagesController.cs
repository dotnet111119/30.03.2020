using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using WebApplication11.Models;

namespace WebApplication11.Controllers
{

    [BasicAuthentication]
    public class MessagesController : ApiController
    {
        // HTTPRequest  : head, body
        // HTTPResponse : head, body
        private static List<Message> messages = new List<Message>();

        private static int counter = 0;

        static MessagesController()
        {
            messages.Add(new Message { Id = 1, Sender = "Danny", Text = "Hello from Danny!!!" });
            messages.Add(new Message { Id = 2, Sender = "Galit", Text = "How are you Danny?" });
            messages.Add(new Message { Id = 3, Sender = "Danny", Text = "I'm good." });
            messages.Add(new Message { Id = 4, Sender = "Steve", Text = "What's up?" });
            messages.Add(new Message { Id = 5, Sender = "Danny", Text = "I'm good. again" });
            messages.Add(new Message { Id = 8, Sender = "Itay", Text = "I am a messgae from Itay!" });
            counter = messages.Count;
        }

        // GET
        [HttpGet]
        [Route("api/messages/")]
        public IHttpActionResult Get() // return IHttpActionResult -- instead of HttpResponseMessage !!
        {
            // Head
            // Body

            Debug.WriteLine(BasicAuthenticationAttribute.username_);

            // if error occured
            //return Request.CreateResponse(HttpStatusCode.BadRequest, "data base offline!");

            //return Request.CreateResponse(HttpStatusCode.OK, messages);

            Request.Properties.TryGetValue("username", out object username);


            string username1 = Thread.CurrentPrincipal.Identity.Name;

            return Ok(messages.Where(m => m.Sender.ToUpper() == username.ToString().ToUpper()));
            //return messages;
        }

        // there is no oveloading....
        // path paramter = { .. }
        [Route("api/messages/getbysender/{sender}")]
        [HttpGet]
        public IHttpActionResult FetchBySender([FromUri] string sender)
        {
            var result = messages.FirstOrDefault(m => m.Sender.ToUpper().Contains(sender.ToUpper()));
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // path paramter  / { ... } 
        [Route("api/messages/getbysenderandtext/{sender}/{text}")]
        [HttpGet]
        public IHttpActionResult FetchBySenderAndText([FromUri] string sender, [FromUri] string text)
        {
            var result = messages.FirstOrDefault(m => m.Sender.ToUpper().Contains(sender.ToUpper())
                                                && m.Text.ToUpper().Contains(text.ToUpper()));
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // GET ID
        [Route("api/messages/{id}", Name = "GetById")]
        public IHttpActionResult Get(int id)
        {
            var result = messages.FirstOrDefault(m => m.Id == id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // POST
        [HttpPost]
        [Route("api/messages/")]
        public IHttpActionResult AddMessage([FromBody] Message message)
        {
            message.Id = ++counter; // add 1 before putting counter value into Id
            messages.Add(message);

            // send url to user of the new created message -- messages/{id}

            // fallback for all results
            //return Content(HttpStatusCode.Created, "created");

            return CreatedAtRoute("GetById", new { id = message.Id }, "body");


        }

        // PUT
        [HttpPut]
        [Route("api/messages/{id}")]
        public IHttpActionResult UpdateMessage([FromUri] int id, [FromBody] Message message)
        {
            var result = messages.FirstOrDefault(m => m.Id == id);
            if (result != null)
            {
                //result.Id = message.Id;
                result.Sender = message.Sender;
                result.Text = message.Text;

                return Ok();
            }
            return NotFound();
        }

        // DELETE
        [HttpDelete]
        [Route("api/messages/{id}")]
        public IHttpActionResult DeleteMessage(int id)
        {
            var result = messages.FirstOrDefault(m => m.Id == id);
            if (result != null)
            {
                messages.Remove(result);
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/messages/search")]
        public IHttpActionResult SearchMessages(string sender = "", string text = "", int max = int.MaxValue)
        {
            var result = messages.Where(m => m.Sender.ToUpper().Contains(sender.ToUpper()) &&
                                                    m.Text.ToUpper().Contains(text.ToUpper())).Take(max);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

    }
}
