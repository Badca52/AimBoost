using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://aimboost.azurewebsites.net/CheckLogin.axd");
            string requestParameters = string.Concat("Username|jlaughlin&Password|blahha");

            Byte[] requestBytes = System.Text.Encoding.GetEncoding(1252).GetBytes(requestParameters);
            request.Method = "POST";
            request.ContentLength = requestBytes.Length;
            request.Timeout = 10000;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(requestBytes, 0, requestBytes.Length);
            dataStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string responseText = reader.ReadToEnd();

                bool isLoggedIn = false;

                if (bool.TryParse(responseText, out isLoggedIn))
                {
                    // Succeeded. Proceed to download updates... etc..
                }
                else
                {
                    isLoggedIn = false;
                }
            }
            else
            {
                // No response from server
            }
        }
    }
}
