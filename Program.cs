using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace WebhookDeleter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Webhook deleter";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Webhook URL : ");
            string weburl = Console.ReadLine();
            Console.Clear();
            Console.Write("Last message : ");
            string lastmsg = Console.ReadLine();


            webhookmsg(weburl, lastmsg);
            deletewebhook(weburl);
        }

        public static string deletewebhook(string webhookurl, string UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.105 Safari/537.36")
        {
            try
            {
                HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(webhookurl);
                byte[] bytes = Encoding.ASCII.GetBytes("{}");
                Req.Method = "DELETE";
                Req.UserAgent = UserAgent;
                Req.ContentLength = (long)bytes.Length;
                using (Stream requestStream = Req.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }
                HttpWebResponse httpWebResponse = (HttpWebResponse)Req.GetResponse();
                return new StreamReader(httpWebResponse.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        private static void sendWebHook(string URL, string msg, string username)
        {
            Post(URL, new NameValueCollection()
        {
                {
                    "username", username
                },
                {
                    "content",  msg
                }
            });
        }
        private static byte[] Post(string uri, NameValueCollection pairs)
        {
            using (WebClient webclient = new WebClient())
                return webclient.UploadValues(uri, pairs);
        }
        public static void webhookmsg(string webhookurl, string msg)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            sendWebHook(webhookurl, msg, "Bye bye");
        }
    }
}
