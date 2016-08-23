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
                attachments = attachment
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
        public Attachment attachments { get; set; }
    }
    public class Attachment
    {
        public string fallback;
        public string color;
        public string pretext;
        public string author_name;
        public string author_link;
        public string author_icon;
        public string title;
        public string title_link;
        public string text;
        public Fields[] fields;

        public string image_url;
        public string thumb_url;
        public string[] mrkdwn_in;
    }

    public class Fields
    {
        [JsonProperty("title")]
        public string Title;
        [JsonProperty("value")]
        public string Value;
        [JsonProperty("short")]
        public bool @short = true;
    }
}
