using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace TrackerServer.Rest
{
    internal class OtRestClient
    {
        internal Dictionary<string, string> Headers = new Dictionary<string, string>();

        [Obsolete]
        internal async Task<IRestResponse> Post(string endPoint, object r, Dictionary<string, string> headers = null)
        {
            endPoint = endPoint.Replace(",", "%2C");
            endPoint = endPoint.Replace(" ", "%20");
            RestClient client = new RestClient(endPoint);
            var request = new RestRequest(Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            if (headers != null)
                foreach (KeyValuePair<string, string> header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            var json = Serialize.ToJson(r);
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            return await ExecuteRequestAsync(client, request);
        }

        [Obsolete]
        internal async Task<IRestResponse> PostForm(string endPoint, Dictionary<string, string> content, Dictionary<string, string> headers = null)
        {
            endPoint = endPoint.Replace(",", "%2C");
            endPoint = endPoint.Replace(" ", "%20");
            RestClient client = new RestClient(endPoint);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            if (headers != null)
                foreach (KeyValuePair<string, string> header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            foreach (var data in content)
            {
                request.AddParameter(data.Key, data.Value, ParameterType.GetOrPost);
            }
            return await ExecuteRequestAsync(client, request);
        }

        [Obsolete]
        internal async Task<IRestResponse> Get(string endPoint, Dictionary<string, string> headers = null)
        {
            endPoint = endPoint.Replace(",", "%2C");
            endPoint = endPoint.Replace(" ", "%20");
            RestClient client = new RestClient(endPoint);
            var request = new RestRequest(Method.GET)
            {
                RequestFormat = DataFormat.Json
            };
            if (headers != null)
                foreach (KeyValuePair<string, string> header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            return await ExecuteRequestAsync(client, request);
        }

        [Obsolete]
        protected async Task<IRestResponse> ExecuteRequestAsync(RestClient client, RestRequest req)
        {
            Exception ex = null;
            var response = await client.ExecuteTaskAsync(req, new CancellationToken(), req.Method);
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.MethodNotAllowed:
                    ex = new BadRequestException(req, (RestResponse)response);
                    break;
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                    ex = new UnauthorizedException(req, (RestResponse)response);
                    break;
                case HttpStatusCode.NotFound:
                    ex = new NotFoundException(req, (RestResponse)response);
                    break;
                case (HttpStatusCode)429:
                    break;

            }
            if (ex == null)
                return response;
            Console.WriteLine($"{ex.Message} {client.BaseHost} {client.BaseUrl}");
            return null;
        }
    }
}
