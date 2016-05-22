using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstLineBot.Controllers
{
    public class chatController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/chat
        [HttpPost]
        public HttpResponseMessage Post()
        {
            //LineBot Helper
            LineBot.LineBotHelper LineBotHelper = new LineBot.LineBotHelper(
                   "你的Channel ID", "你的Channel Secret", "你的MID");
            //Get  Post RawData
            string postData = Request.Content.ReadAsStringAsync().Result;

            //取得LineBot接收到的訊息
            var ReceivedMessage = LineBotHelper.GetReceivedMessage(postData);

            //發送訊息
            var ret = LineBotHelper.SendMessage(
                new List<string>() { ReceivedMessage.result[0].content.from },
                   "你剛才說的是" + ReceivedMessage.result[0].content.text);
            
            //如果給200，LineBot訊息就不會重送
            return Request.CreateResponse(HttpStatusCode.OK, ret);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}