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

namespace MMPLogger
{
    public partial class Service1 : ServiceBase
    {

        #region Constructor

        public Service1()
        {
            InitializeComponent();
        }

        #endregion

        #region Startup Methods

        protected override void OnStart(string[] args)
        {
            Debugger.Break();

            Clockwork.Instance.ENDPOINT = ConfigurationManager.AppSettings["clockwork_url"];
            Clockwork.Instance.API_KEY = ConfigurationManager.AppSettings["clockwork_key"];
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
        }

        protected override void OnStop()
        {
        }

        #endregion

        #region Session Logic

        void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            DoAction(e.Reason);
        }

        private void DoAction(SessionSwitchReason reason)
        {
            switch (reason)
            {
                case SessionSwitchReason.SessionLogon:
                    Clockwork.Instance.Send("", "Greetings!");
                    break;

                case SessionSwitchReason.SessionLogoff:
                    Clockwork.Instance.Send("", "Don't leeeeave meeee! :(");
                    break;

                case SessionSwitchReason.SessionLock:
                    Clockwork.Instance.Send("", "Bye~");
                    break;

                case SessionSwitchReason.SessionUnlock:
                    Clockwork.Instance.Send("", "Welcome back~");
                    break;
            }
        }

        #endregion
    }
}
