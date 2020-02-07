using System;
using RestSharp;

namespace TrackerServer.Rest
{
    class BadRequestException : Exception
    {
        public RestRequest Request { get; set; }
        public RestResponse Response { get; set; }
        public string ErrorMessage { get; set; }

        internal BadRequestException(RestRequest req, RestResponse res) : base("Bad Request: " + res.ErrorMessage)
        {
            Request = req;
            Response = res;
            ErrorMessage = Response.Content;
        }
    }
}