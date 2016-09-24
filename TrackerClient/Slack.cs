using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace TrackerClient
{
    //A simple C# class to post messages to a Slack channel
    //Note: This class uses the Newtonsoft Json.NET serializer available via NuGet
    public class SlackClient
    {
        private readonly Uri _uri;
        private readonly Encoding _encoding = new UTF8Encoding();

        public SlackClient(string urlWithAccessToken)
        {
            _uri = new Uri(urlWithAccessToken);
        }

        //Post a message using simple strings
        public void PostMessage(Attachment attachment, string pretext = null, string fallback = null)
        {
            Payload payload = new Payload()
            {
                Attachments = attachment
            };

            PostMessage(payload);
        }

        //Post a message using a Payload object
        public void PostMessage(Payload payload)
        {
            string payloadJson = JsonConvert.SerializeObject(payload);
            payloadJson = payloadJson.Insert(payloadJson.IndexOf(":") + 1, "[");
            payloadJson = payloadJson.Insert(payloadJson.LastIndexOf('}'), "]");
            using (WebClient client = new WebClient())
            {
                NameValueCollection data = new NameValueCollection();
                data["payload"] = payloadJson;

                var response = client.UploadValues(_uri, "POST", data);

                //The response text is usually "ok"
                string responseText = _encoding.GetString(response);
            }
        }
    }

    //This class serializes into the Json payload required by Slack Incoming WebHooks
    public class Payload
    {
        [JsonProperty("attachments")]
        public Attachment Attachments { get; set; }
    }
    public class Attachment
    {
        public string Fallback;
        public string Color;
        public string Pretext;
        public string AuthorName;
        public string AuthorLink;
        public string AuthorIcon;
        public string Title;
        public string TitleLink;
        public string Text;
        public Fields[] Fields;

        public string ImageUrl;
        public string ThumbUrl;
        public string[] MrkdwnIn;
    }

    public class Fields
    {
        [JsonProperty("title")]
        public string Title;
        [JsonProperty("value")]
        public string Value;
        [JsonProperty("short")]
        public bool Short = true;
    }
}
