using System.Net;

namespace TestTask
{
    internal class Request
    {
        private string _url;
        private string _response;

        public Request(string url)
        {
            _url = url;
        }

        public string GetResponse()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
            request.Method = "GET";

            try
            {
                HttpWebResponse httpWebResponse = (HttpWebResponse)request.GetResponse();
                var stream = httpWebResponse.GetResponseStream();
                if (stream != null)
                {
                    _response = new StreamReader(stream).ReadToEnd();
                }

                return _response;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return string.Empty;
        }
    }
}
