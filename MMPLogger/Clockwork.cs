using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MMPLogger
{
    public class Clockwork
    {

        #region Constants

        public string ENDPOINT = string.Empty;
        public string API_KEY = string.Empty;

        #endregion


        #region Properties

        private static Lazy<Clockwork> _lazy = new Lazy<Clockwork>(() => new Clockwork());

        public static Clockwork Instance
        {
            get
            {
                return _lazy.Value;
            }
        }

        #endregion

        #region Public Methods

        public void Send(string number, string message)
        {
            if (string.IsNullOrEmpty(ENDPOINT) || string.IsNullOrEmpty(API_KEY) || string.IsNullOrEmpty(number))
            {
                return;
            }

            try
            {
                var url = string.Format("{0}?key={1}&to={2}&content={3}", ENDPOINT, API_KEY, number, message);
                var request = (HttpWebRequest)WebRequest.Create(url);

                using (var response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        Console.WriteLine(reader.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

    }
}
