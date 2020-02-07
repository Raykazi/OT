using System;
using RestSharp;

namespace TrackerServer.Rest
{
    class UnauthorizedException : Exception
    {
        public RestRequest Request { get; set; }
        public RestResponse Response { get; set; }
        public string ErrorMessage { get; set; }

        internal UnauthorizedException(RestRequest req, RestResponse res) : base("Unauthorized: " + res.ResponseStatus)
        {
            Request = req;
            Response = res;
            ErrorMessage = Response.Content;
        }
    }
}