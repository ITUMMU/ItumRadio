using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace itumRadio
{
    public class eventLogger : IDisposable
    {
        private String eventSource;

        public eventLogger(string Source, string LogName = "Application")
        {
            this.eventSource = Source;
        }

        public void writeLog(string Message)
        {
            EventLog.WriteEntry(this.eventSource, Message);
        }

        public void writeLog(string Message, EventLogEntryType Type)
        {
            EventLog.WriteEntry(this.eventSource, Message, Type);
        }

        public void writeLog(string Message, EventLogEntryType Type, int EventID)
        {
            EventLog.WriteEntry(this.eventSource, Message, Type, EventID);
        }

        public void writeLog(string Message, EventLogEntryType Type, int EventID, short Category)
        {
            EventLog.WriteEntry(this.eventSource, Message, Type, EventID, Category);
        }

        public void writeLog(string Message, EventLogEntryType Type, int EventID, short Category, byte[] RawData)
        {
            EventLog.WriteEntry(this.eventSource, Message, Type, EventID, Category, RawData);
        }

        public void Dispose()
        {
            eventSource = null;
        }

        ~eventLogger()
        {
            Dispose();
        }
    }
}

