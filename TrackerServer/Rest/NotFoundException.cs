using System;
using RestSharp;

namespace TrackerServer.Rest
{
    class NotFoundException : Exception
    {
        public RestRequest Request { get; set; }
        public RestResponse Response { get; set; }
        public string ErrorMessage { get; set; }

        internal NotFoundException(RestRequest req, RestResponse res) : base("Not Found: " + res.ResponseStatus)
        {
            Request = req;
            Response = res;
            ErrorMessage = Response.Content;
        }
    }
}