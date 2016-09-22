using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrectPassword.Log
{
   public class LogLocal
    {
        /// <summary>
        /// локальный лог, кладется в журналы windowds приложения код ошибки 1111
        /// </summary>
        /// <param name="events">событие</param>
        /// <param name="eventsType">тип события</param>
     public  static void addLocalLog(string events, EventLogEntryType eventsType)
        {
            string sSource;
            string sLog;           

            sSource = "Смена пароля локальному администратору";
            sLog = "Application";
          
            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);

            EventLog.WriteEntry(sSource, events, eventsType, 1111);


        } 

    }
}
