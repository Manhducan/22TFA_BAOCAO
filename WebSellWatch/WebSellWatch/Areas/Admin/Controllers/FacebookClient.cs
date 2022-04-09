using System;
using System.Collections.Generic;

namespace WebSellWatch.Areas.Admin.Controllers
{
    internal class FacebookClient
    {
        private string access_token;

        public FacebookClient(string access_token)
        {
            this.access_token = access_token;
        }

        public string AppId { get; internal set; }
        public string AppSecret { get; internal set; }

        internal void Post(string v, string text)
        {
            throw new NotImplementedException();
        }

        internal void Post(string v, Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }



        /* internal void Post(string v, dynamic parameters)
         {
             throw new NotImplementedException();
         }*/


    }
}