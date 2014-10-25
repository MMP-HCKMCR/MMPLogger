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

        public Service1() //string[] args)
        {
            InitializeComponent();

            /*string eventSourceName = "MMPLogger";
            string logName = "MMPLoggerLog";

            if (args.Count() > 0)
            {
                eventSourceName = args[0];
            }

            if (args.Count() > 1)
            {
                logName = args[1];
            }

            EventLog eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists(eventSourceName))
            {
                System.Diagnostics.EventLog.CreateEventSource(eventSourceName, logName);
            }

            eventLog1.Source = eventSourceName;
            eventLog1.Log = logName;

            components.Add(eventLog1);*/
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
                    Clockwork.Instance.Send("07858055210", "Greetings!");
                    break;

                case SessionSwitchReason.SessionLogoff:
                    Clockwork.Instance.Send("07858055210", "Don't leeeeave meeee! :(");
                    break;

                case SessionSwitchReason.SessionLock:
                    Clockwork.Instance.Send("07858055210", "Bye~");
                    break;

                case SessionSwitchReason.SessionUnlock:
                    Clockwork.Instance.Send("07858055210", "Welcome back~");
                    break;
            }
        }

        #endregion
    }
}
