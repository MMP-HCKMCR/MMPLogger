using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MMPLogger.MMPWebService;

namespace MMPLogger
{
    public partial class Service1 : ServiceBase
    {

        #region Constructor

        public Service1()
        {
            InitializeComponent();
            CanHandleSessionChangeEvent = true;
        }

        #endregion

        #region Startup Methods

        protected override void OnStart(string[] args)
        {
            Clockwork.Instance.ENDPOINT = ConfigurationManager.AppSettings["clockwork_url"];
            Clockwork.Instance.API_KEY = ConfigurationManager.AppSettings["clockwork_key"];
            DoAction(SessionChangeReason.SessionLogon);
        }

        protected override void OnStop()
        {
        }

        #endregion

        #region Session Logic

        protected override void OnSessionChange(SessionChangeDescription reason)
        {
            DoAction(reason.Reason);
        }

        private void DoAction(SessionChangeReason reason)
        {
            switch (reason)
            {
                case SessionChangeReason.SessionLogon:
                    Clockwork.Instance.Send("", "Greetings!");
                    break;

                case SessionChangeReason.SessionLogoff:
                    Clockwork.Instance.Send("", "Don't leeeeave meeee! :(");
                    break;

                case SessionChangeReason.SessionLock:
                    Clockwork.Instance.Send("", "Bye~");
                    break;

                case SessionChangeReason.SessionUnlock:
                    Clockwork.Instance.Send("", "Welcome back~");
                    break;
            }

            using (var service = new MMPService())
            {
                service.AddUserEventByEventTypeId(string.Format("{0}/{1}", Environment.UserDomainName, Environment.UserName), (int)reason);
            }
        }

        #endregion
    }
}
